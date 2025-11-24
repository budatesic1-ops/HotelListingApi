using HotelListingApi.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static List<Hotel> hotels = new List<Hotel>
        {
            new Hotel { ID = 1, Name = "Grand Plaza", Address = "123 main St", Rating =  4.5},
            new Hotel { ID = 2,Name = "Ocean View", Address = "465 Beach Rd", Rating  =4.8}
        };
        // GET: api/<HotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return hotels; 
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel>  Get(int id)
        {
            var hotel = hotels.FirstOrDefault(x => x.ID == id);

            if (hotel == null)
            {
            return NotFound(); 
            }

            return Ok(hotel);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
        {
            if (hotels.Any(h => h.ID == newHotel.ID))
            {
                return BadRequest("This hotel with this ID already exists");
            }
            hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { id = newHotel.ID }, newHotel);
        }
 

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel) //DA LI TREBA HOTEL???
        {
           var existingHotel = hotels.FirstOrDefault(x => x.ID == id);
            if (existingHotel == null)
            {
                return NotFound();
            }

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();

        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.ID == id);
            if(hotel == null)
            {
                return NotFound(new {message = "Hotel not found"});
            }
            hotels.Remove(hotel);
            return NoContent();

        }
    }
}
