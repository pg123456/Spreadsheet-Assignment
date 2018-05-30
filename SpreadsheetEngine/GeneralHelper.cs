/********************
* Name: Patrick Guo
* ID: 11378369
********************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /************************************************************
    * A utility class with static tools for general
    * utility purposes.  
    ************************************************************/
    public class GeneralHelper
    {
        public static bool CharIsNumber(char value)
        {
            if (value >= '0' && value <= '9')
                return true;

            return false;
        }

        public static bool CharIsAlphabet(char value)
        {
            if ((value >= 'A' && value <= 'Z') ||
                    (value >= 'a' && value <= 'z'))
                return true;

            return false;
        }

        public static bool StringIsDouble(string value)
        {
            try
            {
                double value_as_double = Convert.ToDouble(value);
            }
            catch { return false; }

            return true;
        }

        public static bool StringIsVariable(string value)
        {
            char[] value_as_char_array = value.ToCharArray();

            if (null == value || value.Length < 1 || !CharIsAlphabet(value_as_char_array[0]))
                return false;
            for (int i = 1; i < value_as_char_array.Length; i++)
                if (!CharIsNumber(value_as_char_array[i]) && !CharIsAlphabet(value_as_char_array[i]))
                    return false;

            return true;
        }

        public static bool StringIsOperator(string value)
        {
            return ("+" == value || "-" == value || "*" == value || "/" == value);
        }
    }
}
