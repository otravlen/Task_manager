using Microsoft.AspNetCore.Mvc;
using Task_manager.Models;
using Task_manager.Service.Interfaces;

namespace Task_manager.Controllers
{
    public class CreateNewTaskController(ITaskService taskService) : Controller
    {
        private readonly ITaskService _taskService = taskService;



        [HttpPost]
        public async Task<IActionResult> CreateNewTask(CreateTaskVievModel model)
        {
            var respone = await _taskService.Create(model);
            if (respone.StatusCode == Enum.StatusCode.OK)
            {

                return Ok(new { description = respone.Description });

            }
            return BadRequest(new { description = respone.Description });
        }

        public ActionResult CreateTaskView()
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
