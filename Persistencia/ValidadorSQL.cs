using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Interfaces;
using System.Data;

namespace Persistencia
{

    public class ValidadorSQL : Validador
    {
        public string ErrorA { get; set; } = "Error 1";
        public string ErrorB { get; set; } = "Error 2";
        public string ErrorC { get; set; } = "Error 3";

        public Dictionary<string, object> Parametros { get; set; } = new Dictionary<string, object>();
        public int ValorRetornado { get; private set; }

        public override bool  Validar()
        {
            //Verificamos que no este vacio
            if(Valor.ToString() == "")
            {
                return false;
            }
            //Crea el comando
            ComandoSQL comando = new ComandoSQL(Valor.ToString());

            //Verifica y agrega los parametros definido
            if(Parametros.Count != 0)
            {
                foreach(KeyValuePair<string, object> par in Parametros)
                {
                    comando.AgregarParametro(par.Key, par.Value);
                }
            }

            //Ejecuta
            ValorRetornado = comando.Ejecutar();

            //Compara los validadores descomponiendo los tipos de error
            switch (ValorRetornado)
            {
                //Todo salio bien
                case 0:
                    return true;
                //Fallo el primer validador
                case 1:
                    Mensaje = ErrorA;
                    break;
                //Fallo el segundo validador
                case 2:
                    Mensaje = ErrorB;
                    break;
                //Fallo el primer y segundo validador
                case 3:
                    Mensaje = ErrorA + ", ademas " + ErrorB;
                    break;
                //Fallo el tercer validador
                case 4:
                    Mensaje = ErrorC;
                    break;
                //Fallo el primer y tercer validador
                case 5:
                    Mensaje = ErrorA + ", ademas " + ErrorC;
                    break;
                //Fallo el tercer y segundo validador
                case 6:
                    Mensaje = ErrorB + ", ademas " + ErrorC;
                    break;
                //Fallo el primer, segundo y tercer validador
                case 7:
                    Mensaje = ErrorA + ", " + ErrorB + ", y ademas " + ErrorC ;
                    break;
                //Fuera de los parametros
                default:
                    throw new Exception("Fuera de logica soportada");
            }
            return false;
        }
    }
}
