using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskOverlay
{
    public class TextDataLoader
    {
        private string data = "no info";
        private string filepath;
        public TextDataLoader(string path)
        {
            this.filepath = path;
        }

        public void Refresh()
        {
            try
            {
                // Read the file as one string.
                this.data = System.IO.File.ReadAllText(this.filepath);
            }
            catch (Exception ex)
            {
                this.data = $"Add : {this.filepath}";
            }
        }

        public string Data { get { this.Refresh(); return this.data; } }
    }
}
