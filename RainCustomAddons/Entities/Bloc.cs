using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainCustomAddons.Entities
{
    class Bloc
    {
        //Enum
        public enum LinkType
        {
            FOLDER = 1,
            EXE = 2,
            URL = 3
        }
        
        //Variables 
        public string name { get; set; }
        public LinkType type { get; set; }
        public string nameActive { get; set; }
        public string fontColor { get; set; }
        public string fontColorActive { get; set; }
        public List<string> addresses { get; set; }

        public Bloc(string name, string address)
        {
            this.addresses = new List<string>() { address };
            this.name = name;
            this.nameActive = name;
        }
        public Bloc(string name, List<string> addresses)
        {
            this.addresses = addresses;
            this.name = name;
            this.nameActive = name;
        }
        public Bloc(string name, string nameActive, string color, string colorActive,  string address)
        {
            this.addresses = new List<string>() { address };
            this.name = name;
            this.nameActive = nameActive;
            this.fontColor = color;
            this.fontColorActive = colorActive;
        }
    }
}
