using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public Text turnText;
    public GameObject drawPanel;

    public void SwapTurn(bool oTurn)
    {
        turnText.text = oTurn ? "O" : "X";
        turnText.text += "\'s Turn";
    }

    public void ShowDraw()
    {
        drawPanel.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Intro");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("TicTacToe");
    }
}
