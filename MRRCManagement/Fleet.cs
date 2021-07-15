using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Linq.Expressions;

namespace MRRCManagement
{
    /// <summary>
    /// This Class holds the contructor for fleet and its methods.
    /// Fleet controls vehicles and rentals, Deals with Fleet.csv and Rentals.csv files 
    /// and vehicle objects and the rental list.
    /// </summary>
    public class Fleet
    {
        private List<Vehicle> vehiclesList { set; get; } = new List<Vehicle>();
        private List<List<string>> rentalRegoID { set; get; } = new List<List<string>>(); // List of rentals, (rego to id) lists

        private string fleetFilePath { set; get; } = @"..\..\Data\fleet.csv";
        private string rentalsFilePath { set; get; } = @"..\..\Data\rentals.csv";

        private const string fleetFileHeader = "Registration,Grade,Make,Model,Year,NumSeats,Transmission,Fuel,GPS,SunRoof,DailyRate,Colour";
        private const string rentalsFileHeader = "Registration,CustomerID";


        // constructor for fleet
        public Fleet()
        {
        } // end of Fleet constructor


        // adds the provided vehicle to the fleet assuming the vehicle registration does not
        // already exist. It returns true if the add was successful (the registration did not
        // already exist in the fleet), and false otherwise
        public bool AddVehicle(Vehicle vehicle)
        {
            vehiclesList.Add(vehicle);
            return true;
        } // end of AddVehicle


        // edits the vehicle by removing the current one from the vehicles lsit and adding the edited one
        public bool ModifyVehicle(Vehicle modifiedVehicle, string registration)
        {
            if (VehicleExists(registration))
            {
                RemoveVehicle(registration);
                vehiclesList.Add(modifiedVehicle);
                return true;
            }

            else
            {
                Console.WriteLine("\nError. There was a problem editing the customer.");
                return false;
            } 
        } // end of ModifyVehicle


        // removes the vehicle with the provided rego from the fleet if it is not
        // currently rented. It returns true if the removal was successful and false otherwise
        public bool RemoveVehicle(string registration)
        {
            // check if vehicle is being rented, if it isn't allow removale
            if (vehiclesList.Remove(GetVehicle(registration)))
            {
                return true;
            }

            // if vehicle can't be removed from vehicle list
            else
            {
                return false;
            } 
        } // end of RemoveVehicle


        // checks if a vehicle with the given registration exists and returns true if it does, false if not
        public bool VehicleExists(string registration)
        {
            foreach (Vehicle vehicle in vehiclesList)
            {
                if (vehicle.GetVehicleProperties()[0] == registration)
                {
                    return true;
                }
            }
            return false;
        } // end of VehicleExists


        // returns a list of all vehicles if false and just rented vehicles if true
        // If input is true then just rented vehicles will be returned
        public List<Vehicle> GetAllVehicles()
        {
            return vehiclesList;
        } // end of GetAllVehicles

        // returns a list of vehicle (all rented if true input and all unrented if false input)
        public List<Vehicle> GetRentedVehicles(bool rented)
        {
            List<Vehicle> Vehicles = new List<Vehicle>();

            // returns a list of rented vehicles
            if (rented)
            {
                foreach (Vehicle vehicle in vehiclesList)
                {
                    if (IsRented(vehicle.GetVehicleProperties()[0]))
                    {
                        Vehicles.Add(vehicle);
                    }
                }
            }
            else
            {
                foreach (Vehicle vehicle in vehiclesList)
                {
                    if (!IsRented(vehicle.GetVehicleProperties()[0]))
                    {
                        Vehicles.Add(vehicle);
                    }
                }
            }

            return Vehicles;
        } // end of GetRentedVehicles

        // returns the vehicle with the input registration
        public Vehicle GetVehicle(string registration)
        {
            foreach (Vehicle vehicle in vehiclesList)
            {
                if (vehicle.GetVehicleProperties()[0] == registration)
                {
                    return vehicle;
                }
            }

            // will never go to this because of prior tests in program
            return vehiclesList[0];
        } // end of GetVehicle


