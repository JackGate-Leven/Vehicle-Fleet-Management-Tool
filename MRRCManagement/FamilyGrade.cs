namespace MRRCManagement
{
    /// Holds constructor for Family grade vehicles with Family 
    /// default properties.
    public class FamilyGrade : Vehicle
    {
        // constructor for Family vehicle, subclass of Vehicle
        public FamilyGrade(string inputRegistration, VehicleGradeEnum inputGrade, string inputMake, string inputModel, int inputYear)
            : base(inputRegistration, inputGrade, inputMake, inputModel, inputYear)
        {
            // family defaults
            dailyRate = 80;
        }
    } // end of family vehicle constructor
}
