using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Utilidades
{
    /// <summary>
    /// Una utilidad ideal para los programadores vagos que no quieren escribir uno por uno los atributos de una clase
    /// </summary>
    public class Ver_Propiedades
    {
        /// <summary>
        /// Lo imprime en una linea, separando con <>
        /// </summary>
        public static string En_Linea(object Objeto)
        {
            var props = Objeto.GetType().GetProperties();
            var sb = new StringBuilder();
            foreach (var p in props)
            {
                sb.AppendLine("<" + p.Name + ": " + p.GetValue(Objeto, null) + "> ");
            }
            return sb.ToString();
        }
        /// <summary>
        /// Lo retorna en una lista con nombre y dato
        /// </summary>
       
        public static List<string> En_Listado(object Objeto)
        {
            var props = Objeto.GetType().GetProperties();
            var list = new List<string>();
            foreach (var p in props)
            {
                list.Add(p.Name + ": " + p.GetValue(Objeto, null));
            }
            return list;
        }
        /// <summary>
        /// Recomendado para ultilizar en desarrollo, imprime el nombre de la propiedad, y el valor del mismo (conservando su tipo original)
        /// </summary>
        /// <returns>Diccionario con en nombre de la propiedad y su valor</returns>
        public static Dictionary<string,Object> En_Diccionario(object Objeto)
        {
            var props = Objeto.GetType().GetProperties();
            var list = new Dictionary<string, Object>();
            foreach (var p in props)
            {
                list.Add(p.Name, p.GetValue(Objeto, null));
            }
            return list;
        }
    }
}
