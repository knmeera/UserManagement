using System.Collections.Generic;
using Routing.Models.Identity;
namespace IdentityConsoleApplication.Identity
{
    public interface IUser
    {
        List<User> GetAll();
        User GetById(long key);
        User GetByUsername(string key);
        void Add(User user);
        void Update(User user);
        void Remove(User user);
       // bool Login(string username, string password);
        long Login(string username, string password);

       //List<User> GenerateDummyUsers();

    }
}
