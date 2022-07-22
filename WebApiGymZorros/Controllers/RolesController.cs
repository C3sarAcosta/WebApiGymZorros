using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiGymZorros.DTOs.Roles;
using WebApiGymZorros.Entidades;

namespace WebApiGymZorros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        
        [HttpPost("AsignarAdministrador")]
        public async Task<ActionResult> AsignarAdministrador(RolUsuariosDTO rolUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(rolUsuariosDTO.Email);
            if (usuario != null)
            {
                await userManager.AddClaimAsync(usuario, new Claim("Administrador", "1"));
                return NoContent();
            }
            return BadRequest();
        }

        
        [HttpPost("AsignarInstructor")]
        public async Task<ActionResult> AsignarInstructor(RolUsuariosDTO rolUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(rolUsuariosDTO.Email);
            if (usuario != null)
            {
                await userManager.AddClaimAsync(usuario, new Claim("Instructor", "1"));
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost("AsignarUsuario")]
        public async Task<ActionResult> AsignarUsuario(RolUsuariosDTO rolUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(rolUsuariosDTO.Email);
            if (usuario != null)
            {
                await userManager.AddClaimAsync(usuario, new Claim("Usuario", "1"));
                return NoContent();
            }
            return BadRequest();
        }


        [HttpPost("EliminarAdministrador")]
        public async Task<ActionResult> EliminarAdministrador(RolUsuariosDTO rolUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(rolUsuariosDTO.Email);
            if (usuario != null)
            {
                await userManager.RemoveClaimAsync(usuario, new Claim("Administrador", "1"));
                return NoContent();
            }
            return BadRequest();
        }


        [HttpPost("EliminarInstructor")]
        public async Task<ActionResult> EliminarInstructor(RolUsuariosDTO rolUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(rolUsuariosDTO.Email);
            if (usuario != null)
            {
                await userManager.RemoveClaimAsync(usuario, new Claim("Instructor", "1"));
                return NoContent();
            }
            return BadRequest();
        }


        [HttpPost("EliminarUsuario")]
        public async Task<ActionResult> EliminarUsuario(RolUsuariosDTO rolUsuariosDTO)
        {
            var usuario = await userManager.FindByEmailAsync(rolUsuariosDTO.Email);
            if (usuario != null)
            {
                await userManager.RemoveClaimAsync(usuario, new Claim("Usuario", "1"));
                return NoContent();
            }
            return BadRequest();
        }
    }
}
