using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject errorText;

    public void Play()
    {
        SceneManager.LoadScene("TicTacToe");
    }

    public void SwapMenus()
    {
        // Just swap the values of each object
        mainMenu.SetActive(!mainMenu.activeSelf);
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void NumbColsRowsSubmit()
    {
        // Get the text field first
        var text = GameObject.Find("Cols/Rows Field").GetComponent<InputField>().text;
    
        // Check if the integer is valid that is an unsigned int
        bool isInt = uint.TryParse(text, out uint value);
        if (!isInt)
        {
            errorText.SetActive(true);
            return;
        }
        
        // Int is valid so set it in the game info object
        GameObject.Find("Game Info").GetComponent<GameInfo>().rowsCols = (int)value;
    }
}
