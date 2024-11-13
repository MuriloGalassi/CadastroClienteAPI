using Microsoft.AspNetCore.Mvc;
using CadastroClienteAPI.Data;
using CadastroClienteAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace CadastroClienteAPI.Controllers
{
    [Route("api/[cliente]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();

            return Ok(clientes);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente([FromQuery]int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) 
                return NotFound();

            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpGet]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id) 
                return BadRequest();

            _context.Entry(cliente).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromQuery] int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) 
                return NotFound();

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Considerações:
        // 1 - Notação "HttpDelete" estava faltando no endpoint de deletar. Não iria funcionar
        // 2 - Evitar uso de código de retorno "No Content" quando a requisição for feita corretamente: Apesar de ser parte do grupo dos status 200, o código 204 pode induzir 
        //o front-end ao erro. Usar status 200 - Ok
        // 3 (toc do guilherme, hehe) - tente usar a indentação nos "ifs" e um pouco mais de espaço entre as linhas, isso deixa o código mais "bonito" e mais fácil de ler.
        //Uma coisa muito importante: Você não escreve código pra computadores, mas sim pra outros programadores! Então, quanto mais legível o código, melhor
    }
}