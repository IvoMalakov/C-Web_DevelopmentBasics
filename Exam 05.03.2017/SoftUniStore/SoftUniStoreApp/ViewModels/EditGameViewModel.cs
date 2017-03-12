using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniStoreApp.ViewModels
{
    public class EditGameViewModel
    {
        public class EditCategoryViewModel
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public override string ToString()
            {
                string representation =
                    $"<form method=\"POST\" action=\"/games/edit\">\r\n\t\t<label>Name</label>\r\n\t\t<div class=\"form-group\">\r\n\t\t\t<input type=\"hidden\" hidden=\"hidden\" class=\"form-control\" value=\"{this.Id}\" name=\"categoryId\">\r\n\t\t\t<input type=\"text\" class=\"form-control\" value=\"{this.Title}\" name=\"categoryName\"/>\r\n\t\t</div>\r\n\t\t<input type=\"submit\" class=\"btn btn-primary\" value=\"Edit Game\"/>\r\n\t</form>";

                return representation;
            }
        }
    }
}
