using Microsoft.EntityFrameworkCore;
using Order.BL.Interfaces.Repo;
using Order.DAL.Data;
using Order.DAL.Repository;
using AutoMapper;
using Order.Web.MapperProfile;
using Order.BL.Interfaces;
using Order.App.Services;

namespace Order.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Context>(o =>
                    o.UseSqlServer(configuration.GetConnectionString("DataSource")));

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IProviderService, ProviderService>();

            builder.Services.AddAutoMapper(typeof(OrderProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Order}/{action=Index}/{id?}");

            app.Run();
        }
    }
}