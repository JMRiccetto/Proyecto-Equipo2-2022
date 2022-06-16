namespace NavalBattle
{
    public class  CoordsTranslate
    {

        private string CoordsX { get; set; }
        private string CoordsY { get; set; }

        //toma como entrada un mensaje del usuario y lo traduce a coordenadas del tablero.
        public string Translate(string aCoordsLocation)
        {
            string Coords = aCoordsLocation; // A1
            CoordsX = Coords[0].ToString(); // A
            CoordsY = Coords[1].ToString(); // 1

            TranslateX(CoordsX);
            TranslateY(CoordsY);

            string translatedCoords =  (TranslateX + TranslateY); // 11
            
            return translatedCoords;
        }

        public string TranslateX(string CoordsX)
        {
            if (CoordsX == "A")
            {
                return "0";
            }
            else if (CoordsX == "B")
            {
                return "1";
            }
            else if (CoordsX == "C")
            {
                return "2";
            }
            else if (CoordsX == "D")
            {
                return "3";
            }
            else if (CoordsX == "E")
            {
                return "4";
            }
            else if (CoordsX == "F")
            {
                return "5";
            }
            else if (CoordsX == "G")
            {
                return "6";
            }
            else
            {
                return "7";
            }
        }        
        public string TranslateY(string CoordsY)
        {
            if (CoordsY == "1")
            {
                return "0";
            }
            else if (CoordsY == "2")
            {
                return "1";
            }
            else if (CoordsY == "3")
            {
                return "2";
            }
            else if (CoordsY == "4")
            {
                return "3";
            }
            else if (CoordsY == "5")
            {
                return "4";
            }
            else if (CoordsY == "6")
            {
                return "5";
            }
            else if (CoordsY == "7")
            {
                return "6";
            }
            else
            {
                return "7";
            }
        }
    }
}