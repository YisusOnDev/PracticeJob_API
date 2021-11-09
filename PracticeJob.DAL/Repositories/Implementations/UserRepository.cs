using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public TestApiBeContext _context { get; set; }
        public UserRepository(TestApiBeContext context)
        {
            this._context = context;
        }
        public bool Login(User user)
        {
            /* var example_join = (from u in _context.Users
                        join u2 in _context.Users on u.Id equals u2.Id
                        where u.Email == user.Email && u.Password == user.Password
                        select new { u, u2 }).SingleOrDefault(); */

            // var lambdalong = _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).SingleOrDefault();
            return _context.Users.Any(u => u.Email == user.Email && u.Password == user.Password);
        }

        public User Create(User user)
        {
            var u = _context.Users.Add(user);
            _context.SaveChanges();
            return u.Entity;
        }

        public bool Exists(User user)
        {
            return _context.Users.Any(u => u.Email == user.Email);
        }
    }
}
