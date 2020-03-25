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
  public List<List<GameObject>> CardLists = new List<List<GameObject>>();
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

  private static void DistributeCards(SchafkopfGame game, GameObject CardPrefab, GameObject[] fieldPositions, Dictionary<CardValues, Sprite> ValueToSprite, List<List<GameObject>> CardLists)
  {
    for (int i = 0; i < game.PlayerList.Count(); i++)
    {
      var cardList = CreatePlayerStack(fieldPositions[i].transform, CardPrefab, game.PlayerList[i], ValueToSprite);
      CardLists.Add(cardList);
    }
  }

  private static List<GameObject> CreatePlayerStack(
    Transform fieldPos, GameObject CardPrefab,
    Player player,
    Dictionary<CardValues, Sprite> valueToSprite)
  {
    List<GameObject> cardList = new List<GameObject>();
    float xOffset = 0;
    float zOffset = 0;

    for (int j = 0; j < player.Cards.Count(); j++)
    {     
      CardPrefab.GetComponent<SpriteRenderer>().sprite = valueToSprite[player.Cards[j].CardValue];
      GameObject newCard = GameObject.Instantiate(
        CardPrefab,
        new Vector3(fieldPos.position.x + xOffset, fieldPos.position.y, fieldPos.position.z - zOffset),
        Quaternion.identity,
        fieldPos
        );

      newCard.GetComponent<UnityCard>().Owner = player;
      newCard.GetComponent<UnityCard>().CardValue = player.Cards[j].CardValue;

      cardList.Add(newCard);

      xOffset = xOffset + 0.5f;
      zOffset = zOffset + 0.03f;
    }
    return cardList;
  }

  



  

  // Update is called once per frame
  void Update()
  {

  }

}