using System;
using System.Collections.Generic;

namespace MRRCManagement
{
    /// <summary>
    /// The Customer class is used to create an object representing and holding the properties
    /// of each valid customer in the CRM. The customers have 6 properties of which 5 are 
    /// decided by them (title, firstname, lastname, gender, DOB). Customer ID is decided
    /// by the CRM class. There are no default properties for the customer constructor as all
    /// must be valid inputs by the customer.The methods in this class deal with sending the customers
    /// details to other classes. 
    /// </summary>
    public class Customer
    {
        private int ID { set; get; }
        private string title { set; get; }
        private string firstName { set; get; }
        private string lastName { set; get; }
        private GenderEnum gender { set; get; }
        private string DOB { set; get; }

        private enum GenderEnum
        {
            Male = 0,
            Female = 1,
            Other = 2
        }


        // this constructor should construct a customer with the provided attributes
        public Customer(int inputID, string inputTitle, string inputFirstName, string inputLastName, string inputGender, string inputDOB)
        {
            ID = inputID;
            title = inputTitle;
            firstName = inputFirstName;
            lastName = inputLastName;
            gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), inputGender); 
            DOB = inputDOB;
        }


        // this method returns the customers ID
        public int GetCustomerID()
        {
            return ID;
        }


        // this method should return a CSV suitable string for saving the customer to a csv file
        public string ToCSVString()
        {
            return ($"{ID},{title},{firstName},{lastName},{gender},{DOB}");
        }


        // this returns the details needed to make a display table for the customer
        public List<string> ToTableDisplay()
        {
            return (new List<string> { ID.ToString(), title, firstName, lastName, gender.ToString(), DOB });
        }
    } // end of customer class
}