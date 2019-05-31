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
            main.TextSizeOver = 55;
            main.TextSizeDemo = 50;
            main.TextColor = "000000";
            main.TextColorHover = "eeeeee";
            main.gapSize = 70; // Default for 25/30 font is 55.
            
            main.stringEffect = "BORDER";
            main.TextFont = "Londoner";

            //Insertion of multiple addresses blocs
            main.blocs.Add(new Bloc("SlackTer Pack", new List<string>() { "https://www.facebook.com/", "http://youtube.com/", "https://mail.google.com/mail/u/0/", "https://www.udemy.com/" }));

            //Insertion of the blocs
            //Warning for the path put / instead of \
            main.blocs.Add(new Bloc("Unreal Engine", "C:/Program Files/Epic Games/UE_4.22/Engine/Binaries/Win64/UE4Editor.exe"));
            main.blocs.Add(new Bloc("Gwent", "C:/Program Files (x86)/GOG Galaxy/Games/Gwent/Gwent.exe"));
            main.blocs.Add(new Bloc("Things", "E:/things"));
            main.blocs.Add(new Bloc("Skyrim", "G:/SteamLibrary/steamapps/common/Skyrim Special Edition/skse64_loader.exe"));
            main.blocs.Add(new Bloc("Tropico", "G:/SteamLibrary/steamapps/common/Tropico 6/Tropico6.exe"));
            main.blocs.Add(new Bloc("Metro", "com.epicgames.launcher://apps/Snapdragon?action=launch&silent=true"));
            main.blocs.Add(new Bloc("Visual Studio", "C:/Program Files (x86)/Microsoft Visual Studio/2019/Community/Common7/IDE/devenv.exe"));
            main.blocs.Add(new Bloc("Visual Code", new List<string>() { "C:/Users/Megaport/AppData/Local/Programs/Microsoft VS Code/Code.exe", "C:/xampp/xampp-control.exe" }));


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
            System.IO.File.WriteAllText(@"C:\Users\Megaport\Documents\Rainmeter\Skins\LeSkin\" + main.Name+".ini", file);
            return file;
        }
    }
}

