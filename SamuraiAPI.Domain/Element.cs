using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Domain
{
    public class Element
    {
        public int Id { get; set; }
        public string ElementName { get; set; }
        public List<Sword> Swords { get; set; }  = new List<Sword>();
    }
}
