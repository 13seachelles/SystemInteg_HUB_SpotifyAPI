using HUB.Services;

namespace HUB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>(c =>
            {
                c.BaseAddress = new Uri("https://accounts.spotify.com/api/");
            });

            builder.Services.AddHttpClient<ISpotifyService, SpotifyService>(c =>
            {
                c.BaseAddress = new Uri("https://api.spotify.com/v1/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddControllersWithViews();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
