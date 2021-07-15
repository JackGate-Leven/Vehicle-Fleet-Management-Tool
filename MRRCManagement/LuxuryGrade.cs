namespace MRRCManagement
{
    /// Holds constructor for Luxury grade vehicles with Luxury 
    /// default properties.
    public class LuxuryGrade : Vehicle
    {
        // constructor for Luxury vehicle, subclass of Vehicle
        public LuxuryGrade(string inputRegistration, VehicleGradeEnum inputGrade, string inputMake, string inputModel, int inputYear)
            : base(inputRegistration, inputGrade, inputMake, inputModel, inputYear)
        {
            // luxury defaults
            GPS = true;
            sunRoof = true;
            dailyRate = 120;
        }
    } // end of luxury vehicle constructor
}
