using Fantasy.Backend.Data;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _ctx;

        public CountriesController(DataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _ctx.Add(country);
            await _ctx.SaveChangesAsync();
            return Ok(country);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _ctx.Countries.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _ctx.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            var currentCountry = await _ctx.Countries.FindAsync(country.Id);

            if (country == null)
            {
                return NotFound();
            }

            currentCountry.Name = country.Name;
            _ctx.Update(currentCountry);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _ctx.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            country.Name = country.Name;
            _ctx.Remove(country);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}