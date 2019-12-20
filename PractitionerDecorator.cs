using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    // Business Layer Implementation
    // Author Name: Sabin Constantin Lungu
    // Class Name: PractitionerDecorator.cs

    // Purpose of Class: To store data about a Practitioner which is a type of staff that inherits from the base class Staff
    // Last Modified Date: 16/11/2019 @ 13:00
    // Date of Completion: 16/11/2019 @ 13:00
    public class PractitionerDecorator : Staff
    {
        public new int staffID { get; set; } // StaffID is set to public so an exists check can be made in HealthFacade.cs
        private string firstName { get; set; }
        private string surname { get; set; }
        private string address1 { get; set; }
        private string address2 { get; set; }
        public new string category { get; set; } // Category set to public since checks will be made in HealthFacade
        private double locLat { get; set; }
        private double locLon { get; set; }

        public PractitionerDecorator(int staffID, string firstName, string surname, string address1, string address2, string category, double locLat, double locLon)
        {
            this.staffID = staffID;
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.surname = surname ?? throw new ArgumentNullException(nameof(surname));
            this.address1 = address1 ?? throw new ArgumentNullException(nameof(address1));
            this.address2 = address2 ?? throw new ArgumentNullException(nameof(address2));
            this.category = category ?? throw new ArgumentNullException(nameof(category));
            this.locLat = locLat;
            this.locLon = locLon;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}