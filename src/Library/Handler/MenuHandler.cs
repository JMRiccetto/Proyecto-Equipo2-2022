using System;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace NavalBattle
{

    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa los comandos "/cambiartablero" y "/bombas".
    /// </summary>
    public class MenuHandler : BaseHandler
    {
        private menuState state;

        private GameUser user;

        private int gameboardSide;

        private bool bombs = false;

        private bool doubleAttack = false;

        /// <summary>
        /// Constructor de MenuHandler.
        /// </summary>
        /// <param name="next">El próximo handler.</param>
        /// <returns>MenuHandler.</returns>
        public MenuHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/cambiartablero", "/bombas", "/ataquedoble"};
            this.state = menuState.Start;
            this.user = null;
        }

        /// <summary>
        /// Procesa los mensajes "/cambiartablero" y "/bombas" y despliega las opciones para prenderlos y apagarlos.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            try
            {   
                if (this.CanHandle(message))
                {   
                    this.user = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());

                    if (this.user.State == GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No puede acceder al menu mientras está en partida");
                    } 

                    if (this.user.State == GameUser.UserState.Waiting)
                    {
                        throw new InvalidStateException("No puede acceder al menu mientras buscando partida\n\nIngrese /cancelar para cancelar la busqueda");
                    }

                    if (this.state != menuState.Start)
                    {
                        throw new InvalidStateException("Ingrese /start para ver el menu de opciones");
                    } 
                    
                    if (message.Text.ToLower().Trim() == "/cambiartablero")
                    {
                        this.state = menuState.Gameboard;
                        response = "Introduce un tamaño para el tablero entre 6-8.";
                        return true;
                    }
                    else if (message.Text.ToLower().Trim() == "/bombas")
                    {
                        this.state = menuState.Bomb;
                        response = "/on\n" + "/off";
                        return true;
                    }
                    else if (message.Text.ToLower().Trim() == "/ataquedoble")
                    {
                        this.state = menuState.DoubleAttack;    
                        response = "ataquedoble cambiado";
                        return true;
                    }
                }
                else if (this.state == menuState.Gameboard)
                {
                    //response = "Si deseas cambiar el tamaño de tu tablero, por favor introduce un número entre 6-8.";
                    if (message.Text.Trim() == "6")
                    {
                        this.user.GameboardSide = 6;
                        this.state = menuState.Start;
                        response = "El tamaño de tu tablero ha sido restablecido a 6.";
                        return true;
                    }
                    else if (message.Text.Trim() == "7")
                    {
                        this.user.GameboardSide = 7;
                        this.state = menuState.Start;
                        response = "El tamaño de tu tablero ha sido restablecido a 7.";
                        return true;
                    }
                    else if (message.Text.Trim() == "8")
                    {
                        this.user.GameboardSide = 8;
                        this.state = menuState.Start;
                        response = "El tamaño de tu tablero ha sido restablecido a 8.";
                        return true;
                    }
                    else
                    {
                        this.user.GameboardSide = 6;
                        this.state = menuState.Start;
                        response = "No se pudo registrar tu mensaje, el tamaño del tablero será cambiado a 6";
                        return true;
                    }
                }
                else if (this.state == menuState.Bomb)
                {
                    if (message.Text.ToLower().Trim() == "/off")
                    {
                        this.user.Bombs = false;
                        this.state = menuState.Start;
                        response = "Las bombas han sido desactivadas.";
                        return true;
                    }
                    else if (message.Text.ToLower().Trim() == "/on")
                    {
                        this.user.Bombs = true;
                        this.state = menuState.Start;
                        response = "Las bombas han sido activadas.";
                        return true;
                    }
                    else
                    {
                        this.state = menuState.Start;
                        response = "No se pudo registrar su mensaje, el estado de las bombas sigue igual";
                        return true;
                    }
                }
                else if (this.state == menuState.DoubleAttack)
                {
                    if (this.user.DoubleAttack)
                    {
                        this.user.DoubleAttack = false;
                        this.state = menuState.Start;
                        response = "El ataque doble ha sido desactivado.";
                        return true;
                    }
                    this.user.DoubleAttack = true;
                    this.state = menuState.Start;
                    response = "El ataque doble ha sido activado.";
                    return true;
                }
                response = string.Empty;
                return false;
            }
            catch(NullReferenceException ne)
            {
                response = "Ingrese /start para acceder al menu de opciones.";

                return true;
            }
            catch(Exception e)
            {
                Cancel();
                response = e.Message;
                return true;
            }
        }

        protected override void InternalCancel()
        {
            this.state = menuState.Start;
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial. En los "handler" sin estado no hace nada. Los "handlers" que
        /// procesan varios mensajes cambiando de estado entre mensajes deben sobreescribir este método para volver al
        /// estado inicial.
        /// </summary>
        public override void Cancel()
        {
            this.InternalCancel();
            if (this.Next != null)
            {
                this.Next.Cancel();
            }
        }
        /// <summary>
        /// Estado del menu.
        /// </summary>
        private enum menuState
        {
            Start,
            Gameboard,
            Bomb,
            DoubleAttack,
        }
    }
}
