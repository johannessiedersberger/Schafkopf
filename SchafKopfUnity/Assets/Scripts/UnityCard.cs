using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schafkopf;

public class UnityCard : MonoBehaviour
{
  public SchafkopfController SchafKopfController { get; set; }

  public bool IsSelected { get; set; }

  public Player Owner { get; set; }

  public CardValues CardValue { get; set; }

  void OnMouseDown()
  {
    SchafKopfController.SelectCard(this.gameObject);    
  }

  public void ChangeColor(bool select)
  {
    var spriteRenderer = this.GetComponent<SpriteRenderer>();
    if (select)
    {
      spriteRenderer.color = UnityEngine.Color.red;
    }
    else // not Selected
    {
      spriteRenderer.color = new UnityEngine.Color(1, 1, 1, 1);
    }
  }
}
