namespace HotelApp.SpecFlow.Constants
{
    public class ReservationRoutesConstants
    {
        public const string PostPath = "https://localhost:7091/api/reservation/create";
        public const string GetPath = "https://localhost:7091/api/reservation/getbyid";
        public const string EditPath = "https://localhost:7091/api/reservation/edit";
        public const string DeletePath = "https://localhost:7091/api/reservation/delete";

        public const string CreatedReservationKey = "RecentlyAddedReservation";
        public const string EditedReservationKey = "RecentlyEditedReservation";
    }
}
