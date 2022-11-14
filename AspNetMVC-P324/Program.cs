using AspNetMVC_P324.DAL;
using AspNetMVC_P324.Data;
using AspNetMVC_P324.Models.Entities.IdentityModel;
using AspNetMVC_P324.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVC_P324
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(
                    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<User, IdentityRole>
                (options=> {
                    options.User.RequireUniqueEmail=true;
                    options.Password.RequireLowercase=false;
                    options.SignIn.RequireConfirmedEmail=false;
                    options.Password.RequireUppercase=false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("Mailsettings"));

            builder.Services.AddTransient<IMailServices, MailManager>();
            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler("/Error");

            }
            app.UseStatusCodePagesWithReExecute("/Eror/Error1", "?code={0}");


            app.UseEndpoints(endpoints => {
                
                
             endpoints.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
             );
                
                app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            

            app.Run();
        }
    }
}