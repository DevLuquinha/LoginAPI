using Microsoft.AspNetCore.Mvc;
using LoginAPI.Firebase;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseService _firestore;

        public AuthController()
        {
            _firestore = new FirebaseService();
        }

        // Registro de usuarios
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
        {
            bool exists = await _firestore.UserExistsAsync(dto.Email);  // Verifica se existe o usuario com o email
            if (exists)
                return Conflict("Já existe uma conta com esse e-mail! Logue na sua conta ou crie outro usuário.");

            await _firestore.AddUserAsync(Guid.NewGuid().ToString(), dto.Email);
            return Ok("Conta registrada com sucesso!");
        }

    }
    public class SignUpDto
    {
        public string Email { get; set; }
    }
}
