using BuiltCode.API.Dto;
using BuiltCode.API.Helpers;
using BuiltCode.Domain.Entities;
using BuiltCode.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.API.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AccountControler : ControllerBase
    {
        private readonly AppDbContext _db;

        public AccountControler(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Login para usuários com perfil de acesso Admin e Atendente
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto? loginDTO)
        {
            var user = await _db.Usuarios.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (user == null) return NotFound(new { message = "Usuário ou senha inválidos" });

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Senha));

           for (int i = 0; i < computedHash.Length; i++)
           {
               if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
           }

           var token = TokenService.GenerateToken(user);

           return new LoginResponseDto()
           {
                Token = token,
                ExpiresIn = DateTime.Now.AddHours(2),
                UserInfo = new UserInfoDto()
                {
                    Email = user.Email,
                    Name = user.Nome,
                    Perfil = CarregarPerfil(user)
                }
           };
        }

        /// <summary>
        /// Endpoint para registro de novos atendentes, apenas usuário Admin está habilitado.
        /// </summary>
        /// <param name="registroDTO"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar(RegistroDto registroDTO)
        {
            if (await EmailRegistrado(registroDTO.Email)) return BadRequest("E-mail já está em uso");

            using var hmac = new HMACSHA512();

            var user = new Usuario
            {
                Nome = registroDTO.Nome,
                Email = registroDTO.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDTO.Senha)),
                PasswordSalt = hmac.Key,
                Perfil = (EPerfil)1
            };

            _db.Usuarios.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        private string CarregarPerfil(Usuario user)
        {
            return user.Perfil == 0 ? "Admin" : "Atendente";
        }

        private async Task<bool> EmailRegistrado(string email)
        {
            return await _db.Usuarios.AnyAsync(x => x.Email == email.ToLower());
        }
    }
}
