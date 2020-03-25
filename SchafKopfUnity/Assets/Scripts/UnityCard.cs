using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schafkopf;

public class UnityCard : MonoBehaviour
{
  public Player Owner { get; set; }

  public CardValues CardValue { get; set; }

  void OnMouseDown()
  {
    var spriteRenderer = this.GetComponent<SpriteRenderer>();
    spriteRenderer.color = spriteRenderer.color == UnityEngine.Color.red ? new UnityEngine.Color(1,1,1,1) : UnityEngine.Color.red;
  }
}
