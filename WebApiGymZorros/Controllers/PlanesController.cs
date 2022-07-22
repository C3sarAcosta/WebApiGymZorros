using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGymZorros.Contextos;
using WebApiGymZorros.DTOs.Planes;
using WebApiGymZorros.Entidades;

namespace WebApiGymZorros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PlanesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[AllowAnonymous]
        [HttpGet("PlanEspecifico/{id:int}")]
        public async Task<ActionResult<PlanDTO>> PlanEspecifico(int id)
        {
            var plan = await context.Planes.FirstOrDefaultAsync(x => x.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return mapper.Map<PlanDTO>(plan);
        }

        //[AllowAnonymous]
        [HttpGet("ListarPlanesActivos")]
        public async Task<ActionResult<List<PlanDTO>>> ListarPlanesActivos()
        {
            var planes = await context.Planes.Where(x => x.Activo == true).ToListAsync();
            return mapper.Map<List<PlanDTO>>(planes);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpGet("ListarPlanes")]
        public async Task<ActionResult<List<PlanDTO>>> ListarPlanes()
        {
            var planes = await context.Planes.ToListAsync();
            return mapper.Map<List<PlanDTO>>(planes);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpPost("CrearPlan")]
        public async Task<ActionResult> Post(PlanCrearDTO planCrearDTO)
        {
            var existePlan = await context.Planes.AnyAsync(x => x.Nombre == planCrearDTO.Nombre);
            if (existePlan)
            {
                return BadRequest($"El plan {planCrearDTO.Nombre} ya existe");
            }

            var plan = mapper.Map<Plan>(planCrearDTO);
            plan.FechaCreacion = DateTime.UtcNow;
            plan.Activo = true;
            context.Add(plan);
            await context.SaveChangesAsync();
            return Ok(planCrearDTO);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpPut("ModificarTodoPlan/{id:int}")]
        public async Task<ActionResult> ModificarTodoPlan(int id, PlanModificarDTO planModificarDTO)
        {
            var existePlan = await context.Planes.AnyAsync(x => x.Id == id);
            if (!existePlan)
            {
                return BadRequest($"El plan no existe");
            }

            var plan = mapper.Map<Plan>(planModificarDTO);
            plan.Id = id;
            plan.FechaCreacion = DateTime.UtcNow;
            context.Update(plan);
            await context.SaveChangesAsync();
            return NoContent();
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpPatch("ModificarPlan/{id:int}")]
        public async Task<ActionResult> ModificarPlan(int id, JsonPatchDocument<PlanModificarDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var plan = await context.Planes.FirstOrDefaultAsync(x => x.Id == id);
            if (plan == null)
            {
                return NotFound();
            }
            var planDTO = mapper.Map<PlanModificarDTO>(plan);
            patchDocument.ApplyTo(planDTO, ModelState);

            var esValido = TryValidateModel(planDTO);
            if (!esValido)
            {
                return BadRequest();
            }

            mapper.Map(planDTO, plan);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
