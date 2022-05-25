using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;

namespace TravelAgency.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string? searchString)
        {
            List<Travel> travels = new List<Travel>();

            using (TravelContext context = new TravelContext())
            {
                //Controlliamo che la stringa non sia nulla e vuota
                if (searchString != null && searchString != "")
                {
                    //se non lo e' vuol dire che e' stato inserito qualcosa e
                    //ricerchiamo quali viaggi contengono la stringa inserita
                    //nel titolo o nella descrizione
                    travels = context.Travels
                        .Where(travel => travel.Title.Contains(searchString) ||
                        travel.Description.Contains(searchString))
                        .ToList<Travel>();
                }
                else
                {
                    //altrimenti restituiamo la lista
                    travels = context.Travels.ToList<Travel>();
                }
            }

            return Ok(travels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Travel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {

            using (TravelContext context = new TravelContext())
            {
                Travel? travelToFound = context.Travels
                    .Where(travel => travel.Id == id)
                    .FirstOrDefault();

                if (travelToFound == null)
                    return NotFound();
                else
                    return Ok(travelToFound);
            }
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            using (TravelContext context = new TravelContext())
            {

                    User userToAdd = new User();
                    userToAdd.Name = model.Name;
                    userToAdd.Email = model.Email;
                    userToAdd.Message = model.Message;
                    userToAdd.TravelId = model.TravelId;    

                    context.Add(userToAdd);
                    context.SaveChanges();
                    return Ok();                
            }

        }
    }
}

