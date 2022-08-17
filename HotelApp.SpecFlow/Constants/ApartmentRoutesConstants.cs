using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.SpecFlow.Constants
{
    public static class ApartmentRoutesConstants
    {
        public const string PostPath = "https://localhost:7091/api/apartment/create";
        public const string GetPath = "https://localhost:7091/api/apartment/get";
        public const string EditPath = "https://localhost:7091/api/apartment/edit";
        public const string DeletePath = "https://localhost:7091/api/apartment/delete";

        public const string CreatedApartmentKey = "RecentlyAddedApartment";
        public const string EditedApartmentKey = "RecentlyEditedApartment";
    }
}
