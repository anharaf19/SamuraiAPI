using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Domain
{
    public class Sword
    {
            public  int Id { get; set; }
            public string SwordName { get; set; }    

            public int Weight { get; set; }

            public Samurai Samurai { get; set; }
             
            public int SamuraiId { get; set; }  

            public List<Element> Elements { get; set; }  = new List<Element>();

            public SwordType SwordType { get; set; } = new SwordType();

       
    }
}
