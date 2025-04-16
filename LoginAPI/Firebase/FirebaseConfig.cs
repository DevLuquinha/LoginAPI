using Google.Apis.Auth.OAuth2;

namespace LoginAPI.Firebase
{
    public class FirebaseConfig
    {
        public static string? ProjectId { get; } = Environment.GetEnvironmentVariable("FIREBASE_PROJECT_ID");
        public static string? DataBaseName { get; } = Environment.GetEnvironmentVariable("FIREBASE_DB_NAME");
        
        public static string base64 = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIAL_BASE64");
        
        public static string Json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
    }
}