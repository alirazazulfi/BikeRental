using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRental.Entities.DBEtities
{
    public partial class Reservation
    {
        [NotMapped]
        public int BookingDays
        {
            get
            {
                return (int)(EndDate - StartDate).TotalDays + 1;
            }
        }

        [NotMapped]
        public string CancelDetails
        {
            get
            {
                return IsCancled ? $"{CancelReason} \n\r Cancel Date: {ModifiedDate.Value:dd-MMM-yyyy HH:mm}" : string.Empty;
            }
        }
    }
}
