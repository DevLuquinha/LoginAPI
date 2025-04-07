using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace LoginAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Inicializar o firebase
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\Micro\\Desktop\\Keys\\SDK-Admin-Firebase.json"),
            });



            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}
