using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Macros {

    public class MacroBranchNode : MacroNode {
        //================================================================================
        public enum ConditionType {
            ALWAYS = 0,
            EQUALS = 1,
            GREATER_THAN = 2,
            GREATER_THAN_OR_EQUAL = 3,
            LESS_THAN = 4,
            LESS_THAN_OR_EQUAL = 5,
            CONTAINS = 6,
            DOES_NOT_CONTAIN = 7,
            STARTS_WITH = 8,
            DOES_NOT_START_WITH = 9,
            IS_AN_INTEGER = 10,
            IS_NOT_AN_INTEGER = 11,
            IS_A_DECIMAL = 12,
            IS_NOT_A_DECIMAL = 13,
            IS_A_CURRENCY = 14,
            IS_NOT_A_CURRENCY = 15
        }


        //================================================================================
        private ConditionType                   mCondition = ConditionType.ALWAYS;
        private string                          mConditionArgument = "";

        private bool                            mFinal = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroBranchNode() : this(ConditionType.ALWAYS, "", false) { }

        //--------------------------------------------------------------------------------
        public MacroBranchNode(ConditionType condition, string conditionArgument, bool final) {
            mCondition = condition;
            mConditionArgument = conditionArgument;
        }


        // PLAYING ================================================================================
        //--------------------------------------------------------------------------------
        public override bool Play(MacroPlayer player) {
            Console.WriteLine("Branch[" + mCondition + ", " + mConditionArgument + "]");

            // Notify
            player.NotifyPlayingElement(this);

            // Play
            return PlayElements(player);
        }
        

        // HIERARCHY ================================================================================
        //--------------------------------------------------------------------------------
        public MacroBranchingNode BranchingParent { get { return (MacroBranchingNode)base.Parent; } }
        

        // CONDITION ================================================================================
        //--------------------------------------------------------------------------------
        public ConditionType Condition { get { return mCondition; } }
        public string ConditionArgument { get { return mConditionArgument; } }
        

        // FINAL ================================================================================
        //--------------------------------------------------------------------------------
        public bool Final { get { return mFinal; } }


        // CONDITIONS ================================================================================
        //--------------------------------------------------------------------------------
        public bool MeetsCondition(string value) {
            // Checks
            if (mCondition == ConditionType.ALWAYS)
                return true;
            else if ((value == null) || (mConditionArgument == null))
                return false;

            // Condition
            switch (mCondition) {
                case ConditionType.EQUALS:
                    return UString.EqualNumeric(value, mConditionArgument, StringComparison.InvariantCultureIgnoreCase);

                case ConditionType.GREATER_THAN:
                    return UString.GreaterThanNumeric(value, mConditionArgument, StringComparison.InvariantCultureIgnoreCase);

                case ConditionType.GREATER_THAN_OR_EQUAL:
                    return UString.GreaterThanOrEqualNumeric(value, mConditionArgument, StringComparison.InvariantCultureIgnoreCase);

                case ConditionType.LESS_THAN:
                    return UString.LessThanNumeric(value, mConditionArgument, StringComparison.InvariantCultureIgnoreCase);

                case ConditionType.LESS_THAN_OR_EQUAL:
                    return UString.LessThanOrEqualNumeric(value, mConditionArgument, StringComparison.InvariantCultureIgnoreCase);

                case ConditionType.CONTAINS:
                    return value.Contains(mConditionArgument);

                case ConditionType.DOES_NOT_CONTAIN:
                    return !value.Contains(mConditionArgument);

                case ConditionType.STARTS_WITH:
                    return value.StartsWith(mConditionArgument);

                case ConditionType.DOES_NOT_START_WITH:
                    return !value.StartsWith(mConditionArgument);

                case ConditionType.IS_AN_INTEGER:
                    return UString.IsInteger(value);

                case ConditionType.IS_NOT_AN_INTEGER:
                    return !UString.IsInteger(value);

                case ConditionType.IS_A_DECIMAL:
                    return UString.IsDecimal(value);

                case ConditionType.IS_NOT_A_DECIMAL:
                    return !UString.IsDecimal(value);

                case ConditionType.IS_A_CURRENCY:
                    return UString.IsCurrency(value);

                case ConditionType.IS_NOT_A_CURRENCY:
                    return !UString.IsCurrency(value);

                default:
                    return false;
            }
        }

        //--------------------------------------------------------------------------------
        public static string ConditionString(ConditionType condition) {
            switch (condition) {
                case ConditionType.ALWAYS:                  return "Always";
                case ConditionType.EQUALS:                  return "Equals";
                case ConditionType.GREATER_THAN:            return "Greater than";
                case ConditionType.GREATER_THAN_OR_EQUAL:   return "Greater than or equal";
                case ConditionType.LESS_THAN:               return "Less than";
                case ConditionType.LESS_THAN_OR_EQUAL:      return "Less than or equal";
                case ConditionType.CONTAINS:                return "Contains";
                case ConditionType.DOES_NOT_CONTAIN:        return "Does not contain";
                case ConditionType.STARTS_WITH:             return "Starts with";
                case ConditionType.DOES_NOT_START_WITH:     return "Does not start with";
                case ConditionType.IS_AN_INTEGER:           return "Is an integer";
                case ConditionType.IS_NOT_AN_INTEGER:       return "Is not an integer";
                case ConditionType.IS_A_DECIMAL:            return "Is a decimal";
                case ConditionType.IS_NOT_A_DECIMAL:        return "Is not a decimal";
                case ConditionType.IS_A_CURRENCY:           return "Is a currency";
                case ConditionType.IS_NOT_A_CURRENCY:       return "Is not a currency";
                default:                                    return "INVALID";
            }
        }

        //--------------------------------------------------------------------------------
        public static ConditionType[] ConditionList() {
            Array conditions = Enum.GetValues(typeof(ConditionType));
            ConditionType[] conditionList = new ConditionType[conditions.Length];
            for (int i = 0; i < conditions.Length; ++i) {
                conditionList[i] = (ConditionType)conditions.GetValue(i);
            }
            return conditionList;
        }

        //--------------------------------------------------------------------------------
        public static string[] ConditionStringList() {
            ConditionType[] conditionList = ConditionList();
            string[] stringList = new string[conditionList.Length];
            for (int i = 0; i < conditionList.Length; ++i) {
                stringList[i] = ConditionString(conditionList[i]);
            }
            return stringList;
        }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public override Color Colour { get { return Color.FromArgb(255, 189, 125); } }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public override string[] Information {
            get {
                // Type
                string type = "Branch (" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ConditionString(mCondition)) + ")";

                // Parameters
                string parameters = "";
                if (!string.IsNullOrEmpty(mConditionArgument))
                    parameters += "condition argument: '" + mConditionArgument + "'";

                // Return
                return new string[] { type, parameters };
            }
        }

        
        // JSON ================================================================================
        //--------------------------------------------------------------------------------
        public override void WriteJSON(JsonTextWriter writer) {
            // Fields
            writer.WritePropertyName("condition"); writer.WriteValue((int)mCondition);
            writer.WritePropertyName("condition_argument"); writer.WriteValue(mConditionArgument);
            writer.WritePropertyName("final"); writer.WriteValue(mFinal);

            // Node
            base.WriteJSON(writer);
        }

        //--------------------------------------------------------------------------------
        public override void ReadJSON(JToken token) {
            // Fields
            mCondition = (ConditionType)((int)token.SelectToken("condition"));
            mConditionArgument = (string)token.SelectToken("condition_argument");
            mFinal = (bool)token.SelectToken("final");

            // Node
            base.ReadJSON(token);
        }
    }

}
