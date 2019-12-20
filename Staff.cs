using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    // Business Layer Implementation
    // Author Name: Sabin Constantin Lungu
    // Class Name: Staff.cs

    // Purpose of Class: To store data about different types of Staff in the system.
    // Last Modified Date: 16/11/2019 @ 13:00
    // Date of Completion: 16/11/2019 @ 13:00
    public class Staff // Staff class implemented in Business Layer
    {
        public int staffID { get; set; } // StaffID is set to public so an exists check can be made in HealthFacade.cs
        private string firstName { get; set; }
        private string surname { get; set; }
        private string address1 { get; set; }
        private string address2 { get; set; }
        public string category { get; set; } // Category set to public since checks will be made in HealthFacade
        private double locLat { get; set; }
        private double locLon { get; set; }

        public Staff() // Default constructor
        {

        }
        public Staff(int staffID, string firstName, string surname, string address1, string address2, string category, double locLat, double locLon) // Parameterized constructor
        {
            // Checks for null entries to ensure no empty data is present.
            this.staffID = staffID;
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));

            this.surname = surname ?? throw new ArgumentNullException(nameof(surname));
            this.address1 = address1 ?? throw new ArgumentNullException(nameof(address1));
            this.address2 = address2 ?? throw new ArgumentNullException(nameof(address2));
            this.category = category ?? throw new ArgumentNullException(nameof(category));

            this.locLat = locLat;
            this.locLon = locLon;
        }

        public override string ToString() // To string method overriden to display appropriate data
        {
            return this.staffID + "," + this.firstName + "," + this.surname + "," + this.address1 + "," + this.address2 + "," + this.category + "," + this.locLat + "," + this.locLon;
        }
    }
}