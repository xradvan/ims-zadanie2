using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zadanie2_webapp.API.Data;
using zadanie2_webapp.API.Models;

namespace zadanie2_webapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovinkyController : ControllerBase
    {
        private readonly DataContext _context;

        public NovinkyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNovinky() 
        {
            var novinky = await _context.Novinky.ToListAsync();
            return Ok(novinky);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNovinka(int id) 
        {
            var novinka = await _context.Novinky.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(novinka);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNovinka(int id, Novinka updateNovinka) 
        {
            _context.Novinky.Update(updateNovinka);
            _context.SaveChanges();
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> PostNovinka(Novinka novinka)
        {   
            // Posledna pridana novinka
            var lastNovinka = await _context.Novinky.LastOrDefaultAsync();
            if (lastNovinka == null) 
            {
                novinka.Id = 1;
            }
            else 
            {
                novinka.Id = lastNovinka.Id + 1; 
            }                
            await _context.Novinky.AddAsync(novinka);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}