        // returns whether the vehicle with the provided registration is currently
        // being rented
        public bool IsRented(string registration)
        {
            foreach (List<string> set in rentalRegoID)
            {
                // if registration is equal to the input registration
                if (set[0] == registration)
                {
                    return true;
                }
            }

            // if no matching registration is found
            return false;
        } // end of IsRented


        // returns whether the customer with the provided customer ID is currently
        // renting a vehicle
        public bool IsRenting(int customerID)
        {
            foreach (List<string> set in rentalRegoID)
            {
                // if ID is equal to the input ID
                if (int.Parse(set[1]) == customerID)
                {
                    return true;
                }
            }

            // if no matching ID is found
            return false;
        } // end of IsRenting


        // returns the customer ID of the current renter of the vehicle. If it is
        // rented by no one, it returns -1
        public int RentedBy(string registration)
        {
            foreach (var set in rentalRegoID)
            {
                // if the registration matches the input registration
                if (set[0] == registration)
                {
                    // return the matching customer ID
                    return (int.Parse(set[1]));
                }
            }

            // else convey the vehicle is not being rented
            return -1;
        } // end of RentedBy


        // returns a string that displays the rental status of a customer with specific details of what vehicle they are renting
        public string CustomerRentingString(int customerID)
        {
            foreach (Vehicle vehicle in vehiclesList)
            {
                // if the vehicle is being rented by this person
                if (RentedBy(vehicle.GetVehicleProperties()[0]) == customerID)
                {
                    return ($"renting a { vehicle.GetVehicleProperties()[2] } with registration of {vehicle.GetVehicleProperties()[0]}");
                }
            }

            return ("The customer is not renting any vehicles.");
        } // end of RentedBy


        // return rental cost of a given vehicle based of registration
        public double RentalCost(string registration)
        {
            return double.Parse((GetVehicle(registration)).GetVehicleProperties()[10]);
        } // End of return rental cost


        // rents the vehicle with the provided registration to the customer with
        // the provided ID, if the vehicle is not currently being rented. It returns true if
        // the rental was possible, and false otherwise.
        public bool RentVehicle(string registration, int customerID)
        {
            List<string> newRental = new List<string>();
            newRental.Add(registration);
            newRental.Add(customerID.ToString());

            if (IsRented(registration) && IsRenting(customerID))
            {
                return false;
            }
            else
            {
                rentalRegoID.Add(newRental);
                return true;
            }
        } // End of RentVehicle


        // returns a vehicle from a rental, returns true if successful and false if not.
        public bool ReturnVehicle(string registration, int customerID)
        {
            foreach (List<string> set in rentalRegoID)
            {
                // Check if registration and id are a set within rental list
                if (set.Contains(registration) && RentedBy(registration) == customerID)
                {
                    rentalRegoID.RemoveAt(rentalRegoID.IndexOf(set));
                    return true;
                }
            }
            return false;
        } // end of RenturnVehicle


        // constructs a display table with all rentals, registration of vehicles and customer ID's
        public int RentalReportToTable()
        {
            
            if (rentalRegoID.Count >= 1)
            {
                foreach (List<string> set in rentalRegoID)
                {
                    set.Add((GetVehicle(set[0]).GetVehicleProperties()[10]).ToString());
                }
                new TableDisplay(rentalRegoID);
                return 1;
            }
            return 0;
        } // end of RentalReportToTable


        // the initial loading method for the fleet and rentals files
        public bool FleetFileSetup(string fleetFilePath = @"..\..\Data\fleet.csv")
        {
            this.fleetFilePath = fleetFilePath;

            // if the fleet file exists then check if the rental file exists and load each file accordingly
            if (File.Exists(fleetFilePath))
            {
                LoadVehiclesFromFile(fleetFilePath);
                return true;
            }
            else
            {
                this.fleetFilePath = fleetFilePath;
                File.WriteAllText(this.fleetFilePath, fleetFileHeader);
                return false;
            } 
        }


