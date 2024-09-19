using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_manager.Domain;
using Task_manager.Models;
using Task_manager.Service.Interfaces;

namespace Task_manager.Controllers
{
    public class TaskListController(ITaskService taskService) : Controller
    {
        private readonly ITaskService _taskService = taskService;
        
        public ActionResult TaskList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TaskHandler(TaskFilter filter)
        {
            var response = await _taskService.GetTasks(filter);
            return Json(new { recordsFiltered = response.Total, recordsTotal = response.Total, data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> EndTask(long id)
        {
            var respone = await _taskService.EndTask(id);
            if (respone.StatusCode == Enum.StatusCode.OK)
            {
                return Ok(new { description = respone.Description });

            }
            return BadRequest(new { description = respone.Description });
        }

        [HttpGet]
        public async Task<IActionResult> GetTask(long id)
        {
            var respone = await _taskService.GetTask(id);
            return PartialView(respone.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask( TaskVievModel model)
        {
            var response = await _taskService.Update(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(TaskVievModel model)
        {
            var response = await _taskService.Delete(model);
            return Json(response);
        }












        // GET: TaskListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskListController/Create
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

        // GET: TaskListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskListController/Edit/5
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

        // GET: TaskListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskListController/Delete/5
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
