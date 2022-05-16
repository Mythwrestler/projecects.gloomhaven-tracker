using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using GloomhavenTracker.Database;
using GloomhavenTracker.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace GloomhavenTracker.Service.Repos;


public interface UserRepo
{
    public Guid GetUserId();
    public string GetUserName();
}

public class UserRepoImplementation : UserRepo
{
    private readonly GloomhavenContext dbContext;
    private readonly IMapper mapper;
    private readonly string baseAuthURL;
    private readonly IHttpContextAccessor httpContextAccessor;

    private UserIdentity? user;

    private HttpContext httpContext
    {
        get
        {
            if(httpContextAccessor.HttpContext is null) throw new ArgumentNullException("No request context available");
            return httpContextAccessor.HttpContext;
        }
    }

    public UserRepoImplementation(GloomhavenContext context, IMapper mapper, string baseAuthURL, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = context;
        this.mapper = mapper;
        this.baseAuthURL = baseAuthURL;
        this.httpContextAccessor = httpContextAccessor;
    }

    private void GetUser()
    {
        UserIdentity? userIdentity = null;
        using(var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            client.DefaultRequestHeaders.Add("Authorization", httpContext.Request.Headers.Authorization.ToString());

            Task.Run(async () => {
                var response = await client.GetAsync($"{baseAuthURL}/protocol/openid-connect/userinfo");
                if(response is not null && response.StatusCode == HttpStatusCode.OK)
                {
                    var userString = await response.Content.ReadAsStringAsync();
                    userIdentity = JsonSerializer.Deserialize<UserIdentity>(userString);
                }

            }).Wait();
        }

        if(userIdentity is null) throw new ArgumentNullException("Could Not retrieve Identity Data");
        UserDAO? userDAO = dbContext.User.FirstOrDefault(user => user.UserId == userIdentity.UserId);
        if(userDAO is null) {
            userDAO = new UserDAO()
            {
                UserId = userIdentity.UserId,
            };
        }




    }

    public Guid GetUserId()
    {
        if(user is null) GetUser();

        if(user is null) throw new ArgumentException("Could Not Retrieve user Information");
        
        return user.UserId;
    }

    public string GetUserName()
    {
        if(user is null) GetUser();

        if(user is null) throw new ArgumentException("Could Not Retrieve user Information");
        
        return user.UserName;
    }

    [Serializable]
    private class UserIdentity
    {
        [JsonPropertyName("sub")]
        public Guid UserId { get; set; }

        [JsonPropertyName("preferred_username")]
        public string UserName { get; set; } = string.Empty;
    }


}
