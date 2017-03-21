﻿﻿using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using WebApiAsync.Models;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

namespace RefitInternalGenerated
{
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {
#pragma warning disable 0649
        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
#pragma warning restore 0649
    }
}

namespace WebApiAsync.Api
{
    using RefitInternalGenerated;

    [Preserve]
    public partial class AutoGeneratedIGitHubApi : IGitHubApi
    {
        public HttpClient Client { get; protected set; }
        readonly Dictionary<string, Func<HttpClient, object[], object>> methodImpls;

        public AutoGeneratedIGitHubApi(HttpClient client, IRequestBuilder requestBuilder)
        {
            methodImpls = requestBuilder.InterfaceHttpMethods.ToDictionary(k => k, v => requestBuilder.BuildRestResultFuncForMethod(v));
            Client = client;
        }

        public virtual Task<User> GetUser(string user)
        {
            var arguments = new object[] { user };
            return (Task<User>) methodImpls["GetUser"](Client, arguments);
        }

        public virtual Task<List<Repo>> GetRepos(string user)
        {
            var arguments = new object[] { user };
            return (Task<List<Repo>>) methodImpls["GetRepos"](Client, arguments);
        }

    }
}

namespace WebApiAsync.Api
{
    using RefitInternalGenerated;

    [Preserve]
    public partial class AutoGeneratedIGitHubApi : IGitHubApi
    {
        public HttpClient Client { get; protected set; }
        readonly Dictionary<string, Func<HttpClient, object[], object>> methodImpls;

        public AutoGeneratedIGitHubApi(HttpClient client, IRequestBuilder requestBuilder)
        {
            methodImpls = requestBuilder.InterfaceHttpMethods.ToDictionary(k => k, v => requestBuilder.BuildRestResultFuncForMethod(v));
            Client = client;
        }

        public virtual Task<User> GetUser(string user)
        {
            var arguments = new object[] { user };
            return (Task<User>) methodImpls["GetUser"](Client, arguments);
        }

        public virtual Task<List<Repo>> GetRepos(string user)
        {
            var arguments = new object[] { user };
            return (Task<List<Repo>>) methodImpls["GetRepos"](Client, arguments);
        }

    }
}
