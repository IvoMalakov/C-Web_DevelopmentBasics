using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniStoreApp.BindingModels
{
    public class EditGameBindingModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageThumbnail { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Trailer { get; set; }
    }
}
