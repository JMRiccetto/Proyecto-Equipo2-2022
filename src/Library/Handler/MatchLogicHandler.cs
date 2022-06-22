using System;
using System.Text;
using Telegram.Bot.Types;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class MatchLogicHandler : BaseHandler
    {
        public MatchLogicState State;

        public int ShipLength;

        public MatchLogicData Data;

        public MatchLogicHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/gamestart"};
            this.State = MatchLogicState.Start;
            this.ShipLength = 2;
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
                if (this.State == MatchLogicState.Start)
                {
                    /* this.Data.Match = BotUtils.MatchList[0];
                    this.Data.User1 = this.Data.Match.Users[0];
                    this.Data.User2 = this.Data.Match.Users[1];
                    BotUtils.MatchList.Remove(this.Data.Match);
                    BotUtils.InGameMatchList.Add(this.Data.Match); */
                    this.State = MatchLogicState.PlayerOneTurnPlacementCoords;
                    response = "Que empiece el juego! Jugador 1, elija sus coordenadas inciales.";
                }
                else if (this.State == MatchLogicState.PlayerOneTurnPlacementCoords && message.Text.ToLower().Trim() == "/gamestart")
                {
                    if (ShipLength <= 4)
                    {   
                        /* if (message.Text == "/visualizar")
                        {
                            
                        }  */
                        this.Data.PlayerOnePlacementCoords = message.ToString();
                        this.State = MatchLogicState.PlayerOneTurnPlacementDirection;
                        response = "Elige una dirección.";
                        return true;
                    }
                    else
                    {
                        ShipLength = 2;
                        this.State = MatchLogicState.PlayerTwoTurnPlacementCoords;
                        response = "Turno del jugador 2, elija sus coordenadas iniciales.";
                        return true;
                    }
                }
                else if (this.State == MatchLogicState.PlayerOneTurnPlacementDirection && message.Text.ToLower().Trim() == "/gamestart")
                {
                    this.Data.PlayerOnePlacementDirection = message.ToString();
                    this.Data.User1.Gameboard.AddShip(this.ShipLength, this.Data.PlayerOnePlacementCoords, this.Data.PlayerOnePlacementDirection);
                    this.State = MatchLogicState.PlayerOneTurnPlacementCoords;
                    response = $"Barco creado. Barcos por crear {4 - this.ShipLength}, elija unas nuevas coordenadas.";
                    this.ShipLength++;
                    return true;
                }
                else if (this.State == MatchLogicState.PlayerTwoTurnPlacementCoords)
                {
                    if (ShipLength <= 4)
                    {
                        this.Data.PlayerTwoPlacementCoords = message.ToString();
                        this.State = MatchLogicState.PlayerTwoTurnPlacementDirection;
                        response = "Elige una dirección,";
                        return true;
                    }
                    else
                    {
                        this.State = MatchLogicState.PlayerOneTurnAttack;
                        response = "Es hora de atacar, Jugador uno apunte a unas coordenadas.";
                        return true;
                    }
                }
                else if (this.State == MatchLogicState.PlayerTwoTurnPlacementDirection)
                {
                    this.Data.PlayerTwoPlacementDirection = message.ToString();
                    this.Data.User2.Gameboard.AddShip(this.ShipLength, this.Data.PlayerTwoPlacementCoords, this.Data.PlayerTwoPlacementDirection);
                    this.State = MatchLogicState.PlayerTwoTurnPlacementCoords;
                    response = $"Barco creado. Barcos por crear {4 - this.ShipLength}, elija unas nuevas coordenadas.";
                    this.ShipLength++;
                    return true;
                }
                else if (this.State == MatchLogicState.PlayerOneTurnAttack)
                {
                    this.Data.CoordToAttack = message.ToString();
                    Coords coordToAttack = new Coords(this.Data.CoordToAttack);
                    this.Data.User2.Gameboard.RecieveAttack(coordToAttack);
                    this.Data.AttackPrinter = new AttackGameboardPrinter();
                    this.Data.AttackPrinter.PrintGameboard(this.Data.User1.Gameboard);
                    this.Data.DefensePrinter = new DefenseGameboardPrinter();
                    this.Data.DefensePrinter.PrintGameboard(this.Data.User2.Gameboard);
                    this.State = MatchLogicState.PlayerTwoTurnAttack;
                    response = "Turno del jugador 2, apunte a unas coordenadas.";
                    return true;
                }
                else if (this.State == MatchLogicState.PlayerTwoTurnAttack)
                {
                    if (!this.Data.User1.Gameboard.IsMatchFinished() && !this.Data.User2.Gameboard.IsMatchFinished())
                    {
                        this.Data.CoordToAttack = message.ToString();
                        Coords coordToAttack = new Coords(this.Data.CoordToAttack);
                        this.Data.User1.Gameboard.RecieveAttack(coordToAttack);
                        this.Data.AttackPrinter = new AttackGameboardPrinter();
                        this.Data.AttackPrinter.PrintGameboard(this.Data.User2.Gameboard);
                        this.Data.DefensePrinter = new DefenseGameboardPrinter();
                        this.Data.DefensePrinter.PrintGameboard(this.Data.User1.Gameboard);
                        this.State = MatchLogicState.PlayerOneTurnAttack;
                        response = "Turno del jugador 1, apunte a unas coordenadas.";
                        return true;
                    }
                    else if (this.Data.User1.Gameboard.IsMatchFinished())
                    {
                        response = $"Juego terminado, el ganador es {this.Data.User2}";
                        return false;
                    }
                    else if (this.Data.User2.Gameboard.IsMatchFinished())
                    {
                        response = $"Juego terminado, el ganador es {this.Data.User1}";
                        return false;
                    }
                }
                response = string.Empty;
                return false;
            }
            
            catch (Exception e)
            {
                Cancel();
                response = e.Message;
                return true;
            }
        }

        protected override void InternalCancel()
        {
            this.State = MatchLogicState.Start;
            this.Data = new MatchLogicData();
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

        public enum MatchLogicState
        {
            Start,
            PlayerOneTurnPlacementCoords,
            PlayerOneTurnPlacementDirection,
            PlayerTwoTurnPlacementCoords,
            PlayerTwoTurnPlacementDirection,
            PlayerOneTurnAttack,
            PlayerTwoTurnAttack,
        }

        public class MatchLogicData
        {
            public Match Match;

            public GameUser User1;
            
            public GameUser User2;

            public string PlayerOnePlacementCoords { get; set; }

            public string PlayerOnePlacementDirection { get; set; }

            public string PlayerTwoPlacementCoords { get; set; }

            public string PlayerTwoPlacementDirection { get; set; }
            
            public string CoordToAttack { get; set; }

            public IPrinter AttackPrinter;

            public IPrinter DefensePrinter;
        }
    }
}