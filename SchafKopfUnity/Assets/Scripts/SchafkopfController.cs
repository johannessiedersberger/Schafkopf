using System.Runtime.InteropServices;
using UnityEngine;
using Schafkopf;
using System;
using System.Linq;
using System.Collections.Generic;

public class SchafkopfController : MonoBehaviour
{
  public Sprite[] CardFaces;
  private Dictionary<CardValues, Sprite> ValueToSprite = new Dictionary<CardValues, Sprite>();

  // Start is called before the first frame update
  void Start()
  {
    SchafkopfGame game = new SchafkopfGame();
    SetCardValueToSprite(CardFaces, ValueToSprite);

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