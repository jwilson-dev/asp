using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using asp.Models;
using Google.Cloud.Diagnostics.AspNetCore;
using MySql.Data.MySqlClient;
using Npgsql;
using Polly;

namespace asp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        IServiceCollection _services;
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AspContext>
            (options => options.UseSqlServer(Configuration["Data:ASPConnection:ConnectionString"]));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_1);
            string projectId = Google.Api.Gax.Platform.Instance().ProjectId;
            if (!string.IsNullOrEmpty(projectId))
            {
                services.AddGoogleExceptionLogging(options =>
                {
                    options.ProjectId = projectId;
                    options.ServiceName = "CloudSqlSample";
                    options.Version = "Test";
                });
            }
            services.AddSingleton(typeof(DbConnection), (IServiceProvider) => InitializeDatabase());
            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

        }


        DbConnection InitializeDatabase()
        {
            DbConnection connection;
            string database = Configuration["CloudSQL:Database"];
            connection = GetMySqlConnection();
            connection.Open();
            using (var createTableCommand = connection.CreateCommand())
            {
                createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS
                votes(vode_id SERIAL NOT NULL,
                time_cast timestamp NOT NULL,
                candidate CHAR(6) NOT NULL,
                PRIMARY KEY (vote_id)
                )";
                createTableCommand.ExecuteNonQuery();
            }
            return connection;
        }

        DbConnection NewMysqlConnection()
        {
            var connectionString = new MySqlConnectionStringBuilder(
                Configuration["CloudSql: ConnectionString"])
            {
                SslMode = MySqlSslMode.None,
            };
            connectionString.Pooling = true;
            connectionString.MaximumPoolSize = 5;
            connectionString.MinimumPoolSize = 0;
            connectionString.ConnectionTimeout = 15;
            connectionString.ConnectionLifeTime = 1800;
            DbConnection connection = new MySqlConnection(connectionString.ConnectionString);
            return connection;
        }

        DbConnection GetMySqlConnection()
        {
            var connection = Policy
                .HandleResult<DbConnection>(connection => connection.State != ConnectionState.Open)
                .WaitAndRetry(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(5)
                }, (IAsyncResult, TimeSpan, retryCount, context) =>
                {

                })
                .Execute(() => NewMysqlConnection());
            return connection;

        }


    }
}
