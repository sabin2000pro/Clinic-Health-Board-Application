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
    public class Client
    {
        public int clientID { get; set; } // Field set to public in order to check if a client ID exists in HealthFacade.CS
        private string firstName { get; set; }
        private string surname { get; set; }
        private string address1 { get; set; }
        private string address2 { get; set; }
        private double locLon { get; set; }
        private double locLat { get; set; }

        public Client(int clientID, string firstName, string surname, string address1, string address2, double locLon, double locLat)
        {
            this.clientID = clientID;
            // Check for null values
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.surname = surname ?? throw new ArgumentNullException(nameof(surname));

            this.address1 = address1 ?? throw new ArgumentNullException(nameof(address1));
            this.address2 = address2 ?? throw new ArgumentNullException(nameof(address2));
 
            this.locLon = locLon;
            this.locLat = locLat;
        }

        public override string ToString()
        {
            return this.clientID + "," + this.firstName + "," + this.surname + "," + this.address1 + "," + this.address2 + "," + this.locLon + "," + this.locLat;
        }
    }
}