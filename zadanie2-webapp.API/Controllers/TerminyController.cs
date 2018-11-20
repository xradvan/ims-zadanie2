using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zadanie2_webapp.API.Data;
using zadanie2_webapp.API.Models;

namespace zadanie2_webapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminyController : ControllerBase
    {
        private readonly DataContext _context;

        public TerminyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTerminy() 
        {
            var terminy = await _context.Terminy.ToListAsync();
            return Ok(terminy);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTermin(int id) 
        {
            var termin = await _context.Terminy.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(termin);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTermin(int id, Termin updateTermin) 
        {
            _context.Terminy.Update(updateTermin);
            _context.SaveChanges();
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> PostTermin(Termin termin)
        {   
            // Posledny pridany termin
            var lastTermin = await _context.Terminy.LastOrDefaultAsync();
            if (lastTermin == null) 
            {
                termin.Id = 1;
            }
            else 
            {
                termin.Id = lastTermin.Id + 1; 
            }                
            await _context.Terminy.AddAsync(termin);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}