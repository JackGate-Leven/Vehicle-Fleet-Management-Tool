using System.Collections.Generic;

namespace MRRCManagement
{
    // Vehicle Enums
    public enum VehicleGradeEnum
    {
        Economy = 0,
        Family = 1,
        Luxury = 2,
        Commercial = 3
    }

    public enum TransmissionTypeEnum
    {
        Automatic = 0,
        Manual = 1
    }
    public enum FuelTypeEnum
    {
        Petrol = 0,
        Diesel = 1
    }
    // End of Vehicle enums


    /// <summary>
    /// The vehicle class contains the constructor for vehicle objects and the methods 
    /// for returning vehicle properties. The vehicle class is a representation and a holder of a vehicles
    /// properties. This is the base class for CustomVehicle, EconomyVehicle, FamilyVehicle, CommercialVehicle 
    /// and LuxuryVehicle. The Vehicle constructor sets all the vehicles properties to defaults ones so the
    /// sub classes can override whichever ones are called. All the vehicles properties are also initialised here
    /// and the required enums are defined in this file but not within this class.
    /// </summary>
    public class Vehicle
    {
        protected string registration { set; get; }
        protected string  make { set; get; }
        protected string model { set; get; }
        protected string colour { set; get; }
        protected int year { set; get; }
        protected int numSeats { set; get; }
        protected bool GPS { set; get; }
        protected bool sunRoof { set; get; }
        protected double dailyRate { set; get; }
        protected VehicleGradeEnum VehicleGrade { set; get; }
        protected TransmissionTypeEnum transmissionType { set; get; }
        protected FuelTypeEnum fuelType { set; get; }


        // constructor creates a vehicle with all default values and the given mandatory inputs
        public Vehicle(string inputRegistration, VehicleGradeEnum inputGrade, string inputMake, string inputModel, int inputYear, int inputNumSeats = 4,
                       TransmissionTypeEnum inputTransmission = TransmissionTypeEnum.Manual, FuelTypeEnum inputFuel = FuelTypeEnum.Petrol, bool inputGPS = false,
                       bool inputSunRoof = false, double inputDailyRate = 50, string inputColour = "Black")
        {
            // Required inputs variables for vehicle construction
            registration = inputRegistration;
            VehicleGrade = inputGrade;
            make = inputMake;
            model = inputModel;
            year = inputYear;

            // Default vehicle properties, some will be assigned new values by subclass constructors
            numSeats = inputNumSeats;
            transmissionType = inputTransmission;
            fuelType = inputFuel;
            GPS = inputGPS;
            sunRoof = inputSunRoof;
            dailyRate = inputDailyRate;
            colour = inputColour;
        } // End of Vehicle constructor 


        // returns all the vehicles properties in string form
        public List<string> GetVehicleProperties()
        {
            string GPSstatus = "";
            string sunRoofstatus = "";

            if (GPS == true)
            {
                GPSstatus = "GPS";
            }
            else
            {
                GPSstatus = "no GPS";
            }

            if (sunRoof == true)
            {
                sunRoofstatus = "Sunroof";
            }
            else
            {
                sunRoofstatus = "no Sunroof";
            }

            return new List<string> { registration, VehicleGrade.ToString(), make, model, year.ToString(), numSeats.ToString(), 
                                      transmissionType.ToString(), fuelType.ToString(), GPSstatus, sunRoofstatus,
                                      dailyRate.ToString(), colour};
        } // End of Get Vehicle Properties


        // return a CSV suitable string for saving the vehicle to a csv file
        public string ToCSVString()
        {
            return ($"{registration},{VehicleGrade},{make},{model},{year},{numSeats}," +
                $"{transmissionType},{fuelType},{GPS},{sunRoof},{dailyRate},{colour}");
        }


        // returns the details needed to make a display table for the vehicle
        public List<string> ToTableDisplay()
        {
            return new List<string> { 
                registration, VehicleGrade.ToString(), make, model, year.ToString(), numSeats.ToString(), 
                transmissionType.ToString(), fuelType.ToString(), GPS.ToString(), sunRoof.ToString(), 
                dailyRate.ToString(), colour};
        }
    } // End of vehicle class
}