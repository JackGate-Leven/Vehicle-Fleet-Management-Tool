using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRRCManagement
{
    /// <summary>
    /// This class deals with customers and customer based methods.
    /// For instance this class can be used to test the existance of a customer
    /// based on a given ID. The class loads and saves to the customer.csv file.
    /// </summary>
    public class CRM
    {
        private List<Customer> customersList { set; get; } = new List<Customer>();
        public string CRMFilePath = @"..\..\Data\customers.csv";
        private const string CRMFileHeader = "ID,Title,FirstName,LastName,Gender,DOB";

        
        // constructor for crm
        public CRM()
        {
        } // end of CRM


        // creates and returns a new Id number that is the lowest free id number
        public int NewIDNumber()
        {
            for (int newID = 0; newID < 100; newID++)
            {
                if (!CustomerExists(newID))
                {
                    return newID;
                }
            }
            return 0; // if no customers exist
        } // end of NewIDNumber


        // returns true if the given customer exists, false otherwise 
        public bool CustomerExists(int ID)
        {
            foreach (Customer customer in customersList)
            {
                if (customer.GetCustomerID() == ID)
                {
                    return true;
                }
            }
            return false;
        } // end of CustomerExists


        // adds the provided customer to the customer list if the customer ID doesn’t
        // already exist in the CRM. It returns true if the addition was successful (the customer
        // ID didn’t already exist in the CRM) and false otherwise
        public void AddCustomer(Customer newCustomer)
        {
            customersList.Add(newCustomer);
            customersList = customersList.OrderBy(o => o.GetCustomerID()).ToList();
        } // end of AddCustomer


        // edits the customer with the input ID by removing the customer from the customers list and then adding the modified version.
        // then the customers list is ordered so ID order is kept
        public void ModifyCustomer(Customer modifiedCustomer, int ID)
        {
            customersList.Remove(GetCustomer(ID));
            customersList.Add(modifiedCustomer);
            customersList = customersList.OrderBy(o=>o.GetCustomerID()).ToList();
        } // end of ModifyCustomer


        // removes the customer with the provided customer ID from the CRM
        public void RemoveCustomer(int ID, Fleet fleet)
        {
            foreach (Customer customer in customersList)
            {
                if (customer.GetCustomerID() == ID)
                {
                    customersList.Remove(GetCustomer(ID));
                    return;
                }
            }
        } // end of RemoveCustomers


        // returns the list of current customers
        public List<Customer> GetCustomers()
        {
            return customersList;
        } // end of GetCustomers


        // returns the customer who matches the provided ID
        public Customer GetCustomer(int ID)
        {
            foreach (Customer customer in customersList)
            {
                if (customer.GetCustomerID() == ID)
                {
                    return customer;
                }
            }

            // will never go to this because of prior tests in program
            return customersList[ID];
        } // end of GetCustomer


        // startup of crm file loading that checks if files exist
        public bool CRMFileSetup(string crmFilePath = @"..\..\Data\Customers.csv")
        {
            this.CRMFilePath = crmFilePath;

            // if crm file exists then load crm from that file path
            if (File.Exists(crmFilePath))
            {
                LoadFromFile(crmFilePath);
                return true;
            }
            else
            {
                this.CRMFilePath = crmFilePath;
                File.WriteAllText(this.CRMFilePath, CRMFileHeader);
                return false;
            }
        }


        // loads the state of the CRM from file
        public void LoadFromFile(string filePath)
        {
            var read = new StreamReader(File.OpenRead(filePath));
            List<String> lines = new List<string>();
            List<String> linesValues;

            // add all lines from crm csv file to lines list
            while (!read.EndOfStream)
            {
                lines.Add(read.ReadLine()); 
            }

            // file header
            if (lines[0] == CRMFileHeader)
            {
                lines.Remove(CRMFileHeader);
            }

            // splitting of list and values
            for (int lineNumber = 0; lineNumber < lines.Count(); lineNumber++)
            {
                linesValues = new List<string>();
                foreach (string value in lines[lineNumber].Split(','))
                {
                    linesValues.Add(value.Trim());
                }
                customersList.Add(new Customer(int.Parse(linesValues[0]), linesValues[1], linesValues[2], linesValues[3], linesValues[4], linesValues[5]));
            } 

            read.Close();
        } // End of LoadFromFile


        // saves the current state of the CRM to file
        public void SaveToFile()
        {
            string saveFileString = CRMFileHeader;

            foreach (Customer customer in customersList)
            {
                customer.ToString();
                saveFileString += "\n" + customer.ToCSVString();
            }
            File.WriteAllText(this.CRMFilePath, saveFileString);
        } // end of SaveToFile
    }
}