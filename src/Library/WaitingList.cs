using System.Collections.Generic;

namespace NavalBattle
{
    public class WaitingList
    {
        private static List<GameUser> noBombsNoDoubleAttackSize6 = new List<GameUser>();
        private static List<GameUser> noBombsNoDoubleAttackSize7;
        private static List<GameUser> noBombsNoDoubleAttackSize8;
        private static List<GameUser> noBombsDoubleAttackSize6;
        private static List<GameUser> noBombsDoubleAttackSize7;
        private static List<GameUser> noBombsDoubleAttackSize8;
        private static List<GameUser> bombsNoDoubleAttackSize6;
        private static List<GameUser> bombsNoDoubleAttackSize7;
        private static List<GameUser> bombsNoDoubleAttackSize8;
        private static List<GameUser> bombsDoubleAttackSize6 = new List<GameUser>();
        private static List<GameUser> bombsDoubleAttackSize7;
        private static List<GameUser> bombsDoubleAttackSize8;



        public static List<GameUser> NoBombsNoDoubleAttackSize6
        {
            get
            {
                return noBombsNoDoubleAttackSize6;
            }
            
            set
            {
                noBombsNoDoubleAttackSize6 = value;
            }
        }
        public static List<GameUser> NoBombsNoDoubleAttackSize7
        {
            get
            {
                return noBombsNoDoubleAttackSize7;
            }
            
            set
            {
                noBombsNoDoubleAttackSize7 = value;
            }
        }
        public static List<GameUser> NoBombsNoDoubleAttackSize8
        {
            get
            {
                return noBombsNoDoubleAttackSize8;
            }
            
            set
            {
                noBombsNoDoubleAttackSize8 = value;
            }
        }
        public static List<GameUser> NoBombsDoubleAttackSize6
        {
            get
            {
                return noBombsDoubleAttackSize6;
            }
            
            set
            {
                noBombsDoubleAttackSize6 = value;
            }
        }
        public static List<GameUser> NoBombsDoubleAttackSize7
        {
            get
            {
                return noBombsDoubleAttackSize7;
            }
            
            set
            {
                noBombsDoubleAttackSize7 = value;
            }
        }
        public static List<GameUser> NoBombsDoubleAttackSize8
        {
            get
            {
                return noBombsDoubleAttackSize8;
            }
            
            set
            {
                noBombsDoubleAttackSize8 = value;
            }
        }
        public static List<GameUser> BombsNoDoubleAttackSize6
        {
            get
            {
                return bombsNoDoubleAttackSize6;
            }
            
            set
            {
                bombsNoDoubleAttackSize6 = value;
            }
        }
        public static List<GameUser> BombsNoDoubleAttackSize7
        {
            get
            {
                return bombsNoDoubleAttackSize7;
            }
            
            set
            {
                bombsNoDoubleAttackSize7 = value;
            }
        }
        public static List<GameUser> BombsNoDoubleAttackSize8
        {
            get
            {
                return bombsNoDoubleAttackSize8;
            }
            
            set
            {
                bombsNoDoubleAttackSize8 = value;
            }
        }
        public static List<GameUser> BombsDoubleAttackSize6
        {
            get
            {
                return bombsDoubleAttackSize6;
            }
            
            set
            {
                bombsDoubleAttackSize6 = value;
            }
        }
        public static List<GameUser> BombsDoubleAttackSize7
        {
            get
            {
                return bombsDoubleAttackSize7;
            }
            
            set
            {
                bombsDoubleAttackSize7 = value;
            }
        }
        public static List<GameUser> BombsDoubleAttackSize8
        {
            get
            {
                return bombsDoubleAttackSize8;
            }
            
            set
            {
                bombsDoubleAttackSize8 = value;
            }
        }

        public WaitingList()
        {
            noBombsNoDoubleAttackSize6 = new List<GameUser>();
            noBombsNoDoubleAttackSize7 = new List<GameUser>();
            noBombsNoDoubleAttackSize8 = new List<GameUser>();
            noBombsDoubleAttackSize6 = new List<GameUser>();
            noBombsDoubleAttackSize7 = new List<GameUser>();
            noBombsDoubleAttackSize8 = new List<GameUser>();
            bombsNoDoubleAttackSize6 = new List<GameUser>();
            BombsNoDoubleAttackSize7 = new List<GameUser>();
            bombsNoDoubleAttackSize8 = new List<GameUser>();
            bombsDoubleAttackSize7 = new List<GameUser>();
            bombsDoubleAttackSize8 = new List<GameUser>();
        }
    }
}