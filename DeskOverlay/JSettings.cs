using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DeskOverlay
{
    public static class JSettings
    {
        public static string settingsFileName = "settings.json";
        private static string taskbarFile = SMPaths.GetAppDataFile("smtaskbarinfo.txt");
        private static string bginfoFile = SMPaths.GetAppDataFile("smbginfo.txt");

        public static dynamic jdata = null;
        private static int defaultMode = 0;
        
        private static string defaultTextColor = "#FFEA0F55";
        private static string defaultFillColor = "#FFE6E6E6";
        
        private static Thickness bginfoMargin = new Thickness(0, 0, 50, 10);
        private static Thickness taskbarMargin = new Thickness(0, 0, 250, 10);
 
        public static void RefreshData()
        {
            try
            {
                var TextDataLoader = new TextDataLoader(SMPaths.GetAppDataFile(settingsFileName));
                var txtData = TextDataLoader.Data;
                if (txtData.StartsWith("{"))
                {
                    jdata = JValue.Parse(txtData);
                }
                else
                {
                    // direct detection mode
                    if (System.IO.File.Exists(taskbarFile)) {
                        defaultMode = 1;

                        defaultTextColor = "#FF55EA0F";
                        defaultFillColor = "#FFE6E6E6";
                       
                    }
                }


            }
            catch (Exception)
            {
                

            }
        }
        static JSettings()
        {
            RefreshData();
        }

        public static int Mode
        {
            get { return (int)(jdata?.Mode ?? defaultMode); }
        }

        public static int Fontsize
        {
            get { return (int)(jdata?.Fontsize ?? 30); }
        }

        public static int DisplayGridRow
        {
            get
            {
                return (int)(jdata?.Row ?? 4)-1;
            }
        }
        public static int DisplayGridColumn
        {
            get {
                return (int)(jdata?.Column ?? 3)-1;
            }
        }
        public static TextAlignment TextAlignment
        {
            get
            {
                var align = ((string)(jdata?.Align ?? "Right")).Trim().ToLower();
                if (align.StartsWith("left")) { return TextAlignment.Left; }
                if (align.StartsWith("just")) { return TextAlignment.Justify; }
                if (align.StartsWith("mid") || align.StartsWith("cent")  ) { return TextAlignment.Center; }
                return TextAlignment.Right;

            }
        }

        public static Thickness? Margin
        {
            get
            {
                Thickness? thickness = (defaultMode == 1) ? taskbarMargin : bginfoMargin; //new Thickness(0, 0, 0, 0);
                try
                {
                    var margin = ((string)(jdata?.Margin ?? "NotSpecified")).Trim().ToLower();
                    var ma = margin.Split(',');
                    if (ma.Length == 4)
                    {
                        thickness = new Thickness(
                                Double.Parse(ma[0]),
                                Double.Parse(ma[1]),
                                Double.Parse(ma[2]),
                                Double.Parse(ma[3])
                            );
                    }
                }
                catch { }
                return thickness;

            }
        }

        public static Object TextColor
        {
            get
            {
                var color = ((string)(jdata?.TextColor ?? defaultTextColor)).Trim().ToLower();
                try
                {
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                }
                catch { }
                return null;
            }
        }
        public static Object FillColor
        {
            get
            {
                var color = ((string)(jdata?.FillColor ?? defaultFillColor)).Trim().ToLower();
                try
                {
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                }
                catch { }
                return null;
            }
        }

        public static bool ShowBackfill
        {
            get {
                return (bool)(jdata?.BackFill ?? true); }
        }

        public static string AppDataFile { get {
                switch (Mode)
                {
                    case 1:
                        return taskbarFile;
                    default:
                        return bginfoFile;
                        break;
                }
            } }  
        

    }
}
