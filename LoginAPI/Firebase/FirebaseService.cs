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
            string path = FirebaseConfig.CredentialPath;

            GoogleCredential credential = GoogleCredential.FromFile(path);
            var builder = new FirestoreClientBuilder
            {
                Credential = credential
            };

            _firestoreDb = FirestoreDb.Create(FirebaseConfig.ProjectId, builder.Build());
        }

        // Metodo que adiciona o usuario
        public async Task AddUserAsync(string uid, string email)
        {
            var docRef = _firestoreDb.Collection(FirebaseConfig.DataBaseName).Document(uid);
            await docRef.SetAsync(new { email });
        }

        // Metodo que verifica se existe o usuario
        public async Task<bool> UserExistsAsync(string email)
        {
            var users = await _firestoreDb.Collection(FirebaseConfig.DataBaseName)
                .WhereEqualTo("email", email).GetSnapshotAsync();
            return users.Count > 0;
        }
    }
}
