using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  public void OnBeginDrag(PointerEventData eventData)
  {
    Debug.Log("On Begin");
  }

  public void OnDrag(PointerEventData eventData)
  {
    this.transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    
  }
}
