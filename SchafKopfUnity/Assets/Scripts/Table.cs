using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
  public SchafkopfController SchafkopfController;


  void OnMouseDown()
  {
    SchafkopfController.PutCardOnTable(SchafkopfController.GetSelectedCard());
    SchafkopfController.DeselectAllCards();
  }

  public void AddCard(GameObject card)
  {

  }
}
