using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WebApiAsync.Api;
using WebApiAsync.Models;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Reactive.Concurrency;

namespace WebApiAsync.Controllers
{


    [Route("api/[controller]")]
    public class GithubController : Controller
    {

        //private readonly IGitHubApi _gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");

        private readonly GitHubHttpClient _gitHubApi = new GitHubHttpClient();


        // GET api/values
        [HttpGet("users/{user}")]
        public async Task<JsonResult> GetUser(string user)
        {
            return Json(await _gitHubApi.GetUserAsync(user));
        }


        // GET api/values
        [HttpGet("users/{user}/repos")]
        public async Task<JsonResult> GetUserAndRepos(string user)
        {

            Task<User> userTask = _gitHubApi.GetUserAsync(user);
            Task<List<Repo>> repoTask = _gitHubApi.GetReposAsync(user);

            User githubUser = await userTask;
            List<Repo> repos = await repoTask;

            foreach (var repo in repos)
            {
                repo.Owner = githubUser;
            }

            return Json(repos);
        }

        // GET api/values
        [HttpGet("users/{user}/repos/reactive")]
        public async Task<JsonResult> ReactiveGetRepos(string user)
        {
            return Json(await GetUserAndReposObservalbe(user).ToTask());
        }

        private IObservable<List<Repo>> GetUserAndReposObservalbe(string user)
        {
            
            return _gitHubApi
            .GetUserObservable(user)
            .SelectMany(githubUser => _gitHubApi
                .GetReposObservable(user)
                .SelectMany(repos =>
                {
                    Console.WriteLine($"running GetUserAndReposObservalbe() on thread {Thread.CurrentThread.ManagedThreadId}");
                    foreach (var repo in repos)
                    {
                        repo.Owner = githubUser;
                    }
                    return Observable.Return(repos);
                }));
        }


        //http://localhost:5000/api/github/users/?users=starrepublic&users=josipmirkovic&users=lohmander&users=sorting

        [HttpGet("users/")]
        public async Task<JsonResult> ReactiveGetMultipleRepos(string[] users){
            return Json(await Observable
            .Zip(
                users.ToObservable()
                .SelectMany(username => GetUserAndReposObservalbe(username))
            .SubscribeOn(NewThreadScheduler.Default))
            .ObserveOn(Scheduler.Default)
            .ToTask());
        }
    }
}