using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlace : MonoBehaviour
{
    public char CurrentPiece;
    private BoardController control;
    void Start()
    {
        CurrentPiece = 'e';
        control = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
    }

    void OnMouseDown()
    {
        // If it's not empty can't put one
        if (CurrentPiece != 'e') return;

        // It's empty so change the piece based on the turn and create new position
        CurrentPiece = control.oTurn ? 'o' : 'x';

        // Prepare a new piece and set it's parent as the current tile
        GameObject playerPiece = Instantiate(Resources.Load<GameObject>($"Prefabs/Letters/{CurrentPiece}"));
        playerPiece.transform.parent = transform;
        playerPiece.transform.localPosition= new Vector3(control.oTurn ? 0.4f : 0.35f, 0, -0.3f);
        control.CheckWin();
    }
}
