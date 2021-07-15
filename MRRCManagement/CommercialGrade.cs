namespace MRRCManagement
{
    /// Holds constructor for Commercial grade vehicles with commercial 
    /// default properties.
    public class CommercialGrade : Vehicle
    {
        // constructor for Commercial vehicle
        public CommercialGrade(string inputRegistration, VehicleGradeEnum inputGrade, string inputMake, string inputModel, int inputYear) 
            : base(inputRegistration, inputGrade, inputMake, inputModel, inputYear)
        {
            // commercial defaults
            fuelType = FuelTypeEnum.Diesel;
            dailyRate = 130;
        }
    } // end of commercial vehicle constructor
}
