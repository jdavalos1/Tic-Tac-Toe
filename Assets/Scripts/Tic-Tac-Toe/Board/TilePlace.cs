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
    // Update is called once per frame
    void Update()
    {
/*        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo);


        if (hit && hitInfo.transform == transform) 
        {
            if (Input.GetMouseButtonUp(0))
            {
                // If it's not empty can't put one
                if (CurrentPiece != 'e') return;

                // It's empty so change the piece based on the turn and create new position
                CurrentPiece = control.oTurn ? 'o' : 'x';

                // Prepare a new piece and set it's parent as the current tile
                GameObject playerPiece = Instantiate(Resources.Load<GameObject>($"Letters/{CurrentPiece}.prefab"), new Vector3(control.oTurn ? 0.4f : 0.35f, 0, -0.3f), Quaternion.identity);
                playerPiece.transform.SetParent(transform, false);
                control.CheckWin();
            }
        }
*/    }

    void OnMouseDown()
    {
        // If it's not empty can't put one
        if (CurrentPiece != 'e') return;

        // It's empty so change the piece based on the turn and create new position
        CurrentPiece = control.oTurn ? 'o' : 'x';

        // Prepare a new piece and set it's parent as the current tile
        GameObject playerPiece = Instantiate(Resources.Load<GameObject>($"Letters/{CurrentPiece}"));
        playerPiece.transform.parent = transform;
        playerPiece.transform.localPosition= new Vector3(control.oTurn ? 0.4f : 0.35f, 0, -0.3f);
        control.CheckWin();
    }
}
