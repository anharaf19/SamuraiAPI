using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data.DAL
{
    public interface IElement : ICrud<Element>
    {
        Task<IEnumerable<Element>> GetByName(string name);
    }
}
