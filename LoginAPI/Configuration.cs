namespace LoginAPI
{
    public static class Configuration
    {
        // ARMAZENAR ESSA CHAVE DE FORMA PRIVADA
        public static string PrivateKey { get;} = Environment.GetEnvironmentVariable("JWT_PRIVATE_KEY");
    }
}