using System;

namespace MRRCManagement
{
    // exception for parsing text query in rental search into tokens to be analysed
    public class TextParseException : Exception
    {
        public TextParseException(string text) : base($"Error: Expression '{text}' cannot be queried.")
        {
        }
    }

    // exception for when mismatched parenthesis are used within a rental serach query
    public class ShuntingYardException : Exception
    {
        public ShuntingYardException() : base("Error: Mismatched parenthesis in expression.")
        {
        }
    }
}
