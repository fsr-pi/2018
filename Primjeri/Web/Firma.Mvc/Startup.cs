using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Firma.Mvc
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddUserSecrets("Firma")
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
     
      //application settings
      var appSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSection);
      
      string connectionString = appSection["ConnectionString"];
      connectionString = connectionString.Replace("sifra", Configuration["FirmaSqlPassword"]);

      services.AddDbContext<Models.FirmaContext>(options => options.UseSqlServer(connectionString));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      
      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(null, "Artikl/Page{page}", new { Controller = "Artikl", action = "Index" });
        routes.MapRoute(null, "Mjesto/Page{page}", new { Controller = "Mjesto", action = "Index" });
        routes.MapRoute(
              name: "default",
              template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
