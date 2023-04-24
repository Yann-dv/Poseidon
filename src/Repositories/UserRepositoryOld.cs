using PoseidonApi.Data;
using System.Linq;
using PoseidonApi.Domain;
using System;
using System.Collections.ObjectModel;

namespace PoseidonApi.Repositories
{
    // TO DELETE
    /*public class UserRepository
    {
        public LocalDbContext DbContext { get; }

        public UserRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public UserOld FindByUserName(string userName)
        {
            return DbContext.Users.Where(user => user.UserName == userName)
                                  .FirstOrDefault();
        }

        public UserOld[] FindAll()
        {
            return DbContext.Users.ToArray();
        }

        public void Add(UserOld userOld)
        {
        }

        public UserOld FindById(int id)
        {
            return null;
        }
    }*/
}