/*[NavRecycle] -------------- INCLUDE EXTERNAL FILES TO STAY CLEAN ------------
Meter=Image
ImageName=#@#Images\_Assets\recycle.png
X=(-(#BarHeight#+4))r
Y=r
W=#BarHeight#
H=#BarHeight#
ImageAlpha=(Clamp(([BaseAnimation]-(#SCREENAREAWIDTH#-255)),0,255))
SolidColor=0,0,0,0
DynamicVariables=1
LeftMouseUpAction=[shell:::{645FF040-5081-101B-9F08-00AA002F954E}]#UnloadSkin#
MouseOverAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,128"][!UpdateMeter #CURRENTSECTION#][!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,0"][!UpdateMeter #CURRENTSECTION#][!Redraw]

[GodmodeFolder]
Meter=Image
ImageName=#@#Images\_Assets\gfolder.png
X=(-(#BarHeight#+4))r
Y=r
W=#BarHeight#
H=#BarHeight#
ImageAlpha=(Clamp(([BaseAnimation]-(#SCREENAREAWIDTH#-255)),0,255))
SolidColor=0,0,0,0
DynamicVariables=1
LeftMouseUpAction=["#@#\Includes\godmode.{ED7BA470-8E54-465E-825C-99712043E01C}"]#UnloadSkin#
MouseOverAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,128"][!UpdateMeter #CURRENTSECTION#][!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,0"][!UpdateMeter #CURRENTSECTION#][!Redraw]

[NavThisPC]
Meter=Image
ImageName=#@#Images\_Assets\thispc.png
X=(-(#BarHeight#+4))r
Y=r
W=#BarHeight#
H=#BarHeight#
ImageAlpha=(Clamp(([BaseAnimation]-(#SCREENAREAWIDTH#-255)),0,255))
SolidColor=0,0,0,0
DynamicVariables=1
LeftMouseUpAction=[shell:::{20D04FE0-3AEA-1069-A2D8-08002B30309D}]#UnloadSkin#
MouseOverAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,128"][!UpdateMeter #CURRENTSECTION#][!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,0"][!UpdateMeter #CURRENTSECTION#][!Redraw].[TaskView]
Meter=Image
ImageName=#@#Images\_Assets\taskview.png
X=4R
Y=r
W=#BarHeight#
H=#BarHeight#
ImageAlpha=(Clamp(([BaseAnimation]),0,255))
SolidColor=0,0,0,0
DynamicVariables=1
LeftMouseUpAction=[shell:::{3080F90E-D7AD-11D9-BD98-0000947B0257}]#UnloadSkin#
MouseOverAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,128"][!UpdateMeter #CURRENTSECTION#][!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,0"][!UpdateMeter #CURRENTSECTION#][!Redraw]
[TaskMgr]
Meter=Image
ImageName=#@#Images\_Assets\task.png
X=4R
Y=r
W=#BarHeight#
H=#BarHeight#
ImageAlpha=(Clamp(([BaseAnimation]),0,255))
SolidColor=0,0,0,0
DynamicVariables=1
LeftMouseUpAction=[taskmgr.exe]#UnloadSkin#
MouseOverAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,128"][!UpdateMeter #CURRENTSECTION#][!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# SolidColor "0,0,0,0"][!UpdateMeter #CURRENTSECTION#][!Redraw]

[Time]
Meter=String
MeasureName=mTimeShort
X=((#SCREENAREAWIDTH#)*0.925)
Y=(#BottomBarYPos#+(#BarHeight#/6))
FontFace=#BarFont#
FontSize=#BarFontSize#
FontColor=255,255,255,(Clamp(([BaseAnimation]-(#SCREENAREAWIDTH#-255)),0,255))
StringAlign=Center
DynamicVariables=1
AntiAlias=1


    -------------- BCK ANIM -------------
[LauncherAnimation]
Measure=Calc 
Disabled=1
Formula=(LauncherAnimation-((LauncherAnimation-(#SCREENAREAHEIGHT#/1.5))/#AnimationFactor#))
IfEqualValue=(#SCREENAREAHEIGHT#/1.5)-1
IfEqualAction=[!PauseMeasure #CURRENTSECTION#]
DynamicVariables=1

[Group1Launcher1Background]
Meter=Image
ImageName=#@#Images\_Assets\empty.png
X=#XPos10#
Y=#LauncherYPos#
W=#LauncherWidth#
H=(Clamp(([LauncherAnimation]),0,#LauncherHeight#))
ImageAlpha=255
DynamicVariables=1
LeftMouseUpAction=#Group1Launcher1Action##UnloadSkin#
MouseOverAction=[!SetOption #CURRENTSECTION# ImageName "#@#Images\Mouseover\#Group1Launcher1BG#][!UpdateMeter #CURRENTSECTION#]#Indicator1On#[!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# ImageName "#@#Images\_Assets\empty.png"][!UpdateMeter #CURRENTSECTION#]#Indicator1Off#[!Redraw]

[Group1Launcher1Icon]
Meter=Image
ImageName=#@#Images\Icons\#Group1Launcher1Icon#
X=#LauncherIconAndNameXpos#r
Y=(Clamp(([LauncherAnimation]-(#SCREENAREAHEIGHT#*0.12)),(((#SCREENAREAHEIGHT#)-#LauncherHeight#)/1.8),#LauncherIconYpos#))
W=#LauncherIconSize#
H=#LauncherIconSize#
ImageAlpha=([LauncherAnimation])-((#SCREENAREAHEIGHT#/1.5)-275)
DynamicVariables=1

[Group1Launcher1Name]
Meter=String
X=#LauncherIconAndNameXpos#r
Y=#LauncherNameYpos#r
FontFace=#LauncherFont#
FontSize=#LauncherFontSize#
FontColor=255,255,255,(Clamp(([LauncherAnimation])-((#SCREENAREAHEIGHT#/1.5)-275),0,255))
StringAlign=Center
Text=#Group1Launcher1Name#
DynamicVariables=1
AntiAlias=1
*/
