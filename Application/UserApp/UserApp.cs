using Domain.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp
{
    public class UserApp : IUserApp
    {
        private readonly DataContext _dataContext;

        public UserApp(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<User> GetUsers()
        {
            return _dataContext.Users;
        }
    }
}
