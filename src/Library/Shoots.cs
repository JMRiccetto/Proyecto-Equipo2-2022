using System;

namespace NavalBattle
{
    /// <summary>
    /// Cuenta la cantidad de disparos cuyo resultado es Agua o Tocado.
    /// La clase Player cuenta con un atributo de tipo Disparos. Esto es porque Player posee la información
    /// de cual fue el resultado de su ataque.  
    /// El atributo de tipo Disparos podría tenerlo Gameboard también, pero si así fuera Gameboard 
    /// tendría demasiadas responsabilidades.
    /// También estos dos atributos podrían esta en Player y no en una clase aparte pero si esto fuera así,
    /// cada vez que se quiera agregar otro atributo que cuente por ejemplo la cantidad de bomas a las que se
    /// atacaron, Player quedaría con muchos atributos. 
    /// </summary>
    public class Disparos
    {
        private int waterShoots = 0;

        private int shipShoots = 0;

        public int WaterShoots 
        {
            get
            {
                return waterShoots;
            }
        }

        public int ShipShoots 
        {
            get
            {
                return shipShoots;
            }
        }

        /// <summary>
        /// Incrementa la cantidad de disparos al agua.
        /// </summary>
        public void IncWaterShoots()
        {
            waterShoots++;
        }

        /// <summary>
        /// Incrementa la cantidad de disparos a barcos.
        /// </summary>
        public void IncShipShoots()
        {
            shipShoots++;
        }
    }
}