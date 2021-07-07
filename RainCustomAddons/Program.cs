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
            main.TextSizeOver = 45;
            main.TextSizeDemo = 40;
            main.TextColor = "e0e0e0";
            main.TextColorHover = "FFF";
            main.gapSize = 60; // Default for 25/30 font is 55.

            main.stringEffect = "BORDER";
            main.TextFont = "Rougant PERSONAL USE ONLY"; //Kiona nice basic one, londoner script

            //Insertion of multiple addresses blocs
            main.blocs.Add(new Bloc("Web Pack", new List<string>() { "https://www.facebook.com/", "http://youtube.com/", "https://mail.google.com/mail/u/0/" }));

            //Insertion of the blocs
            //Warning for the path put / instead of \
            main.blocs.Add(new Bloc("Unreal Engine", "C:/Program Files/Epic Games/UE_4.22/Engine/Binaries/Win64/UE4Editor.exe"));
            main.blocs.Add(new Bloc("Things", "E:/things"));
            main.blocs.Add(new Bloc("Dead By Daylight", "steam://rungameid/381210"));
            main.blocs.Add(new Bloc("Cyberpunk", "E:/things/Cyberpunk 2077"));
            main.blocs.Add(new Bloc("Skyrim", "G:/SteamLibrary/steamapps/common/Skyrim Special Edition/skse64_loader.exe"));
            main.blocs.Add(new Bloc("League of Legends", "Teamfight Tactics", "eeeeee", "B00B69", "G:/Games/LOL/LeagueClient.exe"));
            main.blocs.Add(new Bloc("Visual Studio", "C:/Program Files (x86)/Microsoft Visual Studio/2019/Community/Common7/IDE/devenv.exe"));

            main.sysBlocs.Add(new SysBloc(SysBloc.SysActions.SHUT));
            main.sysBlocs.First().width = 50;
            main.sysBlocs.First().height = 50;

            main.sysBlocs.Add(new SysBloc(SysBloc.SysActions.THIS_PC));
            main.sysBlocs.Add(new SysBloc(SysBloc.SysActions.TASK));
            main.sysBlocs.Add(new SysBloc(SysBloc.SysActions.BIN));


            //Execution
            Console.WriteLine(ConvertObjectIntoINI(main));

            //uncomment this line if you want to see the output on the console
            //Console.Read();
        }

        static string ConvertObjectIntoINI(Addon main)
        {
            string file = "[Rainmeter]\nUpdate = 1000\nBackgroundMode=1\n\n";

            //file+="[MeterButtonShutDown]\nMeter = IMAGE \n X = 63 \n Y = 10 \n W = 20 \n H = 20 \n LeftMouseDownAction =% systemroot %/system32/shutdown.exe - s - t 00";

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

            //--------------------- CONFIG SYS BLOC --------------------//
            int sysblocIterations = 0;
            foreach (SysBloc sb in main.sysBlocs)
            {
                string x = (sysblocIterations == 0) ? "(#Left#-60)r" : "-75r";
                sysblocIterations++;

                file += "\n\n; =========== SysBloc " + sb.name + " ===========\n\n";

                //SysBlocName
                file += "[SysBloc_" + sb.name + "]";
                //Config bloc
                file += "\nMeter=Image"
                    + "\nImageName =" + sb.imglink
                    + "\nx = " + x
                    + "\ny = r"
                    + "\nSolidColor = 0,0,0,0"
                    + "\nDynamicVariables = 1"
                    + "\nW = " + sb.width
                    + "\nH = " + sb.height
                    + "\nAntiAlias = 1"
                    + "\nLeftMouseUpAction = " + sb.sysActionString;
            }

            //--------------------------CONFIG SIMPLE BLOC ---------------//
            foreach (Bloc bloc in main.blocs)
            {
                file += "\n\n; =========== Bloc" + bloc.name + " ===========\n\n";

                string color = (String.IsNullOrEmpty(bloc.fontColor)) ? main.TextColor : bloc.fontColor;
                string colorActive = (String.IsNullOrEmpty(bloc.fontColorActive)) ? main.TextColorHover : bloc.fontColorActive;
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
                    + "\ny = " + main.gapSize + "r"
                    + "\nSolidColor = 0,0,0,1"
                    + "\nText = " + bloc.name
                    + "\nFontFace = " + main.TextFont
                    + "\nFontSize =#FontSize#"
                    + "\nFontColor = " + color
                    + "\nStringAlign = " + main.align.ToString()
                    + "\nStringEffect =#StringEffect#"
                    + "\nFontEffectColor =#FontEffectColor#"
                    + "\nAntiAlias = 1"
                    + "\nMouseOverAction = !Execute[!ShowMeter " + usename + "Active][!HideMeter " + usename + "Passive][!Update]\n\n";

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
                    + "\nLeftMouseUpAction = " + execute + "[!ShowMeter " + usename + "Passive][!HideMeter " + usename + "Active][!Update]"
                    + "\nMouseLeaveAction = !Execute[!ShowMeter " + usename + "Passive][!HideMeter " + usename + "Active][!Update]";

            }
            System.IO.File.WriteAllText(@"C:\Users\Megaport\Documents\Rainmeter\Skins\LeSkin\" + main.Name + ".ini", file);
            return file;
        }
    }
}