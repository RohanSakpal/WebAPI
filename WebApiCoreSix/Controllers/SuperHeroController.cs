using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCoreSix.Models;

namespace WebApiCoreSix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public SuperHeroController(DataContext context)
        {
            _dataContext = context;
        }
        public static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero { Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York"
                },
                new SuperHero { Id = 2,
                    Name = "IronMan",
                    FirstName = "TOny",
                    LastName = "Strark",
                    Place = "USA"
                }
            };
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            //without db
            //return Ok(heroes);

            //with db
            return Ok(await _dataContext.SuperHeroes.ToListAsync());

        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            //var hero = heroes.Find(h=> h.Id == id);
            //if (hero == null)
            //    return BadRequest("Hero not found");
            //return Ok(hero);

            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero heroU)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(heroU.Id);
            if (hero == null)
                return BadRequest("Hero not found");
            
            hero.Name = heroU.Name;
            hero.FirstName = heroU.FirstName;
            hero.LastName = heroU.LastName;
            hero.Place = heroU.Place;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");

            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

    }

}
