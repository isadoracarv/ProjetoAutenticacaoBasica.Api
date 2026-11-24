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
    public class PassageirosController : ControllerBase
    {
        private AppDbContext _context;

        public PassageirosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPassageiro(CriarPassageiroDto dto)
        {
            var novoPassageiro = new Passageiro
            {
                Cpf = dto.Cpf,
                Nome = dto.Nome
            };

            _context.Passageiros.Add(novoPassageiro);
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = "Passageiro criado com sucesso!" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoPassageiroDto>>> GetPassageiros([FromQuery] string? nome)
        {
            var passageiros = await _context.Passageiros.AsNoTracking().Where(p => string.IsNullOrEmpty(nome) || p.Nome.Contains(nome)).OrderBy(p => p.Nome).Select(p => new InfoPassageiroDto
            {
                Id = p.Id,
                Nome = p.Nome
            }).ToListAsync();
            return Ok(passageiros);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<InfoPassageiroDto>> GetPassageiroPorCpf(string cpf)
        {
            var passageiro = await _context.Passageiros.AsNoTracking().Where(p => p.Cpf == cpf).Select(p => new InfoPassageiroDto
            {
                Id = p.Id,
                Nome = p.Nome
            }).FirstOrDefaultAsync();

            if (passageiro is null)
            {
                return NotFound(new { Mensagem = "Passageiro não encontrado." });
            }

            return Ok(passageiro);
        }
    }
}
