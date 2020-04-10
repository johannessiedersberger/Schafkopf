using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Schafkopf;
using System;

public class GameSelection : MonoBehaviour
{
  public Button Player1Button;
  public Button Player2Button;
  public Button Player3Button;
  public Button Player4Button;

  public Button EichelAssButton;
  public Button GrassAssButton;
  public Button HerzAssButton;
  public Button SchellenAssButton;

  public Button StartButton;

  public SchafkopfController GameController;

  public GameResults GameResults;

  public Player SelectedPlayer { get; private set; }

  public Schafkopf.CardValues? SelectedAss { get; private set; }

  // Start is called before the first frame update
  void Start()
  {
    Player1Button.onClick.AddListener(PlayerOneSelected);
    Player2Button.onClick.AddListener(PlayerTwoSelected);
    Player3Button.onClick.AddListener(PlayerThreeSelected);
    Player4Button.onClick.AddListener(PlayerFourSelected);

    EichelAssButton.onClick.AddListener(EichelAssSelected);
    GrassAssButton.onClick.AddListener(GrassAssSelected);
    HerzAssButton.onClick.AddListener(HerzAssSelected);
    SchellenAssButton.onClick.AddListener(SchellenAssSelected);

    StartButton.onClick.AddListener(StartGame);
  }


  // Players
  private void PlayerOneSelected() => SelectedPlayer = GameController.Game.PlayerList[0];

  private void PlayerTwoSelected() => SelectedPlayer = GameController.Game.PlayerList[1];

  private void PlayerThreeSelected() => SelectedPlayer = GameController.Game.PlayerList[2];

  private void PlayerFourSelected() => SelectedPlayer = GameController.Game.PlayerList[3];


  // Ass Card
  private void EichelAssSelected() => SelectedAss = CardValues.EA;
  private void GrassAssSelected() => SelectedAss = CardValues.GA;
  private void HerzAssSelected() => SelectedAss = CardValues.HA;
  private void SchellenAssSelected() => SelectedAss = CardValues.SA;

  private void StartGame()
  {
    if (SelectedPlayer == null || SelectedAss == null)
      return;

    try
    {
      GameController.SelectGame(SelectedPlayer, SelectedAss.Value);
      var ui = GameObject.FindGameObjectWithTag("GameStartUI");
      var table = GameObject.FindGameObjectWithTag("Table");
      ui.SetActive(false);
      GameController.Table.SetActive(true);
    }
    catch (Exception)
    {
      Debug.Log("Game creation failed");
    }
  }

}
