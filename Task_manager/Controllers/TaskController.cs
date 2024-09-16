using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_manager.Models;
using Task_manager.Service.Interfaces;
using Task_manager.Service.Implementations;
using Task_manager.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;


namespace Task_manager.Controllers
{
    public class TaskController(ITaskService taskService) : Controller
    {
        private readonly ITaskService _taskService = taskService;

        [HttpGet]
        public IActionResult Index() => View();

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
                
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskVievModel model)
        {
            var respone = await _taskService.Create(model);
            if (respone.StatusCode == Enum.StatusCode.OK)
            {
                
                return Ok(new { description = respone.Description });

            }
            return BadRequest(new { description = respone.Description });
        }

        [HttpPost]
        public async Task<IActionResult> TaskHandler(TaskFilter filter)
        {
            var response = await _taskService.GetTasks(filter);
            return Json(new { recordsFiltered = response.Total, recordsTotal = response.Total, data = response.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = await _taskService.GetCompletedTasks();
            return Json(new { data = result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GetTask(long id)
        {
            var respone = await _taskService.GetTask(id);
            return PartialView(respone.Data);
        }







        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
