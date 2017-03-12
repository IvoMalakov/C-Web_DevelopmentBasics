using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftUniStoreApp.Helpers;

namespace SoftUniStoreApp.ViewModels
{
    public class GameDetailsViewModel
    {
        public string Trailer { get; set; }

        public string Title { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(HtmlReader.Read(Constans.GameDetails));
            sb.Replace("{trailer}", this.Trailer);
            sb.Replace("{description}", this.Description);
            sb.Replace("{price}", this.Price.ToString());
            sb.Replace("{size}", this.Size.ToString());
            sb.Replace("{releaseDate}", this.ReleaseDate.ToString());

            return sb.ToString();
        }
    }
}
