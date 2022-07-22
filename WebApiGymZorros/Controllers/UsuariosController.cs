using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiGymZorros.Contextos;
using WebApiGymZorros.DTOs.Usuarios;
using WebApiGymZorros.Entidades;

namespace WebApiGymZorros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;

        public UsuariosController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(UsuarioCrearDTO usuarioCrearDTO)
        {
            var usuario = mapper.Map<ApplicationUser>(usuarioCrearDTO);
            usuario.UserName = usuarioCrearDTO.Email;
            var resultado = await userManager.CreateAsync(usuario, usuarioCrearDTO.Password);

            if (resultado.Succeeded)
            {
                CredencialesUsuario cu = new CredencialesUsuario();
                cu.Email = usuarioCrearDTO.Email;
                cu.Password = usuarioCrearDTO.Password;
                return await ConstruirToken(cu);
            }

            return BadRequest(resultado.Errors);
        }

        //Logearse a la aplicacion
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrector");
            }
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuario.Email),
            };

            //Obtenemos los cleims de roles
            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsRoles = await userManager.GetClaimsAsync(usuario);

            //Funcionamos los claims
            claims.AddRange(claimsRoles);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LlaveJWT"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
            };
        }

    }
}
