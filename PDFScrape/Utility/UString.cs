using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Utility {

    public static class UString {
        // CONVERSION ================================================================================
        //--------------------------------------------------------------------------------
        public static int ToInteger(string value, int defaultValue = 0) {
            // Remove white space and commas
            StringBuilder builder = new StringBuilder();

            foreach (char c in value) {
                if (c == '.')
                    break;
                else if (char.IsDigit(c))
                    builder.Append(c);
            }

            string integerString = builder.ToString();

            // Convert
            if (string.IsNullOrEmpty(integerString))
                return defaultValue;
            return Int32.Parse(integerString);
        }

        //--------------------------------------------------------------------------------
        public static decimal ToDecimal(string value, decimal defaultValue = 0) {
            // Remove white space and commas
            StringBuilder builder = new StringBuilder();

            foreach (char c in value) {
                if (char.IsDigit(c) || (c == '.'))
                    builder.Append(c);
            }

            string decimalString = builder.ToString();

            // Convert
            if (string.IsNullOrEmpty(decimalString))
                return defaultValue;
            return Decimal.Parse(decimalString);
        }

        //--------------------------------------------------------------------------------
        // Converts the value after the first decimal point to an integer.
        public static int ToFractional(string value, int defaultValue = 0) {
            // Remove white space and commas
            StringBuilder builder = new StringBuilder();
            bool foundDecimalPoint = false;

            foreach (char c in value) {
                if (foundDecimalPoint && char.IsDigit(c))
                    builder.Append(c);
                else if (c == '.')
                    foundDecimalPoint = true;
            }

            string fractionalString = builder.ToString();

            // Convert
            if (string.IsNullOrEmpty(fractionalString))
                return defaultValue;
            return Int32.Parse(fractionalString);
        }


        // STRINGS ================================================================================
        //--------------------------------------------------------------------------------
        public static bool IsInteger(string value) {
            bool hasDigits = false;

            foreach (char c in value) {
                if (char.IsDigit(c))
                    hasDigits = true; 
                else if (!char.IsWhiteSpace(c) && c != ',')
                    return false;
            }

            return hasDigits;
        }

        //--------------------------------------------------------------------------------
        public static bool IsDecimal(string value) {
            bool hasDigits = false;
            bool hasDot = false;

            foreach (char c in value) {
                if (char.IsDigit(c))
                    hasDigits = true;
                else if (c == '.')
                    hasDot = true;
                else if (!char.IsWhiteSpace(c) && c != ',')
                    return false;
            }

            return hasDigits && hasDot;
        }

        //--------------------------------------------------------------------------------
        public static bool IsNumeric(string value) {
            return (IsInteger(value) || IsDecimal(value));
        }
        
        //--------------------------------------------------------------------------------
        public static bool IsCurrency(string value) {
            char[] currencySymbols = new char[] {'$', '£', '€'};            
            bool hasCurrencySymbol = false;
            bool hasDigits = false;

            foreach (char c in value) {
                if (currencySymbols.Contains(c))
                    hasCurrencySymbol = true;
                else if (char.IsDigit(c))
                    hasDigits = true;
                else if ((c != '.') && !char.IsWhiteSpace(c) && c != ',')
                    return false;
            }

            return hasCurrencySymbol && hasDigits;
        }
        
        //--------------------------------------------------------------------------------
        public static int WordCount(string value) {
            bool lastWasWhitespace = true;
            int wordCount = 0;

            foreach (char c in value) {
                if (!char.IsWhiteSpace(c)) {
                    lastWasWhitespace = true;
                }
                else {
                    if (lastWasWhitespace)
                        ++wordCount;
                    lastWasWhitespace = false;
                }
            }

            return wordCount;
        }

        
        // COMPARISON ================================================================================
        //--------------------------------------------------------------------------------
        public static bool EqualNumeric(string first, string second, StringComparison comparisonType) {
            // Checks
            if ((first == null) || (second == null))
                throw new ArgumentNullException();

            // Types
            bool firstIsInteger = IsInteger(first);
            bool firstIsDecimal = IsDecimal(first);
            bool secondIsInteger = IsInteger(second);
            bool secondIsDecimal = IsDecimal(second);

            // Compare
            if (firstIsInteger && secondIsInteger)
                return (ToInteger(first) == ToInteger(second));
            else if (firstIsDecimal && secondIsDecimal)
                return (ToDecimal(first) == ToDecimal(second));
            else if (firstIsInteger && secondIsDecimal)
                return (ToInteger(first) == ToDecimal(second));
            else if (firstIsDecimal && secondIsInteger)
                return (ToDecimal(first) == ToInteger(second));
            else {
                Console.WriteLine("'" + first + "' = '" + second + "' : " + first.Equals(second, comparisonType));

                return first.Equals(second, comparisonType);
            }
        }

        //--------------------------------------------------------------------------------
        public static bool GreaterThanNumeric(string first, string second, StringComparison comparisonType) {
            // Checks
            if ((first == null) || (second == null))
                throw new ArgumentNullException();

            // Types
            bool firstIsInteger = IsInteger(first);
            bool firstIsDecimal = IsDecimal(first);
            bool secondIsInteger = IsInteger(second);
            bool secondIsDecimal = IsDecimal(second);

            // Compare
            if (firstIsInteger && secondIsInteger)
                return (ToInteger(first) > ToInteger(second));
            else if (firstIsDecimal && secondIsDecimal)
                return (ToDecimal(first) > ToDecimal(second));
            else if (firstIsInteger && secondIsDecimal)
                return (ToInteger(first) > ToDecimal(second));
            else if (firstIsDecimal && secondIsInteger)
                return (ToDecimal(first) > ToInteger(second));
            else
                return (string.Compare(first, second, comparisonType) > 0);
        }

        //--------------------------------------------------------------------------------
        public static bool GreaterThanOrEqualNumeric(string first, string second, StringComparison comparisonType) {
            // Checks
            if ((first == null) || (second == null))
                throw new ArgumentNullException();

            // Types
            bool firstIsInteger = IsInteger(first);
            bool firstIsDecimal = IsDecimal(first);
            bool secondIsInteger = IsInteger(second);
            bool secondIsDecimal = IsDecimal(second);

            // Compare
            if (firstIsInteger && secondIsInteger)
                return (ToInteger(first) >= ToInteger(second));
            else if (firstIsDecimal && secondIsDecimal)
                return (ToDecimal(first) >= ToDecimal(second));
            else if (firstIsInteger && secondIsDecimal)
                return (ToInteger(first) >= ToDecimal(second));
            else if (firstIsDecimal && secondIsInteger)
                return (ToDecimal(first) >= ToInteger(second));
            else
                return (string.Compare(first, second, comparisonType) >= 0);
        }

        //--------------------------------------------------------------------------------
        public static bool LessThanNumeric(string first, string second, StringComparison comparisonType) {
            // Checks
            if ((first == null) || (second == null))
                throw new ArgumentNullException();

            // Types
            bool firstIsInteger = IsInteger(first);
            bool firstIsDecimal = IsDecimal(first);
            bool secondIsInteger = IsInteger(second);
            bool secondIsDecimal = IsDecimal(second);

            // Compare
            if (firstIsInteger && secondIsInteger)
                return (ToInteger(first) < ToInteger(second));
            else if (firstIsDecimal && secondIsDecimal)
                return (ToDecimal(first) < ToDecimal(second));
            else if (firstIsInteger && secondIsDecimal)
                return (ToInteger(first) < ToDecimal(second));
            else if (firstIsDecimal && secondIsInteger)
                return (ToDecimal(first) < ToInteger(second));
            else
                return (string.Compare(first, second, comparisonType) < 0);
        }

        //--------------------------------------------------------------------------------
        public static bool LessThanOrEqualNumeric(string first, string second, StringComparison comparisonType) {
            // Checks
            if ((first == null) || (second == null))
                throw new ArgumentNullException();

            // Types
            bool firstIsInteger = IsInteger(first);
            bool firstIsDecimal = IsDecimal(first);
            bool secondIsInteger = IsInteger(second);
            bool secondIsDecimal = IsDecimal(second);

            // Compare
            if (firstIsInteger && secondIsInteger)
                return (ToInteger(first) <= ToInteger(second));
            else if (firstIsDecimal && secondIsDecimal)
                return (ToDecimal(first) <= ToDecimal(second));
            else if (firstIsInteger && secondIsDecimal)
                return (ToInteger(first) <= ToDecimal(second));
            else if (firstIsDecimal && secondIsInteger)
                return (ToDecimal(first) <= ToInteger(second));
            else
                return (string.Compare(first, second, comparisonType) <= 0);
        }
    }

}
