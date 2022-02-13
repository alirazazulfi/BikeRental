using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using BikeRental.Common;

namespace BikeRental.Entities.DBEtities
{
    public partial class User
    {
        [NotMapped]
        public string FullName
        {
            get
            {
                string fullName = "";
                if (!FirstName.IsNullEmpty())
                {
                    FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FirstName.ToLower());
                    fullName = FirstName;
                }
                if (!LastName.IsNullEmpty())
                {
                    LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LastName.ToLower());
                    if (fullName.Length > 0) fullName += " " + LastName;
                    else fullName = LastName;
                }
                return fullName;
            }
        }         
    }
}
