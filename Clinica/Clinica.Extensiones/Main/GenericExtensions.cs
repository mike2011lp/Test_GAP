using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Extensiones.Main
{
    /// <summary>
    /// Extensiones para el tipo DbSet
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Extensión para definir si un objeto está vacio
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmpty<TSource>(this IQueryable<TSource> source)
        {
            return source == null || (source != null && !source.Any());
        }

        /// <summary>
        /// Extensión para definir si un objeto no está vacio
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNotEmpty<TSource>(this IQueryable<TSource> source)
        {
            return source != null && source.Any();
        }
    }
}
