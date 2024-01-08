using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Country;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<ApiResult<GetCountryDTO>>> GetCountries(int pageIndex = 0, int pageSize = 10,
           string sortColumn = null, string sortOrder = null, string filterColumn = null, string filterQuery = null)
        {
            return await ApiResult<GetCountryDTO>.CreateAsync(_context.Countries.Select(c => new GetCountryDTO()
            {
                Id = c.Id,
                Name = c.Name,
                ISO2 = c.ISO2,
                ISO3 = c.ISO3,
                TotCities = c.Cities.Count
            }), pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        // POST: api/Countries/IsDupeCountry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("IsDupeField")]
        public bool IsDupeField(int CountryId, string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "Name": return _context.Countries.Any(c => c.Name == fieldValue && c.Id != CountryId);
                case "ISO2": return _context.Countries.Any(c => c.ISO2 == fieldValue && c.Id != CountryId);
                case "ISO3": return _context.Countries.Any(c => c.ISO3 == fieldValue && c.Id != CountryId);
                default: return false;
            }
        }
    }
}
