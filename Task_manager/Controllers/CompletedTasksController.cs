using Microsoft.AspNetCore.Mvc;
using Task_manager.Service.Interfaces;

namespace Task_manager.Controllers
{
    public class CompletedTasksController(ITaskService taskService) : Controller
    {
        private readonly ITaskService _taskService = taskService;

        public ActionResult CompletedTasks()
        {
            return View();
        }

        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = await _taskService.GetCompletedTasks();
            return Json(new { data = result.Data });
        }














        // GET: CompletedTasksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompletedTasksController/Create
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

        // GET: CompletedTasksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompletedTasksController/Edit/5
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

        // GET: CompletedTasksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompletedTasksController/Delete/5
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
