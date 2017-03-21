using System;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApiAsync.Controllers
{



    [Route("api/[controller]")]
    public class AsyncController : Controller
    {

        [HttpGet("parallel")]
        public async Task<JsonResult> GetParallel()
        {

            Task<int> task = longRunningTask(2);
            Task<int> task2 = longRunningTask(3);

            int result = method();
            int taskResult = await task;
            int task2Result = await task2;

            return Json(new { taskResult  = taskResult, result = result });
        }

        // [HttpGet("multithread")]
        // public JsonResult GetMultithread()
        // {

        //     Task<int> task = Task.Run(() => longRunningTask(4));
        //     Task<int> task2 = Task.Run(() => longRunningTask(6));
        //     int result = method();
        //     int taskResult = task.Result;
        //     int task2Result = task2.Result;
            

        //     stopwatch.Stop();
        //     return Json(new { task  = taskResult, task2 = task2Result, result = result });
        // }

        [HttpGet("multithread")]
        public async Task<JsonResult> GetMultithread()
        {
            Task<int> task = Task.Run(() => longRunningTask(2));
            Task<int> task2 = Task.Run(() => longRunningTask(3));
            int result = method();
            int taskResult = await task;
            int task2Result = await task2;
            
            return Json(new { task  = taskResult, task2 = task2Result, result = result });
        }

        [HttpGet("sequential")]
        public async Task<JsonResult> GetSequental()
        {
            int taskResult = await longRunningTask(2);
            int task2Result = await longRunningTask(3);
            int result = method();

            return Json(new { taskResult  = taskResult, result = result });
        }

        private int method()
        {
            Console.WriteLine("method");
            return Thread.CurrentThread.ManagedThreadId;
        }

        private async Task<int> longRunningTask(int delaySeconds)
        {
            Console.WriteLine("task start");
            await Task.Delay(delaySeconds * 1000);
            Console.WriteLine("task done");
            return Thread.CurrentThread.ManagedThreadId;
        }
    }
}