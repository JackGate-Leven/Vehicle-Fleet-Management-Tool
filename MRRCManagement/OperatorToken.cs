using System;
using System.Collections.Generic;
using System.Linq;

namespace MRRCManagement
{
    // for operator tokens AND and OR, for use during VehicleSearch
    public class OperatorToken : IToken
    {
        public string Value;
        public int Precedence { get; set; }
        public OperatorToken(string Operator, int precedence)
        {
            this.Value = Operator;
            Precedence = precedence;
        }

        public override string ToString()
        {
            return Value;
        }

        // for user as a evaluation, such as Blue and Automatic with the operator token being AND or OR
        public ValueToken Evaluate(ValueToken a, ValueToken b, List<string> vehicleProperties)
        {
            if (Value == "AND")
            {
                if ((vehicleProperties.Contains(a.ToString(), StringComparer.OrdinalIgnoreCase) || a.ToString() == "TRUE") 
                    && (vehicleProperties.Contains(b.ToString(), StringComparer.OrdinalIgnoreCase)) || b.ToString() == "TRUE")
                {
                    return new ValueToken("TRUE");
                }
                else
                {
                    return new ValueToken("FALSE");
                }
            }
            else
                if ((vehicleProperties.Contains(a.ToString(), StringComparer.OrdinalIgnoreCase) || a.ToString() == "TRUE")
                    || (vehicleProperties.Contains(b.ToString(), StringComparer.OrdinalIgnoreCase)) || b.ToString() == "TRUE")
                {
                    return new ValueToken("TRUE");
                }
                else
                {
                    return new ValueToken("FALSE");
            }
        }
    }
}