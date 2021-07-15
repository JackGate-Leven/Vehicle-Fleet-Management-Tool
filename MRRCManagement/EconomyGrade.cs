namespace MRRCManagement
{
    /// Holds constructor for Economy grade vehicles with Economy 
    /// default properties.
    public class EconomyGrade : Vehicle
    {
        // constructor for Economy vehicle, subclass of Vehicle
        public EconomyGrade(string inputRegistration, VehicleGradeEnum inputGrade, string inputMake, string inputModel, int inputYear)
            : base(inputRegistration, inputGrade, inputMake, inputModel, inputYear)
        {
            // economy defaults
            transmissionType = TransmissionTypeEnum.Automatic;
        }
    } // end of economy vehicle constructor
}
