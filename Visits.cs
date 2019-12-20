using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    // Business Layer Implementation
    // Author Name: Sabin Constantin Lungu
    // Class Name: Visits.cs

    // Purpose of Class: To store data about different types of Staff in the system.
    // Last Modified Date: 16/11/2019 @ 13:00
    // Date of Completion: 16/11/2019 @ 13:00
    public class Visits
    {

        // Attributes are made public because they need to be printed out in HealthFacade
        public int[] staffRequired { get; set; } // Integer array of staff required
        public int visitType { get; set; } // Type of visit
        public int idCode { get; set; } // Id code for the visit
        public DateTime dateTime; // Date time of visit

        public Visits(int[] staffRequired, int visitType, int idCode, string dateTime) // Constructor
        {
            this.visitType = visitType;
            this.staffRequired = staffRequired ?? throw new ArgumentNullException(nameof(staffRequired)); // Check for null entries
            this.idCode = idCode;
            this.dateTime = Convert.ToDateTime(dateTime); // Convert the date time string to date time
        }

        public override string ToString()
        {
            string displayVisitsString = ""; // Empty string
        
            for (int staffIndex = 1; staffIndex < staffRequired.Length; staffIndex++) // Loop over the staff array
                displayVisitsString += staffIndex + ",";
                
            return displayVisitsString += $"{this.visitType},{this.idCode},{this.dateTime}"; // Return the data by concatenating the empty string onto the new data

            }
        }
    }