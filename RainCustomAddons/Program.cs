using RainCustomAddons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainCustomAddons
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creation of the addon
            Addon main = new Addon("ListMenu");

            //Configuration
            main.TextSizeOver = 27;
            main.TextColorHover = "eeeeee";
            main.stringEffect = "BORDER";
            //main.TextFont = "gobold blocky";
            main.TextFont = "lucky first";
            main.TextFont = "MOON GET!";
            main.TextFont = "MUNICH";
            //main.TextFont = "bluefish demo";

            //Insertion of multiple addresses blocs
            main.blocs.Add(new Bloc("SlackTer Pack", new List<string>() { "https://www.facebook.com/", "http://youtube.com/", "https://mail.google.com/mail/u/0/", "https://www.udemy.com/" }));

            //Insertion of the blocs
            main.blocs.Add(new Bloc("Coeur Caillou", "D:/Blizzard/Hearthstone/Hearthstone.exe")); //Warning for the path put / instead of \
            main.blocs.Add(new Bloc("Photoshop", "D:/CC/Adobe Photoshop CC 2018/Photoshop.exe"));
            main.blocs.Add(new Bloc("Gwent", new List<string>() { "D:/GOG Games/Gwent/Gwent.exe", "D:/Things/GwentUp/GwentUp.exe" })); 
            main.blocs.Add(new Bloc("La league", "D:/RITO/LeagueClient.exe"));
            main.blocs.Add(new Bloc("Messy folder", "D:/things"));
            main.blocs.Add(new Bloc("Skyrim", "D:/SteamLibrary/steamapps/common/Skyrim Special Edition/skse64_loader.exe"));
            main.blocs.Add(new Bloc("Tropico", "D:/SteamLibrary/steamapps/common/Tropico 5/Tropico5Steam.exe"));


            //Execution
            Console.WriteLine(ConvertObjectIntoINI(main));

            //uncomment this line if you want to see the output on the console
            //Console.Read();
        }

        static string ConvertObjectIntoINI(Addon main)
        {
            string file = "[Rainmeter]\nUpdate = 1000\nBackgroundMode=1\n\n";

            //Variables 
            file += "[Variables]"
                + "\nFontFace = " + main.TextFont
                + "\nFontSize = " + main.TextSizeDemo
                + "\nFontSizeActive = " + main.TextSizeOver
                + "\nLeft = 700" //Adjust for position
                + "\n#StringEffect= " + main.stringEffect
                + "\nFontEffectColor = 3a3a3a";

            //Optionnal Height and Width
            if (main.Width != null)
                file += "\nWidth = " + main.Width;
            if (main.Height != null)
                file += "\nHeight = " + main.Height;

            foreach(Bloc bloc in main.blocs)
            {
                file += "\n\n; =========== Bloc"+bloc.name+" ===========\n\n";

                string color = (String.IsNullOrEmpty(bloc.fontColor))? main.TextColor : bloc.fontColor;
                string colorActive = (String.IsNullOrEmpty(bloc.fontColorActive))? main.TextColorHover : bloc.fontColorActive;
                string execute = "!Execute";
                string usename = bloc.name.Replace(" ", "_");

                foreach (string ad in bloc.addresses)
                {
                    execute += "[\"" + ad + "\"]";
                }
                //Config bloc passive
                file += "[" + usename + "Passive]";
                file += "\nMeter=STRING"
                    + "\nx =#Left# "
                    + "\ny = 55r"
                    + "\nSolidColor = 0,0,0,1"
                    + "\nText = " + bloc.name
                    + "\nFontFace = " + main.TextFont
                    + "\nFontSize =#FontSize#"
                    + "\nFontColor = " + color
                    + "\nStringAlign = " + main.align.ToString()
                    + "\nStringEffect =#StringEffect#"
                    + "\nFontEffectColor =#FontEffectColor#"
                    + "\nAntiAlias = 1"
                    + "\nMouseOverAction = !Execute[!ShowMeter "+ usename + "Active][!HideMeter "+ usename + "Passive][!Update]\n\n";

                //Confgig bloc active
                file += "[" + usename + "Active]";
                file += "\nMeter=STRING"
                    + "\nx =r "
                    + "\ny =r"
                    + "\nSolidColor = 0,0,0,1"
                    + "\nText = " + bloc.nameActive
                    + "\nFontFace = " + main.TextFont
                    + "\nFontSize =#FontSizeActive#"
                    + "\nFontColor = " + colorActive
                    + "\nStringAlign = " + main.align.ToString()
                    + "\nStringEffect =#StringEffect#"
                    + "\nFontEffectColor =#FontEffectColor#"
                    + "\nAntiAlias = 1 \n Hidden=1"
                    + "\nLeftMouseUpAction = "+execute+ "[!ShowMeter " + usename + "Passive][!HideMeter " + usename + "Active][!Update]"
                    + "\nMouseLeaveAction = !Execute[!ShowMeter " + usename + "Passive][!HideMeter " + usename + "Active][!Update]";
                
            }
            System.IO.File.WriteAllText(@"C:\Users\pgx-j\Documents\Rainmeter\Skins\LeSkin\"+main.Name+".ini", file);
            return file;
        }
    }
}
