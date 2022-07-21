using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data.DAL
{
    public interface ISword : ICrud<Sword>
    {
        Task<IEnumerable<Sword>> GetByName(string name);

        Task<Sword> AddSwordWithType(Sword sword);
    }
}
