using System;


namespace NavalBattle
{
    public class  CoordsTranslate
    {

        private string CoordsX { get; set; }
        private string CoordsY { get; set; }

        //toma como entrada un mensaje del usuario y lo traduce a coordenadas del tablero.
        public string Translate(string aCoordsLocation) //Juan, cambia esto porque no se cual es la variante de la coordenada.
        {
            string Coords = aCoordsLocation; // A1 //Juan, cambia esto porque no se cual es la variante de la coordenada.

            CoordsX = Coords[0].ToString(); // A
            CoordsY = Coords[1].ToString(); // 1

            CoordsX = (char.Parse(CoordsX) - 65).ToString();
            CoordsY = (Int32.Parse(CoordsY) - 1).ToString();

            string translatedCoords =  (CoordsX + CoordsY); // 11
            
            return translatedCoords;
        }
    }
}