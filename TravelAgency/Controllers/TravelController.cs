using Microsoft.AspNetCore.Mvc;
using TravelAgency.Data;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class TravelController : Controller
    {
        //Invia una richiesta al Server, che restituisce una lista
        //presa dal DB alla vista AdminPage
        [HttpGet]
        public IActionResult Index()
        {
            List<Travel> travels = new List<Travel>();

            using (TravelContext context = new TravelContext())
            {
                travels = context.Travels.ToList<Travel>();
            }

            return View("AdminPage", travels);
        }

        //Invia una richiesta al server, passando l'id come parametro,
        //dove in base al primo Id nel DB uguale a quello passato
        //ritorna la vista Details con il viaggio uguale a quell'id
        [HttpGet]
        public IActionResult Details(int id)
        {
            using (TravelContext context = new TravelContext())
            {
                Travel? travelToFound = context.Travels
                    .Where(travel => travel.Id == id)
                    .FirstOrDefault();

                if (travelToFound == null)
                    return NotFound();
                else
                    return View("Details", travelToFound);
            }
        }

        //Invia una richiesta al server che restituisce la vista Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        //Con il Post inviamo dati dalle View ai Controller.
        //Infatti nella view Create verra' creato un nuovo oggetto di tipo Travel
        //che se non e' valido ritorna la pagina con gli errori da correggere, se
        //invece e' valido lo aggiungiamo al DB e ritorniamo all'azione INDEX.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Travel newTravel)
        {
            if (!ModelState.IsValid)
                return View("Create", newTravel);

            using (TravelContext context = new TravelContext())
            {
                Travel travelToCreate = new Travel(newTravel.Title, newTravel.Description, newTravel.Image, newTravel.Price);
                context.Travels.Add(travelToCreate);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //Chiediamo al server di modificare un elemento grazie alla ricerca tramite
        //Id, se lo trova si viene reinderizzati alla view di Update dove si puo'
        //modificare il viaggio, altrimenti restituisce uno StatusCode404
        [HttpGet]
        public IActionResult Update(int id)
        {
            Travel? travelToUpdate = null;

            using (TravelContext context = new TravelContext())
            {
                travelToUpdate = context.Travels
                    .Where(travel => travel.Id == id)
                    .FirstOrDefault();

                if (travelToUpdate != null)
                    return View("Update", travelToUpdate);
                else
                    return NotFound();
            }
        }

        //La view chiede al controller se le modifiche sono valide, se non lo sono
        //restituisce la stessa view con gli errori da correggere, altrimenti ,tramite id,
        //restituira' il parametro modificato all'azione INDEX, oppure un errore 404.
        [HttpPost]
        public IActionResult Update(int id, Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", travel);
            }

            Travel? travelToUpdate = null;

            using (TravelContext context = new TravelContext())
            {
                travelToUpdate = context.Travels
                    .Where(travel => travel.Id == id)
                    .FirstOrDefault();

                if (travelToUpdate != null)
                {
                    travelToUpdate.Title = travel.Title;
                    travelToUpdate.Description = travel.Description;
                    travelToUpdate.Image = travel.Image;
                    travelToUpdate.Price = travel.Price;

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        //Viene inviata una richiesta dalla View al controller che chiede se quell'id
        //puo' essere eliminato, se viene eliminato restituira' l'azione INDEX senza
        //l'elemento, altrimenti errore 404.
        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (TravelContext context = new TravelContext())
            {
                Travel? travelToDelete = context.Travels
                    .Where(travel => travel.Id == id)
                    .FirstOrDefault();

                if (travelToDelete != null)
                {
                    context.Travels.Remove(travelToDelete);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }


    }
}
