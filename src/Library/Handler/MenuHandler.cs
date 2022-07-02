using System;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace NavalBattle
{

    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class MenuHandler : BaseHandler
    {
        public MenuState State;

        public GameUser User;

        public MenuHandlerData Data;

        public int gameboardSide;

        public bool bombs = false;

        public bool doubleAttack;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public MenuHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/menu", "/cambiartablero", "/bombas", "/ataquedoble"};
            this.State = MenuState.Start;
            this.User = null;
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            try
            {
                if (this.State == MenuState.Start && this.CanHandle(message))
                {
                    this.User = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());
                    
                    if (message.Text.ToLower().Trim() == "/cambiartablero")
                    {
                        this.State = MenuState.Gameboard;
                        response = "Introduce un tamaño para el tablero entre 6-8.";
                        return true;
                    }
                    else if (message.Text.ToLower().Trim() == "/bombas")
                    {
                        this.State = MenuState.Bomb;
                        response = "/on\n" + "/off";
                        return true;
                    }
                    else if (message.Text.ToLower().Trim() == "/ataquedoble")
                    {
                        this.State = MenuState.DoubleAttack;    
                        response = "ataquedoble cambiado";
                        return true;
                    }
                }
                else if (this.State == MenuState.Gameboard)
                {
                    //response = "Si deseas cambiar el tamaño de tu tablero, por favor introduce un número entre 6-8.";
                    if (message.Text.Trim() == "6")
                    {
                        this.User.GameboardSide = 6;
                        this.State = MenuState.Start;
                        response = "El tamaño de tu tablero ha sido restablecido a 6.";
                        return true;
                    }
                    else if (message.Text.Trim() == "7")
                    {
                        this.User.GameboardSide = 7;
                        this.State = MenuState.Start;
                        response = "El tamaño de tu tablero ha sido restablecido a 7.";
                        return true;
                    }
                    else if (message.Text.Trim() == "8")
                    {
                        this.User.GameboardSide = 8;
                        this.State = MenuState.Start;
                        response = "El tamaño de tu tablero ha sido restablecido a 8.";
                        return true;
                    }
                    else
                    {
                        this.User.GameboardSide = 6;
                        this.State = MenuState.Start;
                        response = "No se pudo registrar tu mensaje, el tamaño del tablero será cambiado a 6";
                        return true;
                    }
                }
                else if (this.State == MenuState.Bomb)
                {
                    if (message.Text.ToLower().Trim() == "/off")
                    {
                        this.User.Bombs = false;
                        this.State = MenuState.Start;
                        response = "Las bombas han sido desactivadas.";
                        return true;
                    }
                    else if (message.Text.ToLower().Trim() == "/on")
                    {
                        this.User.Bombs = true;
                        this.State = MenuState.Start;
                        response = "Las bombas han sido activadas.";
                        return true;
                    }
                    else
                    {
                        this.State = MenuState.Start;
                        response = "No se pudo registrar su mensaje, el estado de las bombas sigue igual";
                        return true;
                    }
                }
                else if (this.State == MenuState.DoubleAttack)
                {
                    if (this.User.DoubleAttack)
                    {
                        this.User.DoubleAttack = false;
                        this.State = MenuState.Start;
                        response = "El ataque doble ha sido desactivado.";
                        return true;
                    }
                    this.User.DoubleAttack = true;
                    this.State = MenuState.Start;
                    response = "El ataque doble ha sido activado.";
                    return true;
                }
                response = string.Empty;
                return false;
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
            this.State = MenuState.Start;
            this.Data = new MenuHandlerData();
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

        public enum MenuState
        {
            Start,
            BotGame,
            Gameboard,
            Bomb,
            DoubleAttack,
        }

        public class MenuHandlerData
        {

        }
    }
}
