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

namespace VNextDemo
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BookContext>(options =>
                    options.UseSqlServer("Data Source=.;Initial Catalog=VNextDemo;Integrated Security=True;"));

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
