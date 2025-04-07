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
            var exists = await _firestore.UserExistsAsync(dto.Email);
            if (exists)
                return Conflict("Usuário já existe.");

            await _firestore.AddUserAsync(Guid.NewGuid().ToString(), dto.Email);
            return Ok("Usuário registrado com sucesso.");
        }

    }
    public class SignUpDto
    {
        public string Email { get; set; }
    }
}
