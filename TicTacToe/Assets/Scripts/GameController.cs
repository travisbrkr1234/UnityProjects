using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whoseTurn; //0 = x and 1 = o
    public int turnCount; //counts the number of turns played
    public GameObject[] turnIcons; //displays whos turn it is
    public Sprite[] playerIcons; //0 = x icon and 1 = o icon
    public Button[] ticTacToeSpaces; //playable spaces for our game
    public int[] markedSpaces; //ID's which space was marked by which player
    public Text winnerText; //Holds the text component of the winner text
    public GameObject[] winningLines; //Holds all the different winning line combinations
    public GameObject winnerPanel;

    // Use this for initialization
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < ticTacToeSpaces.Length; i++)
        {
            ticTacToeSpaces[i].interactable = true;
            ticTacToeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TicTacToeButton(int whichNumber)
    {
        ticTacToeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        ticTacToeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoseTurn + 1; //Identify which space has been marked by which player
        turnCount++;
        if (turnCount > 4)
        {
            WinnerCheck();
        }

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    void WinnerCheck() //8 total possible solutions: columns, rows, vertical, horizontal, diagonally
    {
        int solution1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int solution2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int solution3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int solution4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int solution5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int solution6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int solution7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int solution8 = markedSpaces[0] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] {solution1, solution2, solution3, solution4, solution5, solution6, solution7, solution8};
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3*(whoseTurn + 1))
            {
                WinnerDisplay(i);
                return;
            }
        }
    }

    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoseTurn == 0) 
        {
            winnerText.text = "Player X Wins!";
        }
        else if (whoseTurn == 1)
        {
            winnerText.text = "Player O Wins!";
        }
        winningLines[indexIn].SetActive(true);
    }
}