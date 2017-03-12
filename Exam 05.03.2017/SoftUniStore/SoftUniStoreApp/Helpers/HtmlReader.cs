using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniStoreApp.Helpers
{
    public static class HtmlReader
    {
        public static string Read(string path)
        {
            string html = File.ReadAllText(path);
            return html;
        }
    }
}
