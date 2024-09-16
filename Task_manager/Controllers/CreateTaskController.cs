using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_manager.Controllers
{
    public class CreateTaskController : Controller
    {
        public ActionResult CreateTask()
        {
            return View();
        }

        // GET: CreateTaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateTaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateTaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CreateTaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreateTaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CreateTaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreateTaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
