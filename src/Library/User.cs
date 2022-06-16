using System;
using System.Collections.Generic;
using System.IO;

namespace NavalBattle
{
    public class User   //Esta clase va a manejar un nickname del usuario y la funcionalidad de MatchMaking
    {
        //se crea la clase User, se agrega el atributo de NickName y el metodo MatchMaking()

        public string NickName      //apodo del usuario
        {
            get
            {
                return this.NickName;
            }
            private set
            {
                this.NickName = NickName != null ? NickName : value;
            }
        }

        public User(string nickName)    //constructor de la clase User, solo tiene nickname como parametro
        {
            this.NickName = nickName;
        }

        public void MatchMaking() //hay que ver exactamente que hace esta funcion
        {
            //generar codigo para que el otro usuario se conecte a una partida
        }
    }
}