using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InGameUI : MonoBehaviour
{
    public Text turnText;
    public GameObject endGamePanel;
    public Material[] panelMats;

    /// <summary>
    /// Swap text on the player's turn
    /// </summary>
    /// <param name="oTurn">Who plays next?</param>
    public void SwapTurn(bool oTurn)
    {
        turnText.text = oTurn ? "O" : "X";
        turnText.text += "\'s Turn";
    }

    public void ShowWin(bool oWin)
    {
        var t = endGamePanel.transform.Find("Title");
        t.GetComponent<Text>().text = "X Wins!!";
        endGamePanel.GetComponent<Image>().material = panelMats[1];

        if (oWin)
        {
            t.GetComponent<Text>().text = "O Wins!!";
            endGamePanel.GetComponent<Image>().material = panelMats[2];
        }
        endGamePanel.SetActive(true);
    }
    /// <summary>
    /// Show the panel to signify a draw
    /// </summary>
    public void ShowDraw()
    {
        var t = endGamePanel.transform.Find("Title");
        t.GetComponent<Text>().text = "Draw: No One Wins";
        endGamePanel.GetComponent<Image>().material = panelMats[0];
        endGamePanel.SetActive(true);
    }

    /// <summary>
    /// Load new game after an event
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene("TicTacToe");
    }
    /// <summary>
    /// Return to the main menu
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Intro");
    }
}
