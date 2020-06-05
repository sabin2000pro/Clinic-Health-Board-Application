using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BusinessLayer
{
    // Code Author: Sabin Constantin Lungu
    // Matriculation Number: 40397517
    // Last Modified Date: 16/11/2019 @ 13:00

    // Date of Completion: 16/11/2019 @ 13:00
    // Purpose of Class: The purpose of this class is to communicate with the Presentation Layer in order to display the correct results on the Health Board Form
    // Any Errors? : N/A
    
    public static class visitTypes
    {
        // Types of assessments to be checked
        public const int assessment = 0;
        public const int medication = 1;

        public const int bath = 2;
        public const int meal = 3;
    }
    
    public class HealthFacade
    {
        private List<Staff> staffList = new List<Staff>(); // A list of staff
        private List<Client> clientList = new List<Client>(); // A list of client objects 
        private List<Visits> listOfVisits = new List<Visits>(); // A lsit of visits objects

        private bool clientIDExists = false; // Boolean variable to determine whether the client ID exists or not
        private bool staffIDExists = false;

        private bool assessmentTypeValid = false; // Determines if the assessment type is valid or not.
        private int staffDataVerified = 0; // Flag to determine if data is checked

        private string[] writeFilePaths = { @"C:/Users/sabin/Desktop/cw2/staffData.txt"
    , @"C:/Users/sabin/Desktop/cw2/clientData.txt", @"C:/Users/sabin/Desktop/cw2/visitsData.txt"}; // List of file paths to be writtenTo

        private string[] loadPaths = { @"C:/Users/sabin/Desktop/cw2/staffData.txt", @"C:/Users/sabin/Desktop/cw2/clientData.txt", @"C:/Users/sabin/Desktop/cw2/visitsData.txt" };
        private int fileOpenedFlag = 0; // A flag for writing to & saving file
        
        public Boolean addStaff(int id, string firstName, string surname, string address1, string address2, string category, double baseLocLat, double baseLocLon) // Routine to Add members of Staff to the system
        {
            Staff staffObject = new Staff(id, firstName, surname, address1, address2, category, baseLocLat, baseLocLon); // Create a new staff instance.
            
            try
            {
                if (staffObject != null) // If the staff object is not empty
                {
                    staffList.Add(staffObject); // Add the staff instance to the array list
                }

                return true; // Returns true after it has been added

            }
            
            catch {   // Catch the exception
            
                return false; // Return false otherwise
            }
        }
        
        public Boolean addClient(int id, string firstName, string surname, string address1, string address2, double locLat, double locLon) // Adds a client to the system
        {
            Client clientObject = new Client(id, firstName, surname, address1, address2, locLat, locLon); // Create a new Client instance.
           
            try
            {
                if (clientObject != null) // If there is a client instance of the class.
                {
                    clientList.Add(clientObject); // Add the object data to the list
                }

                return true;
            }

            catch { // Catch the exception.
            
                return false; // Otherwise return false
            }
        }

        public Boolean addVisit(int[] staff, int patient, int type, string dateTime) // Routine that adds visits for clients
        {
         
            Visits visits = new Visits(staff, patient, type, dateTime); // Create visits instance
            
            checkAssessmentTypes(type); // Method will be called to check assessment type
            verifyClientID(patient); // Call method to verify if the client ID is valid

            verifyStaffID(patient);
            verifyStaffData(type);

            checkTimeClash(staff, patient, type, dateTime);

            try
            {
                if(visits != null) // If the visits object is not empty
                {
                    listOfVisits.Add(visits); // Add it to the list
                }

                return true;
            }
           
            catch {
            
                return false; // Otherwise return false
            }
        }

        public void checkAssessmentTypes(int type) // Routine that checks for the assessment type
        {
          
            if (type < 0 || type > 3) { // If the type is not between 0-3
              
               assessmentTypeValid = false; // The assessment type is false.
               throw new Exception("\n Assessment type must be between 0-3 \n "); // Throw exception

                }

            else {
           
             assessmentTypeValid = true; // Otherwise it's true.

                }
            }

        public void verifyClientID(int patient) // Routine to verify if a client ID exists or not
        {
            while(clientIDExists)
            {
                if (clientList.Find(searchClientID => searchClientID.clientID == patient) == null) // If a unique client ID in the list does not exist, i.e is null
                {
                    clientIDExists = false; // Set flag to false.
                    
                    throw new Exception("A unique client ID does not exist.");
                }

                else {
                
                    clientIDExists = true;
                }
            }
        }

        public void verifyStaffID(int patient) // Routine to verify if a staff ID exists or not
        {
            while(staffIDExists)
            {
                if(staffList.Find(searchStaffID => searchStaffID.staffID == patient) == null) // If the StaffID is equal to the patient that is empty (doesn't exist)
                {
                    staffIDExists = false; // Staff ID does not exist
                    throw new Exception("A unique Staff ID does not exist"); // Throw exception
                }

                else {
                
                    staffIDExists = true; // Otherwise staff ID exists if the first condition fails
                }
            }
        }

        public void verifyStaffData(int type)
        {
            string[] workerTypes = { "Social Worker", "General Practitioner", "Care Worker", "Community Nurse" }; // A string array of worker types
            int[] staffRequirements = { 2, 1 }; // Array of staff requirements

            int isClientAvailable = 0;
            int isStaffAvailable = 0;

            switch(type) {
            
                case 0:

                    if(staffList.Find(searchQuery => searchQuery.category == workerTypes[0]) == null || staffList.Find(searchQuery => searchQuery.category == workerTypes[3]) == null)
                    throw new Exception("Invalid Member Of Staff");

                    isStaffAvailable = 0;
                    break;

                case 1:
                    if (staffList.Count != staffRequirements[1]) // If the staff list count is not equal to 1

                        throw new Exception("\n Non-existent Client \n ");
                        isClientAvailable = 0;
                 
                    if (staffList.Find(searchQuery => searchQuery.category == workerTypes[3]) == null) // If there are no community nurses in the array

                    throw new Exception("\n There is no Community Nurse. \n "); // Throw exception.
                    isStaffAvailable = 0;
                    break; // Break out of the case statement

                case 2:
                    
                    if (staffList.FindAll(searchQuery => searchQuery.category == workerTypes[2]).Count != staffRequirements[0]) // IF the count of the care workers is less than 2
                        throw new Exception("There are not enough Social Workers to handle Bathing"); // Throw exception with the appropriate message
                    break;

                case 3:

                    if (staffList.Count != staffRequirements[0])
                        throw new Exception("\n Invalid Member Of Staff Present");
                    
                    isStaffAvailable = 0;

                    if (staffList.Find(searchQuery => searchQuery.category == workerTypes[2]) == null)

                    throw new Exception("No care worker available");
                    isStaffAvailable = 0;

                    staffDataVerified = 1; // Set flag to true since data has been verified
                    break;
            }
        }

        public void checkTimeClash(int[] staff, int patient, int type, string dateTime) // Routine that checks time clash between appointments
        {
            string exceptionMessage = "Time Clash";

                foreach (Visits visitID in listOfVisits) // For each of the visits
                {
                    DateTime startTime = visitID.dateTime; // Set the start time to the visit date time
                    DateTime endTime = startTime;

                    if (type == 0)
                    {
                        endTime.AddMinutes(60);
                    }

                    if(type == 1)
                    {
                        endTime.AddMinutes(20);
                    }

                    if(type == 2)
                    {
                        endTime.AddMinutes(30);
                    }

                    if(type == 3) // If the assessment type is type 3.
                    {
                        endTime.AddMinutes(30); // Add 30 minutes to the appointment
                    }

                    if(Convert.ToDateTime(dateTime) <= startTime && Convert.ToDateTime(dateTime) >= endTime) // If the date time is between the start time and end time
                    {
                        
                        throw new Exception(exceptionMessage); // Throw exception
                    }
                }
            }

        public String getStaffList() // Routine that prints the staff list
        {
            String result = string.Join(Environment.NewLine, staffList); // Set the output result by joining the strings and adding them on a new line
            return result;
        }

        public String getClientList() // Routine that gets the client list
        {
            String result = string.Join(Environment.NewLine, clientList); // Display the client list on a new line
            return result;
        }

        public String getVisitList() // Method that gets a list of visits
        {
            String result = string.Join(Environment.NewLine, listOfVisits); // Join the list on a new line
            
            return result;
        }

        public void clear() // Clear the form data.
        {
            // Clear the data from the form
            staffList.Clear();
            clientList.Clear();
            listOfVisits.Clear();
        }

        public Boolean load() // Routine to load data from a file.
        {
            try
            {
                clear(); // Clear form by default
                string line; // Line in the file

                // Create instances of Stream Readers to read the file paths in
                StreamReader staffReader = new StreamReader(loadPaths[0]);
                StreamReader clientReader = new StreamReader(loadPaths[1]);
                StreamReader visitsReader = new StreamReader(loadPaths[2]);
                
                while ((line = staffReader.ReadLine()) != null) // Loop over the staff reader file path
                {
                    string[] splitData = line.Split(','); // Split the contents in the file using a comma delimiter.
                    addStaff(Convert.ToInt32(splitData[0]), splitData[1], splitData[2], splitData[3], splitData[4], splitData[5], Convert.ToDouble(splitData[6]), Convert.ToDouble(splitData[7]));
                } 

                while((line = clientReader.ReadLine()) != null)
                {
                    string[] tokenizedData = line.Split(','); // Split the data using a comma
                    addClient(Convert.ToInt32(tokenizedData[0]), tokenizedData[1], tokenizedData[2], tokenizedData[3], tokenizedData[4], Convert.ToDouble(tokenizedData[5]), Convert.ToDouble(tokenizedData[6]));
                }

                while((line = visitsReader.ReadLine()) != null) // Loop over the visits file
                {
                    
                    string[] tokenizedData = line.Split(','); // Split the data in the file by a comma delimiter.
                    int[] visitStaffIds = Array.ConvertAll(tokenizedData[0].Split(' '), tempVar => int.Parse(tempVar)); // This line of code takes tokenizedData[0] at index 0 which is "1 3" in the text file for visits, and converts it into an int[] of {1, 3}
                  
                    addVisit(visitStaffIds, Convert.ToInt32(tokenizedData[1]), Convert.ToInt32(tokenizedData[2]), Convert.ToString(tokenizedData[3])); // Call method to add the data and display it
                }
            }
            
            catch (Exception exc) {
            
                return true;
            }

            return false;
        }

        public bool save() // Routine to save data to a file.
        {
            writeStaffData(writeFilePaths); // Call the method
            
            return true;
        }

        public void writeStaffData(string[] writeFilePaths) // Routine that saves data for Staff to a file
        {
            if (!File.Exists(writeFilePaths[0])) { // If the file path at index 0 does not exist
            
                TextWriter writer = new StreamWriter(writeFilePaths[0]); // Create a new Text Writer instance.

                foreach (Staff memberOfStaff in staffList) { // For every member of staff in the staff list

                writer.WriteLine(memberOfStaff.ToString()); // Write it to the file by calling toString()
                    
               }

                fileOpenedFlag = 1; // File flag is now true
                writer.Close(); // Close the file writer.

                fileOpenedFlag = 0; // File is closed 
                writeClientData(writeFilePaths);
            }

            else if (File.Exists(writeFilePaths[0]))
            {
                File.WriteAllText(writeFilePaths[0], String.Empty); // Empty out the file

                using (var theTextWriter = new StreamWriter(writeFilePaths[0], true))
                {
                    foreach (Staff memberOfStaff in staffList) // For each member of staff in the staff list
                    
                        theTextWriter.WriteLine(memberOfStaff.ToString()); // Call ToString() to add the data to the file
                        theTextWriter.Close();
                }
            }
        }
        
        public void writeClientData(string[] writeFilePaths) // Routine that writes client data to a file
        {
            if (!File.Exists(writeFilePaths[1])) // If the file path at index 1 does not exist
            {
                TextWriter writer = new StreamWriter(writeFilePaths[1]); // Create a textwriter instance of type stream writer by passing in the path

               foreach (Client client in clientList) {

               writer.WriteLine(client.ToString());
                   
              }
               fileOpenedFlag = 1; // Flag is opened

               writer.Close();

               fileOpenedFlag = 0; // File opened flag is false
               writeVisitsData(writeFilePaths);
            }

            else if (File.Exists(writeFilePaths[1]))
            {
                File.WriteAllText(writeFilePaths[1], String.Empty); // Empty out the strings in the file

                using (var theTextWriter = new StreamWriter(writeFilePaths[1], true)) // Uses the specific file path to open the file
                {
                    foreach (Client client in clientList)

                    theTextWriter.WriteLine(client.ToString()); // Write the data for the clients to the file using ToString()
                    theTextWriter.Close(); // Close the file to prevent errors
                }
            }
        }
        
        public void writeVisitsData(string[] writeFilePaths) // Routine to write visits data to file
        {
            if (!File.Exists(writeFilePaths[2])) { // If the file path at index 2 does not exist
            
                TextWriter writer = new StreamWriter(writeFilePaths[2]);

               foreach (Visits clientVisits in listOfVisits) { // For each of the client visits in the list of visits

               writer.WriteLine(clientVisits.ToString()); // Write the clients to the output.
               writer.Close(); // Close the file writer
               }
                
            }

            else if (File.Exists(writeFilePaths[2])) {
            
                File.WriteAllText(writeFilePaths[2], String.Empty); // Empty out the file

                using (var theTextWriter = new StreamWriter(writeFilePaths[2], true))
                {
                    foreach (Visits clientVisits in listOfVisits) // For each client visit in the list of visits

                    theTextWriter.WriteLine(clientVisits.ToString()); // Call the ToString() Method to write the data to the visitListData.txt
                    theTextWriter.Close(); // Close file
                }
            }
        }
    }
}
