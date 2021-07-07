using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainCustomAddons.Entities
{
    class Margin
    {
        public string text { get; set; }
        public string shortText { get; set; }

        public List<int> pixels;

            public Margin(string text, string shortText)
        {
            this.text = text;
            this.shortText = shortText;
            this.pixels = new List<int> { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
        }
    }
}
