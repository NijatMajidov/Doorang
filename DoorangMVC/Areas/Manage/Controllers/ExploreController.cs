using DoorangMVC.Business.Exceptions;
using DoorangMVC.Business.Services.Abstract;
using DoorangMVC.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoorangMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ExploreController : Controller
    {
        private readonly IExploreService _exploreService;
        public ExploreController(IExploreService exploreService)
        {
            _exploreService = exploreService;
        }
        public IActionResult Index()
        {
            return View(_exploreService.GetAllExplores());
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Explore explore)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _exploreService.CreateExplore(explore);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var existexplore = _exploreService.GetExplore(x => x.Id == id);

            if (existexplore == null)
                return NotFound();

            return View(existexplore);
        }

        [HttpPost]
        public IActionResult DeleteExplore(int id)
        {
            var existexplore = _exploreService.GetExplore(x => x.Id == id);

            if (existexplore == null)
                return NotFound();
            try
            {
                _exploreService.DeleteExplore(id);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existexplore = _exploreService.GetExplore(x => x.Id == id);

            if (existexplore == null)
                return NotFound();

            return View(existexplore);
        }
        [HttpPost]
        public IActionResult Update(Explore explore)
        {
            if(!ModelState.IsValid) return View();

            _exploreService.UpdateExplore(explore.Id, explore);
            return RedirectToAction("Index");
        }
    }
}
