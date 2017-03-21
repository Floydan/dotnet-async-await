using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WebApiAsync.Api;
using WebApiAsync.Models;

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

    }
}