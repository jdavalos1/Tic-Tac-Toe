using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    public Material[] tileMats;
    public GameObject tileModel;
    public GameObject lineModel;
    public bool oTurn = false;
    public Text text;

    [Min(3)]
    public int minRowsCols;
    
    private List<GameObject> tiles;
    private int numberOfRowsColumns;
    private InGameUI inGameUI;

    void Awake()
    {
        var gameInfo = GameObject.Find("Game Info");
        inGameUI = GameObject.Find("In-Game UI").GetComponent<InGameUI>();
        numberOfRowsColumns = gameInfo == null ? minRowsCols : gameInfo.GetComponent<GameInfo>().rowsCols;
    }
    // Start is called before the first frame update
    void Start()
    {
        inGameUI.SwapTurn(oTurn);
        tiles = new List<GameObject>();
        int total = numberOfRowsColumns * numberOfRowsColumns;

        for(int i = 0; i < total; i++)
        {
            AddTile(i);
        }
    }

    // Adds a tile to the overall board
    void AddTile(int i)
    {
        // Position of board piece
        int col = i % numberOfRowsColumns;
        int row = numberOfRowsColumns - (i / numberOfRowsColumns);
        var tilePosition = new Vector3(col, row, 0);
        GameObject tile = Instantiate(tileModel, tilePosition, tileModel.transform.rotation);
        tile.transform.SetParent(transform, false);

        // Get the new tile but if it's the first row set it up otherwise use the prev row's opposite
        var nextMat = tileMats[i % 2];
        if(row < numberOfRowsColumns)
        {
            nextMat = tiles[i - numberOfRowsColumns].GetComponent<Renderer>().material.color == tileMats[0].color ? tileMats[1] : tileMats[0];
        }
        tile.GetComponent<Renderer>().material = nextMat;

        tile.name = $"{i}";

        tiles.Add(tile);
    }
    
    // Checks for wins on the board (rows, columns, diagonals)
    public void CheckWin()
    {
        char currentPlayer = oTurn ? 'o' : 'x';
        int count, index, row, col;
        List<GameObject> potentialTiles;
        // Check columns
        for(int i = 0; i < numberOfRowsColumns; i++)
        {
            row = 0;
            index = numberOfRowsColumns * row + i;
            count = 0;

            potentialTiles = new List<GameObject>();
            // Row comparison
            while (index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
            {
                potentialTiles.Add(tiles[index]);
                count++;
                index = numberOfRowsColumns * (++row) + i;
            }
            if(count == numberOfRowsColumns)
            {
                StartCoroutine(WinnerLine(potentialTiles, new Vector3(0, 0, 0)));
                return;
            }
        }
        
        // Check rows
        for(int i = 0; i < numberOfRowsColumns; i++)
        {
            col = 0;
            index = numberOfRowsColumns * i + col;
            count = 0;
            
            potentialTiles = new List<GameObject>();
            // Col comparison
            while (index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
            {
                potentialTiles.Add(tiles[index]);
                count++;
                index = numberOfRowsColumns * i + (++col);
            }

            if(count == numberOfRowsColumns)
            {
                StartCoroutine(WinnerLine(potentialTiles, new Vector3(0, 90, 0)));
                return;
            }
        }

        // Check leftmost diagonal
        row = 0;
        col = 0;
        index = 0;
        count = 0;
        potentialTiles = new List<GameObject>();

        while(index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
        {
            potentialTiles.Add(tiles[index]);
            count++;
            index = numberOfRowsColumns * (++row) + (++col);
        }
        if(count == numberOfRowsColumns)
        {
            StartCoroutine(WinnerLine(potentialTiles, new Vector3(0, -45, 0)));
            return;
        }

        // Check rightmost diagonal
        row = 0;
        col = numberOfRowsColumns - 1;
        index = numberOfRowsColumns * row + col;
        count = 0;
        potentialTiles = new List<GameObject>();

        while(index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
        {
            potentialTiles.Add(tiles[index]);
            count++;
            index = numberOfRowsColumns * (++row) + (--col);
        }
        
        if (count == numberOfRowsColumns)
        {
            StartCoroutine(WinnerLine(potentialTiles, new Vector3(0, 45, 0)));
            return;
        }

        if (CheckDraw())
        {
            inGameUI.ShowDraw();
        }
        oTurn = !oTurn;
        inGameUI.SwapTurn(oTurn);
    }

    // Check if the board in the case of a draw
    private bool CheckDraw()
    {
        int check = 0;
        // Linearly go through all the values until an empty space is found
        foreach(var tile in tiles)
        {
            if (tile.GetComponent<TilePlace>().CurrentPiece == 'e') break;
            check++;
        }

        return check == tiles.Count;
    }

    // Add a line on the board to show the user where they've won
    IEnumerator WinnerLine(List<GameObject> winnerTiles, Vector3 angle)
    {
        foreach(var t in winnerTiles)
        {
            GameObject line = Instantiate(lineModel);
            line.transform.parent = t.transform;
            line.transform.localScale = new Vector3(0.1f, 1, 1);
            line.transform.localPosition = new Vector3(0, 10, 0);
            line.transform.localRotation = Quaternion.Euler(line.transform.localRotation.x + angle.x,
                                                            line.transform.localRotation.y + angle.y,
                                                            line.transform.localRotation.z + angle.z);
            yield return new WaitForSeconds(0.5f);
        }

        inGameUI.ShowWin(oTurn);
    }

    IEnumerator DisableTilePlace()
    {
        foreach(var t in tiles)
        {
            t.GetComponent<TilePlace>().enabled = false;
            yield return null;
        }
    }
}
