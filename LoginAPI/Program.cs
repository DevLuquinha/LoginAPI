using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using LoginAPI.Firebase;
using LoginAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LoginAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load(); // Isso carrega o .env automaticamente
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<TokenService>();

            // Definição do JWT
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; 
                x.SaveToken = true;             // Salvar o token no cookie
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
            // Inicializar o firebase
            FirebaseApp.Create(new AppOptions
            {
                // Credential = FirebaseConfig.GetCredential()
                Credential = GoogleCredential.FromJson(FirebaseConfig.Json)
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
