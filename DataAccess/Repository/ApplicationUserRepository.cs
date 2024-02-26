using BookyStore.DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
        }

        public void Update(ApplicationUser user)
        {
            _db.ApplicationUsers.Update(user); 
        }
    }
}
