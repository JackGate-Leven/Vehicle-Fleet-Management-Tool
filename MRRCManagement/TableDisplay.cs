using System;
using System.Collections.Generic;

namespace MRRCManagement
{
    /// <summary>
    /// This class holds the constructors that create and display tables of Customer, Vehicle and Rental
    /// data. The class gets input from Fleet and CRM to create display tables.
    /// </summary>
    public class TableDisplay
    {
        // Creates line with - across it the width of the table, creates a horizontal border
        private void NewLine(int tableWidth)
        {
            Console.WriteLine(new string('-', tableWidth));
        }


        // constructor creates and displays a table containing all the properties of the supplied customers
        public TableDisplay(List<Customer> dataForTable)
        {
            int tableWidth = 82;
            List<string> tableHeader = new List<string> { "ID", "Title", "First Name", "Last Name", "Gender", "DOB" };

            // start of table display construction
            NewLine(tableWidth);
            PrintRow(tableHeader);

            foreach (Customer customer in dataForTable)
            {
                NewLine(tableWidth);
                PrintRow(customer.ToTableDisplay());
            }

            NewLine(tableWidth);

            // prints row with given columns
            void PrintRow(List<string> columns)
            {
                List<int> valueMaxLengths = new List<int> { 5, 6, 16, 20, 6, 10};
                for (int value = 0; value < tableHeader.Count; value++)
                {
                    // if value is longer then column width cut it off
                    if (columns[value].Length >= valueMaxLengths[value] + 1)
                    {
                        columns[value] = columns[value].Substring(0, valueMaxLengths[value] - 3);
                        columns[value] += "...";
                    } 
                }
                Console.WriteLine(String.Format($"| {columns[0],-5} | {columns[1],-6} | {columns[2],-16} | {columns[3],-20} | {columns[4],-6} | {columns[5],-10} |"));
            }
        } // end of Customer table construction


        // constructor creates and displays a table containing all the properties of the supplied vehicles
        public TableDisplay(List<Vehicle> dataForTable)
        {
            int tableWidth = 146;
            List<string> tableHeader = new List<string> { "Registration","Grade","Make","Model","Year","NumSeats",
                                        "Transmission","Fuel","GPS","SunRoof","DailyRate","Colour" };

            // start of table display construction
            NewLine(tableWidth);
            PrintRow(tableHeader);

            foreach (Vehicle vehicle in dataForTable)
            {
                NewLine(tableWidth);
                PrintRow(vehicle.ToTableDisplay());
            }

            NewLine(tableWidth);

            // prints row with given columns
            void PrintRow(List<string> columns)
            {
                List<int> valueMaxLengths = new List<int> { 12 , 10, 12, 18, 4, 8, 12, 6, 5, 7, 9, 7};
                for (int value = 0; value < tableHeader.Count; value++)
                {
                    // if value is longer then column width cut it off
                    if (columns[value].Length >= valueMaxLengths[value] + 1)
                    {
                        columns[value] = columns[value].Substring(0, valueMaxLengths[value]-3);
                        columns[value] += "...";
                    } 
                }
                Console.WriteLine(String.Format($"| {columns[0],-12} | {columns[1],-10} | {columns[2],-12} | {columns[3],-18} | {columns[4],-4} | {columns[5],-8} | " +
                    $"{columns[6],-12} | {columns[7],-6} | {columns[8],-5} | {columns[9],-7} | {columns[10],-9} | {columns[11],-7} |"));
            }
        } // end of vehicle table construction


        // constructor creates and displays a table containing all the rentals 
        public TableDisplay(List<List<string>> regoIDList)
        {
            int tableWidth = 34;
            List<string> tableHeader = new List<string> { "Registration","ID", "DailyRate" };

            // start of table display construction
            NewLine(tableWidth);
            PrintRow(tableHeader);

            foreach (List<string> regoIDSet in regoIDList)
            {
                NewLine(tableWidth);
                PrintRow(regoIDSet);
            }

            NewLine(tableWidth);

            // prints row with given columns
            void PrintRow(List<string> columns)
            {
                List<int> valueMaxLengths = new List<int> { 12 , 3 , 9};

                for (int value = 0; value < tableHeader.Count; value++)
                {
                    // if value is longer then column width cut it off
                    if (columns[value].Length >= valueMaxLengths[value] + 1)
                    {
                        columns[value] = columns[value].Substring(0, valueMaxLengths[value] - 3);
                        columns[value] += "...";
                    } 
                }
                Console.WriteLine(String.Format($"| {columns[0],-12} | {columns[1],-3} | {columns[2],-9} |"));
            }
        } // end of Rental table construction
    }
}
