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
  public GameObject[] Players;
  public Dictionary<CardValues, Sprite> ValueToSprite = new Dictionary<CardValues, Sprite>();

  // Start is called before the first frame update
  void Start()
  {
    SchafkopfGame game = new SchafkopfGame();
    SetCardValueToSprite(CardFaces, ValueToSprite);
    StartCoroutine(DistributeCards(game, CardPrefab, Players, ValueToSprite));
  }

  private static IEnumerator<WaitForSeconds> DistributeCards(SchafkopfGame game, GameObject CardPrefab, GameObject[] Players, Dictionary<CardValues, Sprite> ValueToSprite)
  {
    float yOffset = 0;
    float zOffset = 0.03f;

    for (int i = 0; i < game.PlayerList.Count(); i++)
    {
      yOffset = 0.3f;
      zOffset = 0.03f;

      for (int j = 0; j < game.PlayerList[i].Cards.Count(); j++)
      {
        yield return new WaitForSeconds(0.05f);
        CardPrefab.GetComponent<SpriteRenderer>().sprite = ValueToSprite[game.PlayerList[i].Cards[j].CardValue];
        GameObject newCard = GameObject.Instantiate(
          CardPrefab,
          new Vector3(Players[i].transform.position.x, Players[i].transform.position.y - yOffset, Players[i].transform.position.z - zOffset),
          Quaternion.identity,
          Players[i].transform
          );
        yOffset = yOffset + 0.3f;
        zOffset = zOffset + 0.03f;
      }
    }
  }

  private static void SetCardValueToSprite(Sprite[] cardFaces, Dictionary<CardValues, Sprite> dictionary)
  {
    var cardValues = Enum.GetValues(typeof(CardValues)).Cast<CardValues>().ToArray();
    for (int i = 0; i < cardFaces.Length; i++)
      dictionary.Add(cardValues[i], cardFaces[i]);
  }

  // Update is called once per frame
  void Update()
  {

  }

}