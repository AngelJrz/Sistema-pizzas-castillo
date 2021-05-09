using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utilerias
{
    public class AdministradorHash
    {
        /// <summary>
        /// Genera un Hash sobre el texto deseado.
        /// </summary>
        /// <param name="texto">Texto a realizar el Hash.</param>
        /// <returns>Hash del texto pasado.</returns>
        public static string GenerarHash(string texto)
        {
            return BCrypt.Net.BCrypt.HashPassword(texto);
        }

        /// <summary>
        /// Compara el texto sobre un Hash.
        /// </summary>
        /// <param name="texto">Texto sobre el cual se va a comparar.</param>
        /// <param name="textoConHash">Hash que va a ser comparado.</param>
        /// <returns>true si son iguales, false si no lo son.</returns>
        public static bool CompararHash(string texto, string textoConHash)
        {
            bool sonIguales = false;

            if (BCrypt.Net.BCrypt.Verify(texto, textoConHash))
            {
                sonIguales = true;
            }

            return sonIguales;
        }
    }
}
