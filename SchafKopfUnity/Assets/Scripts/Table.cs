using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
  public SchafkopfController SchafkopfController;


  void OnMouseDown()
  {
    SchafkopfController.PutCardOnTable(SchafkopfController.GetSelectedCard());
  }

  public void AddCard(GameObject card)
  {

  }
}
