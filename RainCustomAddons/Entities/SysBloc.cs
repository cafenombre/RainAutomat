using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainCustomAddons.Entities
{
    class SysBloc
    {

        //Enum
        public enum SysActions
        {
            OTHER = 0,
            SHUT = 1,
            BIN = 2,
            THIS_PC = 3,
            TASK = 4
        }

        //const

        const int defaultSysIconWidth = 60;
        const int defaultSysIconHeight = 60;

        //vars
        public string sysActionString { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string imglink { get; set; }
        public string name { get; set; }

        public SysBloc(SysActions action)
        {
            switch (action)
            {
                case SysActions.SHUT:
                    this.sysActionString = "%systemroot%/system32/shutdown.exe -s -t 00";
                    this.imglink = "Images/shut.png";
                    this.name = "SHUT";
                    break;
                case SysActions.BIN:
                    this.sysActionString = "[shell:::{645FF040-5081-101B-9F08-00AA002F954E}]#UnloadSkin#";
                    this.imglink = "Images/bin.png";
                    this.name = "BIN";
                    break;
                case SysActions.THIS_PC:
                    this.sysActionString = "[shell:::{20D04FE0-3AEA-1069-A2D8-08002B30309D}]#UnloadSkin#";
                    this.imglink = "Images/this_pc.png";
                    this.name = "THIS_PC";
                    break;
                case SysActions.TASK:
                    this.sysActionString = "[taskmgr.exe]#UnloadSkin#";
                    this.imglink = "Images/task.png";
                    this.name = "TASK";
                    break;
                default:
                    throw new Exception("Cannot use this sysaction type here, use another contruction Sysbloc(action = OTHER, actionString, imgLink, name); ");
            }

            this.width = defaultSysIconWidth;
            this.height = defaultSysIconHeight;
        }

        public SysBloc(SysActions action, string actionString, string imgLink, string name)
        {
            if(action == SysActions.OTHER)
            {
                this.sysActionString = actionString;
                this.imglink = imglink;
                this.width = defaultSysIconWidth;
                this.height = defaultSysIconHeight;
                this.name = name;
            }
            else
            {
                throw new Exception("For a predefined Action use the other constructor SysBloc(action)");
            }
        }


    }
}
