using System;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface UserService
{
    public User GetCurrentUser();
    public User GetUserById(Guid userId);
}

public class UserServiceImplementation : UserService
{
    private readonly UserRepo userRepo;

    public UserServiceImplementation(UserRepo userRepo)
    {
        this.userRepo = userRepo;
    }

    public User GetCurrentUser()
    {
        return userRepo.GetCurrentUser();
    }

    public User GetUserById(Guid userId)
    {
        return userRepo.GetUserById(userId);
    }

}