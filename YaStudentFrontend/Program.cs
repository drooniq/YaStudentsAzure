using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using YaStudentFrontend.Services;

namespace YaStudentFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://yastudents-api-app--yastudents-api-app.ashyglacier-4533aebf.swedencentral.azurecontainerapps.io") });
            builder.Services.AddScoped<StudentService>();

            await builder.Build().RunAsync();
        }
    }
}
