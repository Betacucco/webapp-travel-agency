using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult Form(TravelUser data)
        {
            if (!ModelState.IsValid)
            {
                using (TravelContext context = new TravelContext())
                {
                    List<Travel> travelsList = context.Travels.ToList();

                    data.Travel = travelsList;
                }

                return RedirectToAction("GetById", data);
            }

            using(TravelContext context = new TravelContext())
            {
                User newUser = new User();
                newUser.Name = data.Users.Name;
                newUser.Email = data.Users.Email;
                newUser.Message = data.Users.Message;
                newUser.TravelId = data.Users.TravelId;

                context.Users.Add(newUser);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
