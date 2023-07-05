using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Facturacion.Domain.DTOs;
using Facturacion.Application.Repository.Interfaces;
using Facturacion.Infrastruture.ApplicationDbContext;
using AutoMapper;
using Facturacion.Domain.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

namespace Facturacion.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _userServices;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
       

        public LoginController(ILoginServices userServices, IMapper mapper, IConfiguration configuration)
        {
            _userServices = userServices;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult> Login(UserDTO userDTO)
        {

            var User = _mapper.Map<User>(userDTO);

            var Admin =await _userServices.GetAdmin(User);

            if (Admin is  null)
            {

                return BadRequest(new { Error = "Las creadenciales no son validas"});

            }

            //Generar token
            var Token = GenerateToken(Admin);


            return Ok(new { token = Token });

        }

        
        private string GenerateToken(User user)
        {
                
            List<Claim> Claims = new()
            {

                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            //Obtenemos La llave
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            //obtenemos las credenciales 
            var Credetials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


            //Creamos el Token
            var SecurityToken = new JwtSecurityToken(

                    claims: Claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: Credetials

                    );

            //Serializamos el Token
            string Token = new JwtSecurityTokenHandler().WriteToken(SecurityToken);

            return Token;

        }
    }
}
