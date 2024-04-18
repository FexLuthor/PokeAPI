using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeView
{
    public class PokeModel
    {
        public bool OnlineSuccess = false;
        public string? Name { get; set; } //Fragezeichen markiert variables als nullable

        public List<string>? Ability { get; set; } //gibt mehrere daher Liste
        public List<string>? Type { get; set; }

        public string? PictureUrl { get; set; }
        public string? ShinyUrl { get; set; }

        public PokeModel() // Konstruktor - sonst gabs Probleme ... wofür war dann eig get / set?!?!?!
        {
            Ability = new List<string>();
            Type = new List<string>();
        }
    }
}
