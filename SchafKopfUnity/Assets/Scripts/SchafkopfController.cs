using System.Runtime.InteropServices;
using UnityEngine;
using Schafkopf;
using System;
using System.Linq;
using System.Collections.Generic;

public class SchafkopfController : MonoBehaviour
{
  public Sprite[] CardFaces;
  public GameObject CardPrefab;
  public GameObject[] PlayerStackPositions;
  public GameObject[] PlayerStichPositions;
  public GameObject Table;
  public List<List<GameObject>> CardLists = new List<List<GameObject>>();
  public Dictionary<CardValues, Sprite> ValueToSprite = new Dictionary<CardValues, Sprite>();
  public GameObject GameResult;

  private int _currentPlayerIndex = 0;

  public SchafkopfGame Game { get; private set; }
  public Sauspiel Sauspiel { get; private set; }

  // Start is called before the first frame update
  void Start()
  {
    Game = new SchafkopfGame();
    Table.GetComponent<Table>().SchafkopfController = this;
    SetCardValueToSprite(CardFaces, ValueToSprite);
    CardLists.Add(new List<GameObject>()); //Tabe list
    DistributeCards(Game, CardPrefab, PlayerStackPositions, ValueToSprite, CardLists);
  }

  #region gamestart
  private static void SetCardValueToSprite(Sprite[] cardFaces, Dictionary<CardValues, Sprite> dictionary)
  {
    var cardValues = Enum.GetValues(typeof(CardValues)).Cast<CardValues>().ToArray();
    for (int i = 0; i < cardFaces.Length; i++)
      dictionary.Add(cardValues[i], cardFaces[i]);
  }

  private void DistributeCards(SchafkopfGame game, GameObject CardPrefab, GameObject[] playerPositions, Dictionary<CardValues, Sprite> ValueToSprite, List<List<GameObject>> CardLists)
  {
    for (int i = 0; i < game.PlayerList.Count(); i++)
    {
      StartCoroutine(CreatePlayerStack(game.PlayerList[i], playerPositions[i].transform));
    }
  }

  private IEnumerator<WaitForSeconds> CreatePlayerStack(Player player, Transform fieldPos)
  {
    List<GameObject> cardList = new List<GameObject>();
    float xOffset = 0;
    float zOffset = 0;

    for (int j = 0; j < player.Cards.Count(); j++)
    {
      yield return new WaitForSeconds(0.05f);

      CardPrefab.GetComponent<SpriteRenderer>().sprite = ValueToSprite[player.Cards[j].CardValue];
      GameObject newCard = InstantiateCard(player, player.Cards[j].CardValue, CardPrefab, fieldPos, xOffset, zOffset);
      newCard.GetComponent<UnityCard>().IsSelectable = false;
      cardList.Add(newCard);

      xOffset = xOffset + 0.5f;
      zOffset = zOffset + 0.03f;
    }
    CardLists.Add(cardList);
  }

  private GameObject InstantiateCard(Player player, CardValues cardvalue, GameObject CardPrefab, Transform fieldPos, float xOffset, float zOffset)
  {
    var card = GameObject.Instantiate(
        CardPrefab,
        new Vector3(fieldPos.position.x + xOffset, fieldPos.position.y, fieldPos.position.z - zOffset),
        Quaternion.identity,
        fieldPos
        );
    card.GetComponent<UnityCard>().Owner = player;
    card.GetComponent<UnityCard>().CardValue = cardvalue;
    card.GetComponent<UnityCard>().SchafKopfController = this;
    return card;
  }
  #endregion

  #region GameSelection

  public void SelectGame(Player selectedPlayer, CardValues selectedCardValue)
  {
    Sauspiel = new Sauspiel(Game, selectedPlayer, selectedCardValue);
    MakeTurn(_currentPlayerIndex);
  }

  #endregion

  #region cardSelection
  public void SelectCard(GameObject card)
  {
    var currentCard = card.GetComponent<UnityCard>();
    if (currentCard.IsSelected)
    {
      currentCard.ChangeColor(false);
      currentCard.IsSelected = false;
    }
    else
    {
      currentCard.ChangeColor(true);
      currentCard.IsSelected = true;
    }
    DeselectAllCardsExcept(card);
  }

