using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApiAsync.Models;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;

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

        public IObservable<User> GetUserObservable(string username){
            return _client.GetModelObservable<User>($"{BaseUrl}/users/{username}");
        }

        public IObservable<List<Repo>> GetReposObservable(string username){
            return _client.GetModelObservable<List<Repo>>($"{BaseUrl}/users/{username}/repos");
        }
    }

    public static class HttpClientExteiontions
    {

        private static readonly Dictionary<string,string> _cache = new Dictionary<string, string>();


        public static IObservable<T> GetModelFromCache<T>(this HttpClient client, string url, bool throwErrorOnFailure = false){

            var cacheKey = url;
            string data;
            var inCache = _cache.TryGetValue(cacheKey, out data);

            return Observable.Create<string>(o => {
                var cancel = new CancellationDisposable();

                if(inCache){
                    o.OnNext(data);
                }

                client.GetStringAsync(url).ToObservable<String>().Subscribe( next => {
                    _cache.Add(cacheKey, next);
                    o.OnNext(next);
                    o.OnCompleted();
                }, error => {
                    if( !inCache || throwErrorOnFailure){
                        o.OnError(error);
                    }else{
                        o.OnCompleted();
                    }
                });

                return cancel;
            })
            .Select( stringData => JsonConvert.DeserializeObject<T>(stringData));
        }

        public static IObservable<T> GetModelObservable<T>(this HttpClient client, string url){
            var observable = client.GetStringAsync(url).ToObservable<String>();
            return observable.Select( x => JsonConvert.DeserializeObject<T>(x));
        }
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