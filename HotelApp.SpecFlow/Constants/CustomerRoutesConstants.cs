using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.SpecFlow.Constants
{
    public static class CustomerRoutesConstants
    {
        public const string PostPath = "https://localhost:7091/api/customer/create";
        public const string GetPath = "https://localhost:7091/api/customer/getbyid";
        public const string EditPath = "https://localhost:7091/api/customer/edit";
        public const string DeletePath = "https://localhost:7091/api/customer/delete";

        public const string CreatedCustomerKey = "RecentlyAddedCustomer";
        public const string EditedCustomerKey = "RecentlyEditedCustomer";
    }
}
