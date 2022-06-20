using System;


namespace NavalBattle
{
    public class CoordsTranslate
    {

        private string CoordsX { get; set; }
        private string CoordsY { get; set; }

        //toma como entrada un mensaje del usuario y lo traduce a coordenadas del tablero.
        public string Translate(string aCoordsLocation) //Juan, cambia esto porque no se cual es la variante de la coordenada.
        {
            string Coords = aCoordsLocation.ToUpper(); // A1 //Juan, cambia esto porque no se cual es la variante de la coordenada.

            CoordsX = Coords[0].ToString(); // A
            CoordsY = Coords[1].ToString(); // 1

            CoordsX = (char.Parse(CoordsX) - 65).ToString(); // de A a 65 = 0, B a 66 = 1, C a 67 = 2, etc.
            CoordsY = (Int32.Parse(CoordsY) - 1).ToString(); //de 1 = 0, 2 = 1, 3 = 2, etc.

            string CoordsTranslate =  (CoordsX + CoordsY); // 11
            
            return CoordsTranslate;
        }
    }
}