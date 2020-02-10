using System;
using System.Data;
using Dapper;
using memespace.Models;

namespace memespace.Repositories
{
  public class AccountRepository
  {
    IDbConnection _db;
    public AccountRepository(IDbConnection db)
    {
      _db = db;
    }

    //REGISTER
    public void Register(User user)
    {
      //generate the user id
      //HASH THE PASSWORD
      string sql = @"
                INSERT INTO users 
                (id, username, firstname, lastname, email, hash, imgUrl)
                VALUES 
                (@id, @username, @firstname, @lastname, @email, @Hash, @imgurl)";
      _db.Execute(sql, user);
    }

    internal User GetUserByEmail(string email)
    {
      string sql = "SELECT * FROM users WHERE email = @email";
      return _db.QueryFirstOrDefault<User>(sql, new { email });
    }

    internal User GetUserById(string id)
    {
      string sql = "SELECT * FROM users WHERE id = @id";
      return _db.QueryFirstOrDefault<User>(sql, new { id });
    }

    public void Edit(User user)
    {
      string sql = @"
        UPDATE user
        SET
          imgUrl = @ImgUrl,
          backDropUrl = @BackDropUrl,
          bio = @Bio,
          interests = @Interests
        WHERE id = @Id";
      _db.Execute(sql, user);
    }
  }
}