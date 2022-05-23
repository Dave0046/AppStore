using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp
{
    public interface IUserApp
    {
        IQueryable<User> GetUsers();
    }
}
