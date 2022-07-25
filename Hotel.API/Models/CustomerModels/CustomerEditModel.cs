using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.CustomerModels
{
    public class CustomerEditModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }

        public string CNP { get; set; }
    }
}
