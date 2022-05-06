using AlunosAPI.Repository.Interfaces;
using AlunosAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlunosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAuthenticate _auth;
        private readonly IConfiguration _configuration;

        public AccountController(IAuthenticate auth, IConfiguration configuration)
        {
            _auth = auth ?? throw new ArgumentNullException(nameof(auth));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> LoginUser(LoginModel userInfo)
        {
            var result = await _auth.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
                return GenerateToken(userInfo);
            else
            {
                ModelState.AddModelError("LoginUser", "Registro Inváido.");
                return BadRequest(ModelState);
            }

        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser(RegisterModel model)
        {
           
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "As senhas devem ser iguais.");
                    return BadRequest(ModelState);
                }

                var result = await _auth.RegisterUser(model.Email, model.Password);

                if (result)
                    return Ok($"Usuário {model.Email} criado com sucesso");
                else
                {
                    ModelState.AddModelError("CreateUser", "Registro Inváido.");
                    return BadRequest(ModelState);
                }

        }

        private ActionResult<UserToken> GenerateToken(LoginModel model)
        {
            var claims = new[]
            {
                new Claim("email", model.Email),
                new Claim("meuToken", "token do PH"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };
            //Recupera a chave secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //Faz a assinatura digital do token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Define a expiração do token
            var expiration = DateTime.UtcNow.AddMinutes(20);

            //Gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            //Retorna o token como string
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }
    }
}
