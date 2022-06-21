using System;


namespace NavalBattle
{
    /// <summary>
    /// esta clase toma un string del usuario y lo convierte en otro preparado para su utilizacion
    /// esto lo hace a traves del cambio de string - ascii - string
    /// </summary>
    public class CoordsTranslate
    {

        private string CoordsX { get; set; }
        private string CoordsY { get; set; }

        /// <summary>
        /// transforma un mensaje B2 a 11, A1 a 00
        /// </summary>
        /// <param name="aCoordsLocation"></param>
        /// <returns></returns>
        public string Translate(string aCoordsLocation) //Juan, cambia esto porque no se cual es la variante de la coordenada.
        {
            string Coords = aCoordsLocation.ToUpper(); // A1 //Juan, cambia esto porque no se cual es la variante de la coordenada.

            /// <summary>
            /// coordenada Horizontal
            /// </summary>
            /// <returns></returns>
            CoordsX = Coords[0].ToString(); // A
            
            /// <summary>
            /// coordenada vertical
            /// </summary>
            /// <returns></returns>
            CoordsY = Coords[1].ToString(); // 1

            CoordsX = (char.Parse(CoordsX) - 65).ToString(); // de A a 65 = 0, B a 66 = 1, C a 67 = 2, etc.
            CoordsY = (Int32.Parse(CoordsY) - 1).ToString(); //de 1 = 0, 2 = 1, 3 = 2, etc.

            string CoordsTranslate =  (CoordsX + CoordsY); // 11
            
            return CoordsTranslate;
        }
    }
}