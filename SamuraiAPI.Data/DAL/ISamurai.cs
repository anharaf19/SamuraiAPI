using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data.DAL
{
    public interface ISamurai : ICrud<Samurai>
    {
        Task<IEnumerable<Samurai>> GetByName(string name);

        Task<Samurai> AddSamuraiWithSword(Samurai obj);

        Task<IEnumerable<Samurai>> GetAllSamurai();


    }
}
