using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainCustomAddons.Entities
{
    class Addon
    {
        //Enum

        public enum Align
        {
            LEFT = 1,
            CENTER = 2,
            RIGHT = 3
        }

        const string defaultFont = "bluefish demo";
        const string defaultTextColor = "255,255,255,250";
        const int defaultTextSize = 25;
        const Align defaultAlign = Align.RIGHT;
        const string defaultEffect = "SHADOW";
        const int defaultGapSize = 55;

        //Variables 
        public string Name { get; set; }
        public int Blur { get; set; }
        public string TextColor { get; set; }
        public string TextColorHover { get; set; }
        public string TextFont { get; set; }
        public int TextSizeDemo { get; set; }
        public int TextSizeOver { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public List<Bloc> blocs { get; set; }
        public Align align { get; set; }
        //https://docs.rainmeter.net/manual/meters/string/inline/
        public string stringEffect { get; set; }
        public int gapSize { get; set; }

        //Constructors
        public Addon(string name)
        {
            this.Name = name;
            this.Blur = 1;
            this.TextColor = defaultTextColor;
            this.TextColorHover = defaultTextColor;
            this.TextFont = defaultFont;
            this.TextSizeDemo = defaultTextSize;
            this.TextSizeOver = defaultTextSize;
            this.Width = null;
            this.Height = null;
            this.blocs = new List<Bloc>();
            this.align = defaultAlign;
            this.stringEffect = defaultEffect;
            this.gapSize = defaultGapSize;
        }

        public Addon(string name, string color, string colorHover, string font, int textSize, int textSizeHover, int Width, int Height, Align align, string stringEffect)
        {
            this.Name = name;
            this.Blur = 1;
            this.TextColor = color;
            this.TextColorHover = colorHover;
            this.TextFont =(string.IsNullOrEmpty(font))?defaultFont: font;
            this.TextSizeDemo = defaultTextSize;
            this.TextSizeOver = defaultTextSize;
            this.Width = Width;
            this.Height = Height;
            this.blocs = new List<Bloc>();
            this.align = align;
            this.stringEffect = stringEffect;
            this.gapSize = defaultGapSize;
        }
    }
}
