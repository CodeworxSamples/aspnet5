using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Swashbuckle.Swagger;

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
                        Title = "Bookservice Sample API",
                        Description = "The worlds greatest online book store.",
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

            //app.UseWelcomePage();
        }
    }
}
