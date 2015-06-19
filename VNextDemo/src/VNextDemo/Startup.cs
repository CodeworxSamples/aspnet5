using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using Microsoft.Framework.ConfigurationModel;

namespace VNextDemo
{
    public class Startup
    {
        public static IConfiguration Config;

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Config = new Configuration()
                        .AddJsonFile("config.json")
                        .AddEnvironmentVariables();

            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BookContext>(options => options
                                .UseSqlServer(Config.Get("Data:DefaultConnection:ConnectionString")));

            services.AddSwagger(s => {
                s.SwaggerGenerator(c => {
                    c.Schemes = new[] { "http", "https" };
                    c.SingleApiVersion(new Info {
                        Version = "v1",
                        Title = "Swashbuckle Sample API",
                        Description = "A sample API for testing Swashbuckle",
                        TermsOfService = "Some terms ..."
                    });
                });

                s.SchemaGenerator(opt => opt.DescribeAllEnumsAsStrings = true);
            });
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
