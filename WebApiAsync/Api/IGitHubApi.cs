

using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using WebApiAsync.Models;

namespace WebApiAsync.Api
{
    public interface IGitHubApi
    {
        [Get("/users/{user}")]
        Task<User> GetUser(string user);


        [Get("/users/{user}/repos")]
        Task<List<Repo>> GetRepos(string user);
    }
}