using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore.V1;

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
        public async Task AddUserAsync(string Uid, string Email, string Password)
        {
            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);   // Criptografa a senha

            DocumentReference docRef = _firestoreDb.Collection(FirebaseConfig.DataBaseName).Document(Uid);
            await docRef.SetAsync(new 
            {
                Uid,
                Email,
                HashedPassword
            });
        }

        // Metodo que verifica se existe o usuario
        public async Task<bool> UserExistsAsync(string Email)
        {
            var users = await _firestoreDb.Collection(FirebaseConfig.DataBaseName)
                .WhereEqualTo("Email", Email).GetSnapshotAsync();
            return users.Count > 0;
        }


        // Metodo de validar o usuario
        public async Task<bool> ValidateUserAsync(string Email, string Password)
        {
            var users = await _firestoreDb.Collection(FirebaseConfig.DataBaseName)
                .WhereEqualTo("Email", Email).GetSnapshotAsync();

            // Não existe usuário
            if (users.Count == 0)
                return false;

            // Pega o documento e verifica a senha, caso a senha digitada seja igual a senha armazenada, retorna True
            var userDoc = users.Documents.First();
            var storedHash = userDoc.GetValue<string>("HashedPassword");

            return BCrypt.Net.BCrypt.Verify(Password, storedHash);
        }
    }
}
