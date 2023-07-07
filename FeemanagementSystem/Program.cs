using FeemanagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace FeemanagementSystem
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			builder.Services.AddSession();
			
			builder.Services.AddDbContext<FeeManagementSystemContext>(option => option.UseSqlServer(builder.Configuration["Conn"]));
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => option.LoginPath = "/Home/Index");
			var app = builder.Build();
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();
			app.MapControllerRoute(
				name:"default",			
				pattern:"{controller=Home}/{action=Index}/{id?}");
			app.Run();
		}
	}
}