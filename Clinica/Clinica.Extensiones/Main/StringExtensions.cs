using Clinica.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Extensiones.Main
{
    /// <summary>
    /// Manejo de extensiones para tipo string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Limitar una cadena de acuerdo a un largo definido
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string LimitLength(this string source, int length)
        {
            return source.Substring(0, Math.Min(length, source.Length));
        }

        /// <summary>
        /// Parse separated string of date and time
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="strTime"></param>
        /// <param name="dateFormat"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        public static DateTime ParseDateAndTime(string strDate, string strTime, string dateFormat, string timeFormat)
        {
            DateTime result = DateTime.MinValue;

            //If date and format exists, then try to parse on DateTime
            if (!string.IsNullOrEmpty(strDate) && !string.IsNullOrEmpty(strTime))
            {
                //Build unified Date and format
                var unifiedDate = strDate + CommonConstants.STR_WHITESPACE + strTime;
                var unifiedFormat = dateFormat + CommonConstants.STR_WHITESPACE + timeFormat;

                //Perform conversion
                result = unifiedDate.SimpleParseExact(unifiedFormat);
            }
            //Also try to parse if only date
            else if (!string.IsNullOrEmpty(strDate) && string.IsNullOrEmpty(strTime))
            {
                result = strDate.SimpleParseExact(dateFormat);
            }
            //Try to parse if only time
            else if (string.IsNullOrEmpty(strDate) && !string.IsNullOrEmpty(strTime))
            {
                result = strTime.SimpleParseExact(timeFormat);
            }

            return result;
        }

        /// <summary>
        /// Prase string to DateTime
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime SimpleParseExact(this string source, string format)
        {
            return string.IsNullOrEmpty(source) ? DateTime.MinValue : DateTime.ParseExact(source, format, null);
        }
    }
}
