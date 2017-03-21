using System;
using Newtonsoft.Json;

namespace WebApiAsync.Models
{
    public class User : Owner
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("blog")]
        public object Blog { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("hireable")]
        public object Hireable { get; set; }

        [JsonProperty("bio")]
        public object Bio { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }

        [JsonProperty("public_gists")]
        public int PublicGists { get; set; }

        [JsonProperty("followers")]
        public int Followers { get; set; }

        [JsonProperty("following")]
        public int Following { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}