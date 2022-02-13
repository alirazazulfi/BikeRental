using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRental.Entities.DBEtities
{
    public partial class Bike
    {
        [NotMapped]
        public double AverageRating
        {
            get
            {
                if (BikeRatings != null && BikeRatings.Count > 0)
                {
                    double average = BikeRatings.Average(x => x.Rating);
                    return average;
                }
                else return 0;
            }
        }
    }
}
