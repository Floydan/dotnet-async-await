using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApiAsync.Models;

namespace WebApiAsync.Api
{


    public class GitHubHttpClient{

        private const string BaseUrl = "https://api.github.com";
        private readonly HttpClient _client = new HttpClient();

        public GitHubHttpClient(){
            _client.DefaultRequestHeaders.Add("User-Agent", "Awesome-Octocat-App");
        }
        
        public Task<User> GetUserAsync(string username){
               return _client.GetModelAsync<User>($"{BaseUrl}/users/{username}");
        }

        public Task<List<Repo>> GetReposAsync(string username){
               return _client.GetModelAsyncBad<List<Repo>>($"{BaseUrl}/users/{username}/repos");
        }
    }

    public static class HttpClientExteiontions
    {
        public static Task<T> GetModelAsync<T>(this HttpClient client, string url)
        {
            
            var jsonTask = client.GetStringAsync(url);
            return jsonTask.ContinueWith(json => JsonConvert.DeserializeObject<T>(json.Result));
        }

        public static async Task<T> GetModelAsyncBad<T>(this HttpClient client, string url)
        {
            var json = await client.GetStringAsync(url);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
        }
    }   

}