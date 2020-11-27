using System;
using IEC61850.Common;


namespace common_functions
{
    public class data_extract
    {
        public dynamic ExtractValue(dynamic value)
        {
            
            dynamic result = null;
            string Parse;
            if (value.ToString().Contains("{") || value.ToString().Contains("}"))
            {

                Parse = value.ToString().Replace("{", "").Replace("}", "");

                if(value.GetType() == MmsType.MMS_BOOLEAN)
                    result = bool.Parse(value.ToString());
                else if(value.GetType() == MmsType.MMS_INTEGER)
                    result = int.Parse(value.ToString());
                else if(value.GetType() == MmsType.MMS_FLOAT)
                    result = float.Parse(value.ToString());
                else if(value.GetType() == MmsType.MMS_BIT_STRING)
                    result = Convert.ToInt16(value.ToString(), 2);
                else if(value.GetType() == MmsType.MMS_UTC_TIME)
                    result = value.GetUtcTimeAsDateTimeOffset();
                else if(value.GetType() == MmsType.MMS_STRUCTURE)
                    result = float.Parse(Parse.ToString());
                else
                    result = value.ToString();

            }
            else
            {
                
                if(value.GetType() == MmsType.MMS_BOOLEAN)
                    result = bool.Parse(value.ToString());
                else if(value.GetType() == MmsType.MMS_INTEGER)
                    result = int.Parse(value.ToString());
                else if(value.GetType() == MmsType.MMS_FLOAT)
                    result = float.Parse(value.ToString());
                else if(value.GetType() == MmsType.MMS_BIT_STRING)
                    result = Convert.ToInt16(value.ToString(), 2);
                else if(value.GetType() == MmsType.MMS_UTC_TIME)
                    result = value.GetUtcTimeAsDateTimeOffset();
                else
                    result = value.ToString();
            }
            return result;
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
                result = "Invalid Function Code";
            return result;
        }
    }
}