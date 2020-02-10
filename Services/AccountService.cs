using System;
using memespace.Models;
using memespace.Repositories;

namespace memespace.Services
{
  public class AccountService
  {
    private readonly AccountRepository _repo;

    public AccountService(AccountRepository repo)
    {
      _repo = repo;
    }

    public User Register(UserRegistration creds)
    {
      User user = new User();
      user.Id = Guid.NewGuid().ToString();
      user.Email = creds.Email;
      user.Username = creds.Username;
      user.FirstName = creds.FirstName;
      user.LastName = creds.LastName;
      user.ImgUrl = creds.ImgUrl;
      user.Hash = BCrypt.Net.BCrypt.HashPassword(creds.Password);

      _repo.Register(user);
      return user;
    }

    public User SignIn(UserSignIn creds)
    {
      User user = _repo.GetUserByEmail(creds.Email);
      if (user == null || !BCrypt.Net.BCrypt.Verify(creds.Password, user.Hash))
      {
        throw new Exception("Invalid Email or Password");
      }
      user.Hash = null;
      return user;
    }

    public User GetUserById(string Id)
    {
      User user = _repo.GetUserById(Id);
      if (user == null) { throw new Exception("Invalid Request"); }
      user.Hash = null;
      return user;
    }

    public User Edit(User info)
    {
      User user = _repo.GetUserById(info.Id);
      if (user == null)
      {
        throw new Exception("Invalid ID");
      }
      user.ImgUrl = info.ImgUrl;
      user.BackDropUrl = info.BackDropUrl;
      user.Bio = info.Bio;
      user.Interests = user.Interests;

      _repo.Edit(user);
      return user;
    }
  }
}