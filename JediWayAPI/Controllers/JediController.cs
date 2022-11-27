using Microsoft.AspNetCore.Mvc;

namespace JediWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JediController : ControllerBase
    {
        private readonly DataContext _context;
        public JediController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("v1")]
        public async Task<ActionResult<List<Jedi>>> Get()
        {
            return Ok(await _context.Jedis.ToListAsync());
        }

        [HttpGet("v1/{id}")]
        public async Task<ActionResult<Jedi>> Get(int id)
        {
            var hero = await _context.Jedis.FindAsync(id);

            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPost("v1")]
        public async Task<ActionResult<List<Jedi>>> AddHero(Jedi hero)
        {
            _context.Jedis.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Jedis.ToListAsync());
        }

        /// <summary>
        /// Update super hero
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("v1")]
        public async Task<ActionResult<Jedi>> UpdateHero(Jedi request)
        {
            var dbHero = await _context.Jedis.FindAsync(request.Id);

            if (dbHero == null)
                return BadRequest("Hero not found");


            dbHero.FirstName = string.IsNullOrWhiteSpace(request.FirstName)
                ? dbHero.FirstName
                : request.FirstName;
            dbHero.LastName = string.IsNullOrWhiteSpace(request.LastName)
                ? dbHero.LastName
                : request.LastName;
                        dbHero.Homeworld = string.IsNullOrWhiteSpace(request.Homeworld)
                ? dbHero.Homeworld
                : request.Homeworld;
            dbHero.Species = string.IsNullOrWhiteSpace(request.Species)
                ? dbHero.Species
                : request.Species;
            dbHero.Gender = string.IsNullOrWhiteSpace(request.Gender)
                ? dbHero.Gender
                : request.Gender;

            await _context.SaveChangesAsync();
            return Ok(await _context.Jedis.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Jedi>>> Delete(int id)
        {
            var dbHero = await _context.Jedis.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            _context.Jedis.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Jedis.ToListAsync());
        }

    }
}