  public void DeselectAllCards()
  {
    foreach (var list in CardLists)
    {
      foreach (var card in list)
      {
        card.GetComponent<UnityCard>().IsSelected = false;
        card.GetComponent<UnityCard>().ChangeColor(false);
      }
    }
  }

  public void DeselectAllCardsExcept(GameObject card)
  {
    foreach (var list in CardLists)
    {
      var cardsToDeselect = list.Where(c => c != card).Select(ca => ca.GetComponent<UnityCard>());
      foreach (var c in cardsToDeselect)
      {
        c.IsSelected = false;
        c.ChangeColor(false);
      }
    }
  }

  public GameObject GetSelectedCard()
  {
    foreach (var list in CardLists)
    {
      var selectedCards = list.Where(card => card.GetComponent<UnityCard>().IsSelected);
      if (selectedCards.Count() > 0)
        return selectedCards.First();
    }
    throw new Exception("No Card Found");
  }
  #endregion

  #region round
  public void MakeTurn(int playerIndex)
  {
    EnablePlayerCards(playerIndex);
    DisablePlayersExcept(playerIndex);
  }

  private void EnablePlayerCards(int playerIndex)
  {
    foreach (var card in CardLists[playerIndex + 1])
    {
      card.GetComponent<UnityCard>().IsSelectable = true;
    }
  }

  private void DisablePlayersExcept(int playerIndex)
  {
    for (int i = 0; i < CardLists.Count(); i++)
    {
      if (i == playerIndex + 1)
      {
        // Do nothing
      }
      else
      {
        foreach (var card in CardLists[i])
        {
          card.GetComponent<UnityCard>().IsSelectable = false;
        }
      }
    }
  }


  #endregion
  public void PutCardOnTable(GameObject card)
  {
    if (CardLists.First().Contains(card)) // card is already on the table
      return;

    MoveCardToTablePosition(card);

    MoveCardToTableList(card);
    
    if(CardLists.First().Count() == 4)
    {
      EndTurn();
      if(CardLists.SelectMany(x => x).ToList().Count() == 0)
      {
        Debug.Log("Game is over!!!");
        var pointsByPlayer = Game.CountStichPointsByPlayers();
        var result = GameResult.GetComponent<GameResults>();
        result.SetTextFieldValues(pointsByPlayer.Values.ToArray());
        GameResult.SetActive(true);
        GameObject.FindGameObjectWithTag("Table").SetActive(false);
      }
    }

    MakeTurn(GetNextPlayer());
  }

  private void MoveCardToTablePosition(GameObject card)
  {
    // calculate new position
    var newPos = Table.transform.position;
    newPos.z = card.transform.position.z - CardLists.First().Count() * 0.1f;
    newPos.x += CardLists.First().Count() * 0.4f;
    card.transform.position = newPos;

    card.GetComponent<UnityCard>().IsSelectable = false;
  }

  private void MoveCardToTableList(GameObject card)
  {
    foreach (var cardList in CardLists)
      cardList.Remove(card);
    CardLists.First().Add(card);
  }

  private void EndTurn()
  {
    Card[] cards = CardLists.First().Select(card => Game.GetCardbyValue(card.GetComponent<UnityCard>().CardValue)).ToArray();
    Card firstCard = Game.GetCardbyValue(CardLists.First()[0].GetComponent<UnityCard>().CardValue);
    Card highestCard = Sauspiel.CardComparison(cards, firstCard);

    var winner = highestCard.Owner;
    MoveStichToPlayerStichStack(winner);

    DeselectAllCards();
    CardLists.First().Clear();
    Game.RedistributeStich(cards, highestCard);
   
    _currentPlayerIndex = Game.GetPlayerIndex(winner) - 1;
  }

  private void MoveStichToPlayerStichStack(Player winner)
  {
    foreach (var card in CardLists.First())
    {
      var winnerStichePos = PlayerStichPositions[Game.GetPlayerIndex(winner)];
      card.transform.position = winnerStichePos.transform.position;    
    }   
  }

  private int GetNextPlayer()
  {
    int lastPlayer = _currentPlayerIndex;
    int nextPlayer;
    if (lastPlayer >= 0 && lastPlayer < 3)
    {
      nextPlayer = ++lastPlayer;
    }
    else
    {
      nextPlayer = 0;
    }
    _currentPlayerIndex = nextPlayer;
    return _currentPlayerIndex;
  }

  // Update is called once per frame
  void Update()
  {

  }

}