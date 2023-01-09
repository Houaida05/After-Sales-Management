

using shared;
using System.ComponentModel.DataAnnotations;

namespace ClientWebService.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductPhoto { get; set; }


    }

   
}
