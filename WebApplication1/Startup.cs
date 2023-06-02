using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApplocation1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký dịch vụ MailSettings
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            // Đăng ký dịch vụ IEmailService với scope Transient
            services.AddSingleton<IEmailService, EmailService>();

            // Đăng ký các dịch vụ khác
            services.AddControllersWithViews();
        }
    }
}
