using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gi = GameObject.Find("Game Info");
        float length = gi == null ? 3 : gi.GetComponent<GameInfo>().rowsCols;
        var newPos = new Vector3((length - 1) / 2.0f,
                                 length / 2.0f,
                                 transform.position.z - (length - 3));

        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
