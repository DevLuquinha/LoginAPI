using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore.V1;
using System.Net;

namespace LoginAPI.Firebase
{
    public class FirebaseService
    {
        private readonly FirestoreDb _firestoreDb;

        // Construtor da Classe
        public FirebaseService()
        {
            // Caminho absoluto para sua chave do Firebase
            string path = @"C:\Users\Micro\Desktop\Keys\SDK-Admin-Firebase.json";

            GoogleCredential credential = GoogleCredential.FromFile(path);
            var builder = new FirestoreClientBuilder
            {
                Credential = credential
            };

            _firestoreDb = FirestoreDb.Create("sistema-de-login-do-luquinha", builder.Build());
        }

        public async Task AddUserAsync(string uid, string email)
        {
            var docRef = _firestoreDb.Collection("Usuarios").Document(uid);
            await docRef.SetAsync(new { email });
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var users = await _firestoreDb.Collection("Usuarios")
                .WhereEqualTo("email", email).GetSnapshotAsync();
            return users.Count > 0;
        }
    }
}
