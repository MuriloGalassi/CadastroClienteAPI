using Microsoft.AspNetCore.Mvc;
using CadastroClienteAPI.Models;
using Microsoft.EntityFrameworkCore;
using CadastroClienteAPI.Data.Context;


namespace CadastroClienteAPI.Controllers
{
    [Route("api/clientes")]
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
        [HttpPost]
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
                return NotFound();

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
    }
}