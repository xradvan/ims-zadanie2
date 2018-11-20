using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zadanie2_webapp.API.Data;
using zadanie2_webapp.API.Models;

namespace zadanie2_webapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTablesController : ControllerBase
    {
        private readonly DataContext _context;

        public TestTablesController(DataContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var values = await _context.TestTables.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.TestTables.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }


        // POST api/values
        [HttpPost("add")]
        public async Task<IActionResult> AddValue(TestTable table)
        {
            var tbs = await _context.TestTables.ToListAsync();
            var max = 0;
            foreach (var t in tbs) 
            {
                if (t.Id > max)
                    max = t.Id;
            }
            var tableToSave = new TestTable{
                Id = max,
                Data = table.Data
            };
            await _context.TestTables.AddAsync(table);
            await _context.SaveChangesAsync();
            
            return Ok(max);
        }
    }
}