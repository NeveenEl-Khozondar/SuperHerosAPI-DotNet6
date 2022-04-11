using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstEFAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperController : ControllerBase
    {

        private readonly DataContextOfDB _context;

        public SuperController(DataContextOfDB context)
        {
            _context = context;
        }

        //AddGetMethod
        [HttpGet]
        //create an asynchronous method 
        //IActionResult will not return anything or the schema I need to return the interface  
        //public async Task<IActionResult> Get()
        public async Task<ActionResult<List<SuperAPI>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
            //this the previous way
            //return OK(hearoes) statue here 200 
             //  return BadRequest(heroes); // would result in a 400
            //return NotFound(heroes); // not found
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperAPI>> Get(int id)
        {

            //the old way
            //var hero = heroes.Find(h => h.Id == id);
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }
        [HttpPost]
        // SuperAPI hero is the request object
        //[FromBody]SuperAPI hero --  I just write like this if I have specific type but in this case because I have a complex type so no need for FromBody
        public async Task<ActionResult<List<SuperAPI>>> AddHreo (SuperAPI hero)
        {
            // I use SaveChangesAsync because here with Add method I change something in my DB tables so I need to save these changes 
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
            //the previous way
            //heroes.Add(hero);
            //return Ok(heroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperAPI>>> UpdateHero(SuperAPI request)
        {
            //var hero = heroes.Find(h => h.Id == request.Id);
            //if (hero == null)
            //    return BadRequest("Hero not found");
            //            dbhero.Name = request.Name;
           // hero.FirstName = request.FirstName;
           // hero.LasttName = request.LasttName;
           // hero.Place = request.Place;
           // return Ok(heroes);

            var dbhero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
                return BadRequest("Hero not found");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LasttName = request.LasttName;
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperAPI>>> Delete(int id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero not found");
            _context.SuperHeroes.Remove(dbhero);

            await _context.SaveChangesAsync(); 
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
