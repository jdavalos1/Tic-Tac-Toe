using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public Material[] tileMats;
    public GameObject tileModel;
    public bool oTurn;

    [Min(3)]
    public int minRowsCols;
    
    private List<GameObject> tiles;
    private int numberOfRowsColumns;

    void Awake()
    {
        var gameInfo = GameObject.Find("Game Info");
        numberOfRowsColumns = gameInfo == null ? minRowsCols : gameInfo.GetComponent<GameInfo>().rowsCols;
    }
    // Start is called before the first frame update
    void Start()
    {
        oTurn = false;
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
        // Check columns
        for(int i = 0; i < numberOfRowsColumns; i++)
        {
            row = 0;
            index = numberOfRowsColumns * row + i;
            count = 0;
            
            // Row comparison
            while (index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
            {
                count++;
                index = numberOfRowsColumns * (++row) + i;
            }
            if(count == numberOfRowsColumns)
            {
                Debug.Log("Winner winner chicken dinner");
                return;
            }
        }
        
        // Check rows
        for(int i = 0; i < numberOfRowsColumns; i++)
        {
            col = 0;
            index = numberOfRowsColumns * i + col;
            count = 0;
            
            // Col comparison
            while (index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
            {
                count++;
                index = numberOfRowsColumns * i + (++col);
            }

            if(count == numberOfRowsColumns)
            {
                Debug.Log("Winner winner chicken dinner");
                return;
            }
        }

        // Check leftmost diagonal
        row = 0;
        col = 0;
        index = 0;
        count = 0;
        while(index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
        {
            count++;
            index = numberOfRowsColumns * (++row) + (++col);
        }
        if(count == numberOfRowsColumns)
        {
            Debug.Log("Winner winner chicken dinner");
            return;
        }

        // Check rightmost diagonal
        row = 0;
        col = numberOfRowsColumns - 1;
        index = numberOfRowsColumns * row + col;
        count = 0;
        while(index < tiles.Count && tiles[index].GetComponent<TilePlace>().CurrentPiece == currentPlayer)
        {
            count++;
            index = numberOfRowsColumns * (++row) + (--col);
        }
        
        if (count == numberOfRowsColumns)
        {
            Debug.Log("Winner winner chicken dinner");
            return;
        }
        oTurn = !oTurn;

        if (CheckDraw())
        {
            Debug.Log("Draw");
        }
    }

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
}
