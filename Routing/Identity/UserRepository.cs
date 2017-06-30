using System;
using System.Collections.Generic;
using System.Linq;
using Routing.Models.Identity;
namespace IdentityConsoleApplication.Identity
{
    class UserRepository : IUser
    {
 
        public long Login(string username, string password)
        {
            using (var _dbContext = new IdentityManagementEntities())
            {
                var user = _dbContext.Users.Where(t => t.UserName == username && t.Password == password && t.IsEnabled == true && t.EmailActive == true).SingleOrDefault();
                if (user != null)
                    return user.ID;

                return -1;
            }
 
         }


        public List<User> GetAll()
        {
            using (var _dbContext = new IdentityManagementEntities())
            {
                var users = _dbContext.Users.ToList();
                return users;
            }
         }

        public User GetById(long key)
        {
            using (var _dbContext = new IdentityManagementEntities())
            {
                var user = _dbContext.Users.FirstOrDefault(t => t.ID.Equals(key));
                return user;
            }
         }

        public User GetByUsername(string key)
        {
            using (var _dbContext = new IdentityManagementEntities())
            {
                var user = _dbContext.Users.FirstOrDefault(t => t.UserName.Equals(key));
                return user;
            }
        }

        public void Add(User user)
        {
            //Add to Database
           try
            {
                using (var _dbContext = new IdentityManagementEntities())
                {
                    _dbContext.Users.Add(user);
                    //_dbContext.Entry(user).State = System.Data.EntityState.Added;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                 
            }
        }

        public void Update(User user)
        {
            //save modified entity using new Context
            using (var _dbContext = new IdentityManagementEntities())
            {
                _dbContext.Users.Attach(user);
                _dbContext.Entry(user).State = System.Data.EntityState.Modified;
                _dbContext.SaveChanges();
            }

        }

        public void Remove(User user)
        {
            using (var _dbContext = new IdentityManagementEntities())
            {
                _dbContext.Users.Attach(user);
                _dbContext.Entry(user).State = System.Data.EntityState.Deleted;
                _dbContext.SaveChanges();
            }

        }
    }
 }

