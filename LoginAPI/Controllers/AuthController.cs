﻿using Microsoft.AspNetCore.Mvc;
using LoginAPI.Firebase;
using LoginAPI.Services;

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
        public async Task<IActionResult> SignUp([FromBody] SignUpDto Dto)
        {
            bool exists = await _firestore.UserExistsAsync(Dto.Email);  // Verifica se existe o usuario com o email e senha
            if (exists)
                return Conflict("Já existe uma conta com esse e-mail! Logue na sua conta ou crie outro usuário.");

            await _firestore.AddUserAsync(Dto.Uid, Dto.Email, Dto.Password);
            return Ok("Conta registrada com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInDto Dto)
        {
            bool exists = await _firestore.ValidateUserAsync(Dto.Email, Dto.Password);  // Verifica se existe o usuario com o email e senha
            if (!exists)
                return Unauthorized("Email ou senha incorretos! Por favor, digite novamente.");
            
            TokenService tokenService = new TokenService();
            var token = tokenService.GenerateToken(Dto.Uid);

            return Ok( new {token} );
        }
    }

    #region DTOs
    public class SignUpDto
    {
        public string Uid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInDto
    {
        public string Uid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    #endregion
}
