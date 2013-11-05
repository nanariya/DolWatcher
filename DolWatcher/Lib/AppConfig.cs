using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolWatcher.Lib
{
    public class AppConfig
    {
        public void LoadDefault()
        {
            this.DolLogDir = 
                System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) 
                + "\\KOEI\\GV Online\\Log\\Chat";

            this.NewestFileName = "";

            this.Enable10c870 = true;
            this.Enable80aaff = true;
            this.Enable80ffff = true;
            this.Enabled4d4d4 = true;
            this.EnableOther = true;
            this.EnableDate = true;
            this.Color10c870 = "#10c870";
            this.Color80aaff = "#80aaff";
            this.Color80ffff = "#80ffff";
            this.Colord4d4d4 = "#d4d4d4";
            this.ColorOther = "#000000";
            this.Size10c870 = (Decimal)12;
            this.Size80aaff = (Decimal)12;
            this.Size80ffff = (Decimal)12;
            this.Sized4d4d4 = (Decimal)12;
            this.SizeOther = (Decimal)12;

        }

        public String DolLogDir { get; set; }

        public String NewestFileName { get; set; }
        public DateTime NewestFileTime { get; set; }
        public int LineCount { get; set; }
        public int ReadedRowNumber { get; set; }

        public bool Enable80aaff { get; set; }
        public bool Enable80ffff { get; set; }
        public bool Enabled4d4d4 { get; set; }
        public bool Enable10c870 { get; set; }
        public bool EnableOther { get; set; }

        public bool EnableDate { get; set; }

        public String Color80aaff { get; set; }
        public String Color80ffff { get; set; }
        public String Colord4d4d4 { get; set; }
        public String Color10c870 { get; set; }
        public String ColorOther { get; set; }

        public Decimal Size80aaff { get; set; }
        public Decimal Size80ffff { get; set; }
        public Decimal Sized4d4d4 { get; set; }
        public Decimal Size10c870 { get; set; }
        public Decimal SizeOther { get; set; }


        
    }
}
