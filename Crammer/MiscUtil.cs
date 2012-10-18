using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Crammer
{
    class MiscUtil
    {

        /// <summary>
        /// Safely converts a decimal string to a float taking into consideration the potential differences
        /// between the two different decimal deimiter characters, period and comma. This function is needed
        /// since users with different locale settings might want to exchange dictionaries.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float safeFloatConvert(string val)
        {
            if (string.IsNullOrEmpty(val))
                return (CrammerDictionary.DEFAULT_FONT_SIZE);

            float fval = 0;
            if (float.TryParse(val, out fval) == false)
            {
                StringBuilder sb = new StringBuilder(val);
                string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                if (val.Contains(",") && separator == ".")
                {
                    sb.Replace(",", ".");
                    fval = float.Parse(sb.ToString());
                }
                else if (val.Contains(".") && separator == ",")
                {
                    sb.Replace(".", ",");
                    fval = float.Parse(sb.ToString());
                }
                else
                {
                    fval = CrammerDictionary.DEFAULT_FONT_SIZE;
                }
            }
            return (fval);
        }

    }
}
