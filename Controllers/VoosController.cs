using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoBasica.Api.Data;
using ProjetoAutenticacaoBasica.Api.Models;
using ProjetoAutenticacaoBasica.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ProjetoAutenticacaoBasica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VoosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CriarVoo( Voo voo)
        {
            var newVoo = new Voo
            {
                Codigo = voo.Codigo,
                Origem = voo.Origem,
                Destino = voo.Destino
            };

            _context.Voos.Add(newVoo);
            await _context.SaveChangesAsync();
            return Ok("Voo criado com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumoVooDto>>> GetVoos()
        {
            var voos = await _context.Voos.AsNoTracking().OrderBy(v => v.Codigo).Select(v => new ResumoVooDto
            {
                Id = v.Id,
                Codigo = v.Codigo,
                Trajeto = v.Origem + "->" + v.Destino
            })
            .ToListAsync();
           return Ok(voos);
        }
    }
}
