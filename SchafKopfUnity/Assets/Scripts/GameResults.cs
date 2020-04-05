using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResults : MonoBehaviour
{
  public GameSelection GameSelection;
  public SchafkopfController Controller;

  public GameObject[] PointTextFields;
  public Button RestartButton;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetTextFieldValues(int[] values)
  {
    for (int i = 0; i < PointTextFields.Length; i++)
    {
      PointTextFields[i].GetComponent<Text>().text = values[i].ToString();
    }
  }
}
