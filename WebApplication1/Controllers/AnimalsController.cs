using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public AnimalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Animal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAnimal()
    {
        return await _context.Animals.ToListAsync();
    }

    // GET: api/Animal/5
    [HttpGet("{animalid:int}")]
    public async Task<ActionResult<Animal>> GetAnimal(int animalid)
    {
        var animal = await _context.Animals.FindAsync(animalid);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }

    // PUT: api/Animal/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{animalid:int}")]
    public async Task<IActionResult> PutAnimal(int? animalid, Animal animal)
    {
        if (animalid != animal.AnimalId)
        {
            return BadRequest();
        }

        _context.Entry(animal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AnimalExists(animalid))
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

    // POST: api/Animal
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAnimal", new { animalid = animal.AnimalId }, animal);
    }

    // DELETE: api/Animal/5
    [HttpDelete("{animalid:int}")]
    public async Task<IActionResult> DeleteAnimal(int? animalid)
    {
        var animal = await _context.Animals.FindAsync(animalid);
        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AnimalExists(int? animalid)
    {
        return _context.Animals.Any(e => e.AnimalId == animalid);
    }
}
