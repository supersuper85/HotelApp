using System.ComponentModel.DataAnnotations;

namespace HotelApp.SpecFlow.Models.CustomerModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string CNP { get; set; }

        
    }
}
