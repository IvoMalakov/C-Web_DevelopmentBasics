using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftUniStoreApp.Helpers;

namespace SoftUniStoreApp.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string ImageThumblain { get; set; }

        public string Title { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HtmlReader.Read(Constans.GameTemplate));
            sb.Replace("{thumblain}", this.ImageThumblain);
            sb.Replace("{title}", this.Title);
            sb.Replace("{price}", this.Price.ToString());
            sb.Replace("{size}", this.Size.ToString());
            sb.Replace("{id}", this.Id.ToString());
            sb.Replace("{description}", this.Description.Substring(0, 299));

            return sb.ToString();
        }
    }
}