        // the initial loading method for the fleet and rentals files
        public bool RentalFileSetup(string rentalsFilePath = @"..\..\Data\rentals.csv")
        {
            this.rentalsFilePath = rentalsFilePath;
            try
            {
                // if the fleet file exists then check if the rental file exists and load each file accordingly
                if (File.Exists(rentalsFilePath))
                {
                    LoadRentalsFromFile(rentalsFilePath);
                    return true;
                }
                else
                {
                    this.rentalsFilePath = rentalsFilePath;
                    File.WriteAllText(this.rentalsFilePath, rentalsFileHeader);
                    return false;
                }
            }
            catch
            {
                throw new IOException();
            }
        }


        // loads the list of vehicles from the fleet file
        public void LoadVehiclesFromFile(string filePath)
        {
            var read = new StreamReader(File.OpenRead(filePath));
            List<String> lines = new List<string>();
            List<string> linesValues;

            while (!read.EndOfStream)
            {
                lines.Add(read.ReadLine());
            }

            // header removal from csv file
            if (lines[0] == fleetFileHeader)
            {
                lines.Remove(fleetFileHeader);
            } 

            // splitting of list and values
            for (int lineNumber = 0; lineNumber < lines.Count(); lineNumber++)
            {
                linesValues = new List<string>();
                foreach (string value in lines[lineNumber].Split(','))
                {
                    linesValues.Add(value.Trim());
                }

                // All enum parsing
                VehicleGradeEnum vehicleGradeInput= (VehicleGradeEnum)Enum.Parse(typeof(VehicleGradeEnum), linesValues[1]);
                TransmissionTypeEnum transmissionTypeInput = (TransmissionTypeEnum)Enum.Parse(typeof(TransmissionTypeEnum), linesValues[6]);
                FuelTypeEnum fuelTypeInput = (FuelTypeEnum)Enum.Parse(typeof(FuelTypeEnum), linesValues[7]);

                vehiclesList.Add(new Vehicle(
                    linesValues[0], vehicleGradeInput, linesValues[2], linesValues[3], int.Parse(linesValues[4]), int.Parse(linesValues[5]),
                    transmissionTypeInput, fuelTypeInput, bool.Parse(linesValues[8]), bool.Parse(linesValues[9]), double.Parse(linesValues[10]), 
                    linesValues[11] ));
            }
            read.Close();
        } // end of LoadVehiclesFromFile


        // loads the list of rentals from the rentals file
        public void LoadRentalsFromFile(string filePath)
        {
            var read = new StreamReader(File.OpenRead(filePath));
            List<String> lines = new List<string>();

            // add all lines from rental csv file to lines list
            while (!read.EndOfStream)
            {
                lines.Add(read.ReadLine());
            }

            // header removal from csv file
            if (lines[0] == rentalsFileHeader)
            {
                lines.Remove(rentalsFileHeader);
            } 

            // splitting of list and values
            for (int lineNumer = 0; lineNumer < lines.Count(); lineNumer++)
            {
                string[] individualValues = lines[lineNumer].Split(',');
                rentalRegoID.Add(new List<string> (){ individualValues[0], individualValues[1] });
            }
            read.Close();
        } // end of LoadRentalsFromFile


        // saves the current list of vehicles to fleet file
        public void SaveFleetToFile()
        {
            string saveFileString = fleetFileHeader;

            foreach (Vehicle vehicle in vehiclesList)
            {
                vehicle.ToString();
                saveFileString += "\n" + vehicle.ToCSVString();
            }
            File.WriteAllText(this.fleetFilePath, saveFileString);
        } // end of SaveFleetToFile


        // saves the current list of rentals to rentals file
        public void SaveRentalsToFile()
        {
            string saveFileString = rentalsFileHeader;

            foreach (List<string> set in rentalRegoID)
            {
                saveFileString += "\n" + set[0] + "," + int.Parse(set[1]);
            }
            File.WriteAllText(this.rentalsFilePath, saveFileString);
        } // end of SaveRentalsToFile
    }
}