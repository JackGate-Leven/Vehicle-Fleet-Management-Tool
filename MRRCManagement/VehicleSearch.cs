using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MRRCManagement
{
    public class VehicleSearch
    {
        // Take a string mathematical expression and use it to build a list of tokens.
        public static List<IToken> ParseText(string text)
        {
            List<IToken> returnList = new List<IToken>();

            // split the input string into its token parts
            string regexPattern = @"(OR)|(AND)|([)(])";
            string[] sets = Regex.Split(text, regexPattern);

            // turn each string set into its corresponding token
            foreach (var value in sets)
            {
                // see if token is an operator
                if (value.Equals("AND", StringComparison.OrdinalIgnoreCase))
                {
                    returnList.Add(new OperatorToken("AND", 2));
                } 
                else if (value.Equals("OR", StringComparison.OrdinalIgnoreCase))
                {
                    returnList.Add(new OperatorToken("OR", 1));
                }
                else if (value == "(")
                {
                    returnList.Add(new LeftParenthesisToken());
                }
                else if (value == ")")
                {
                    returnList.Add(new RightParenthesisToken());
                }
                else
                {
                    returnList.Add(new ValueToken(value.Trim()));
                } // check what type of token the value is and assign it
            }

            return returnList;
        }

        // Takes a list of tokens in infix order (Red, AND, Automatic) and 
        // turns them into a postfix order using shunting yard algorithm(Red, Automatic, AND).
        public static List<IToken> ShuntingYard(List<IToken> postfixTokens)
        {
            List<IToken> infixTokens = new List<IToken>();
            Stack<IToken> operatorStack = new Stack<IToken>();

            try
            {
                foreach (IToken token in postfixTokens)
                {
                    if (token is ValueToken)
                    {
                        infixTokens.Add(token);
                    }
                    else if (token is OperatorToken)
                    {
                        while (operatorStack.Any() && operatorStack.Peek() is OperatorToken
                            && ((OperatorToken)operatorStack.Peek()).Precedence >= ((OperatorToken)token).Precedence)
                        {
                            infixTokens.Add(operatorStack.Pop());
                        }
                        operatorStack.Push(token);
                    }
                    else if (token is LeftParenthesisToken)
                    {
                        operatorStack.Push(token);
                    }
                    else if (token is RightParenthesisToken)
                    {
                        while (!(operatorStack.Peek() is LeftParenthesisToken))
                        {
                            infixTokens.Add(operatorStack.Pop());
                        }
                        operatorStack.Pop();
                    }
                }
                while (operatorStack.Any())
                {
                    infixTokens.Add(operatorStack.Pop());
                }
            }
            catch
            {
                throw new ShuntingYardException();
            }

            return infixTokens;
        }


        // Takes a list of tokens in postfix and evaluates them to return a double (result).
        public static List<Vehicle> Evaluate(List<IToken> postfixToken, List<Vehicle> vehicles)
        {
            postfixToken.RemoveAll(token => token.ToString() == "");

            IToken valA;
            IToken valB;
            OperatorToken Operator;
            Stack<IToken> stack = new Stack<IToken>();
            List<string> vehicleProperties;
            List<Vehicle> results = new List<Vehicle>();
            string match;

            foreach (Vehicle vehicle in vehicles)
            {
                vehicleProperties = new List<string> (vehicle.GetVehicleProperties());
                match = "False";

                // if rentals search is for an individual property
                if (postfixToken.Count == 1)
                {
                    if (vehicleProperties.Contains(postfixToken[0].ToString()))
                    {
                        match = "TRUE";
                    }
                }
                else
                {
                    // check filters to find vehicles that match search
                    for (int i = 0; i < postfixToken.Count; i++)
                    {
                        if (postfixToken[i] is OperatorToken)
                        {
                            if (stack.Count <= 1)
                            {
                                //throw new Exception();
                            }
                            else
                            {
                                valA = stack.Pop();
                                valB = stack.Pop();
                                Operator = (OperatorToken)postfixToken[i];
                                match = Operator.Evaluate((ValueToken)valB, (ValueToken)valA, vehicleProperties).Value;
                            }
                        }
                        else
                        {
                            stack.Push(postfixToken[i]);
                        }   
                    }
                }
                if (match == "TRUE")
                {
                    results.Add(vehicle);
                }
            }
            return results;
        }
    }
}
