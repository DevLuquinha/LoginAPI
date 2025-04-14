using Google.Apis.Auth.OAuth2;

namespace LoginAPI.Firebase
{
    public class FirebaseConfig
    {
        // Variaveis de ambiente
        public static string? CredentialJson { get; } = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIAL_JSON");
        
        public static string? ProjectId { get; } = Environment.GetEnvironmentVariable("FIREBASE_PROJECT_ID");
        
        public static string? DataBaseName { get; } = Environment.GetEnvironmentVariable("FIREBASE_DB_NAME");

        // Metodo para pegar o JSON da chave do Firebase e transformar em string
        public static GoogleCredential GetCredential()
        {
            var json = CredentialJson;

            if (string.IsNullOrEmpty(json))
                throw new Exception("FIREBASE_CREDENTIAL_JSON Não foi Encontrado");

            // Converte string para stream para passar ao GoogleCredential
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            return GoogleCredential.FromStream(stream);
        }
    }
}