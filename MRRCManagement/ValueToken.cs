namespace MRRCManagement
{
    // the ValueToken holds properties of vehicles for use in VehicleSearch
    public class ValueToken : IToken
    {
        public string Value;

        // constructor for a ValueToken, takes input for its value
        public ValueToken(string value)
        {
            this.Value = value;
        }

        // sends the tokens value as output
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
