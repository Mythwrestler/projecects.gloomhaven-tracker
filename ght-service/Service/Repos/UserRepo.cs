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
using GloomhavenTracker.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace GloomhavenTracker.Service.Repos;


public interface UserRepo
{
    public User GetCurrentUser();
    public User GetUserById(Guid userId);
}

public class UserRepoImplementation : UserRepo
{
    private readonly GloomhavenContext dbContext;
    private readonly IMapper mapper;
    private readonly string baseAuthURL;
    private readonly IHttpContextAccessor httpContextAccessor;
    private UserDAO? currentUserDAO;

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

    public User GetCurrentUser()
    {
        if(currentUserDAO is null) GetCurrentUserDAO();
        return mapper.Map<User>(currentUserDAO);
    }

    public User GetUserById(Guid userId)
    {
        UserDAO? user = GetUserDAO(userId);
        if(user == null)
            throw new ArgumentException("user not found");
        return mapper.Map<User>(user);
    }


    private UserDAO? GetUserDAO(Guid userId)
    {
        return dbContext.User.Find(userId);
    }

    private void GetCurrentUserDAO()
    {

        UserIdentity? userIdentity = null;
        using(var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            StringValues authQueryParam;
            string authHeaderValue;
            if(httpContext.Request.Query.TryGetValue("access_token", out authQueryParam))
                authHeaderValue = $"Bearer {authQueryParam.ToString()}";
            else
                authHeaderValue = httpContext.Request.Headers.Authorization.ToString();

            client.DefaultRequestHeaders.Add("Authorization", authHeaderValue);

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
                UserId = userIdentity.UserId
            };
            dbContext.Add(userDAO);
        }
        userDAO.UserName = userIdentity.UserName;
        userDAO.FirstName = userIdentity.FirstName;
        userDAO.LastName = userIdentity.LastName;
        userDAO.Email = userIdentity.Email;

        dbContext.SaveChanges();
        
        this.currentUserDAO = userDAO;
    }


    [Serializable]
    private class UserIdentity
    {
        [JsonPropertyName("sub")]
        public Guid UserId { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}
