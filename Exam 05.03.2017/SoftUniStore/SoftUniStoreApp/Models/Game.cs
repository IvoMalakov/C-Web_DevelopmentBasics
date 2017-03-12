using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniStoreApp.Models
{
    public partial class Game
    {
        private string title;
        private decimal price;
        private decimal size;
        private string imageThumbnail;
        private string trailer;
        private string desciption;

        public Game()
        {
            this.Owners = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (!IsTitleValid(value))
                {
                    throw new ArgumentException("Invalid title");
                }

                this.title = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }

            set
            {
                if (!IsDecimalValueValid(value))
                {
                    throw new ArgumentException("Invalid price");
                }

                this.price = value;
            }
        }

        public decimal Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (!IsDecimalValueValid(value))
                {
                    throw new ArgumentException("Invalid size");
                }

                this.size = value;
            }
        }

        [Column("ImageThumbnail")]
        public string ImageThumbnail
        {
            get
            {
                return this.imageThumbnail;
            }

            set
            {
                if (!IsImageThumbnailValid(value))
                {
                    throw new ArgumentException("Invalid image thumbnail");
                }

                this.imageThumbnail = value;
            }
        }

        public string Trailer
        {
            get
            {
                return this.trailer;
            }

            set
            {
                string checkTrailer = TakeTrailer(value);

                if (!IsTrailerValid(checkTrailer))
                {
                    throw new ArgumentException("Invalid trailer");
                }

                this.trailer = checkTrailer;
            }
        }

        public string Description
        {
            get
            {
                return this.desciption;
            }

            set
            {
                if (!IsDescriptionValid(value))
                {
                    throw new ArgumentException("Invalid description");
                }

                this.desciption = value;
            }
        }

        public DateTime? ReleaseDate { get; set; }

        public virtual ICollection<User> Owners { get; set; }
    }
}
