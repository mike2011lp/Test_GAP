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
    }
}
