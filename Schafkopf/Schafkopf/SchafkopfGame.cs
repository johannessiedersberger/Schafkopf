using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schafkopf
{
    
    public enum Card
    {
        //7 8 9 10 U O K A
        E7, E8, E9, E10, EU, EO, EK, EA, //Eichel
        G7, G8, G9, G10, GU, GO, GK, GA, //Gras
        H7, H8, H9, H10, HU, HO, HK, HA, //Herz
        S7, S8, S9, S10, SU, SO, SK, SA, //Schellen
    }

    public class SchafkopfGame
    {
        public List<Player> Players { get; private set; }

        public SchafkopfGame()
        {
            Players = new List<Player>();
            var allCards = Enum.GetValues(typeof(Card)).Cast<Card>().ToList();
            for (int i = 0; i < 4; i++)
            {
                Players.Add(new Player(GetCards(allCards)));
            }
        }

        private static List<Card> GetCards(List<Card> cards)
        {
            Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());

            Card[] cardsForPlayer = new Card[8];
            for (int i = 0; i < 8; i++)
            {
                int cardIndex = random.Next(0, cards.Count() - 1);
                var cardToAdd = cards[cardIndex];
                cards.Remove(cardToAdd);
                cardsForPlayer[i] = cardToAdd;
            }
            return cardsForPlayer.ToList();
        }
    }
}
