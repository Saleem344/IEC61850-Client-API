using System;
using IEC61850.Common;


namespace common_functions
{
    public class data_extract
    {
        public dynamic ExtractValue(dynamic value)
        {
            switch (value.GetType())
            {
                case MmsType.MMS_BOOLEAN:
                    return value.GetBoolean();
                case MmsType.MMS_INTEGER:
                    return value.ToInt32();
                case MmsType.MMS_FLOAT:
                    return value.ToFloat ();
                case MmsType.MMS_BIT_STRING:
                    return Convert.ToInt16(value.ToString(), 2);
                case MmsType.MMS_UTC_TIME:
                    return value.GetUtcTimeAsDateTimeOffset();
                case MmsType.MMS_STRUCTURE:
                    return ExtractValue(value.GetElement(0));
                default:
                    return "";
            }
        }
        public dynamic ExtractFC(string value)
        {
            dynamic result = null;
            if (value == "ST")
                result = FunctionalConstraint.ST;
            else if (value == "MX")
                result = FunctionalConstraint.MX;
            else if (value == "SP")
                result = FunctionalConstraint.SP;
            else if (value == "SV")
                result = FunctionalConstraint.SV;
            else if (value == "CF")
                result = FunctionalConstraint.CF;
            else if (value == "DC")
                result = FunctionalConstraint.DC;
            else if (value == "SG")
                result = FunctionalConstraint.SG;
            else if (value == "SE")
                result = FunctionalConstraint.SE;
            else if (value == "SR")
                result = FunctionalConstraint.SR;
            else if (value == "OR")
                result = FunctionalConstraint.OR;
            else if (value == "BL")
                result = FunctionalConstraint.BL;
            else if (value == "EX")
                result = FunctionalConstraint.EX;
            else if (value == "CO")
                result = FunctionalConstraint.CO;
            else if (value == "US")
                result = FunctionalConstraint.US;
            else if (value == "MS")
                result = FunctionalConstraint.MS;
            else if (value == "RP")
                result = FunctionalConstraint.RP;
            else if (value == "BR")
                result = FunctionalConstraint.BR;
            else if (value == "LG")
                result = FunctionalConstraint.LG;
            else if (value == "ALL")
                result = FunctionalConstraint.ALL;
            else if (value == "NONE")
                result = FunctionalConstraint.NONE;
            else
                result = "";
            return result;
        }
    }
}