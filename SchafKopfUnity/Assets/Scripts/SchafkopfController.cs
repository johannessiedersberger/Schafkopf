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
  public Transform[] PlayerStackPositions;
  public Transform Table;
  public List<List<GameObject>> CardLists = new List<List<GameObject>>();
  public List<GameObject> CardTable = new List<GameObject>();
  public Dictionary<CardValues, Sprite> ValueToSprite = new Dictionary<CardValues, Sprite>();

  // Start is called before the first frame update
  void Start()
  {
    SchafkopfGame game = new SchafkopfGame();
    SetCardValueToSprite(CardFaces, ValueToSprite);
    DistributeCards(game, CardPrefab, PlayerStackPositions, ValueToSprite, CardLists);
    
  }

  private static void SetCardValueToSprite(Sprite[] cardFaces, Dictionary<CardValues, Sprite> dictionary)
  {
    var cardValues = Enum.GetValues(typeof(CardValues)).Cast<CardValues>().ToArray();
    for (int i = 0; i < cardFaces.Length; i++)
      dictionary.Add(cardValues[i], cardFaces[i]);
  }

  private void DistributeCards(SchafkopfGame game, GameObject CardPrefab, Transform[] fieldPositions, Dictionary<CardValues, Sprite> ValueToSprite, List<List<GameObject>> CardLists)
  {
    for (int i = 0; i < game.PlayerList.Count(); i++)
    {
      StartCoroutine(CreatePlayerStack(game.PlayerList[i], fieldPositions[i]));
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
      GameObject newCard = InstantiateCard(player, player.Cards[j].CardValue,CardPrefab, fieldPos, xOffset, zOffset);     
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
    foreach(var list in CardLists)
    {
      var cardsToDeselect = list.Where(c => c != card).Select(ca => ca.GetComponent<UnityCard>());
      foreach(var c in cardsToDeselect)
      {
        c.IsSelected = false;
        c.ChangeColor(false);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

}