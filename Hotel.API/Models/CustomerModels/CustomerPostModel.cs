using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.CustomerModels
{
    public class CustomerPostModel
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string CNP { get; set; }
    }
}
