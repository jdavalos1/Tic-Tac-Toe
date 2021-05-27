using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = -2f;
    public float maxDepthZ= -1f;
    public float panSpeed = 10f;

    private Vector3 centerPos;
    private Vector2 panLimitsY;
    private Vector2 panLimitsX;
    private float minDepthZ;

    // Start is called before the first frame update
    void Start()
    {
        var gi = GameObject.Find("Game Info");
        float length = gi == null ? 3 : gi.GetComponent<GameInfo>().rowsCols;
        var newPos = new Vector3((length - 1) / 2.0f,
                                 length / 2.0f,
                                 transform.position.z - (length - 3));
        centerPos = newPos;

        // Get the minimum depths and check if max depth is larger than new pos min
        minDepthZ = maxDepthZ < newPos.z ? newPos.z - 1 : newPos.z;
        // Get min and max movement in cardinal directions
        panLimitsX = new Vector2(-0.5f, length - 0.5f);
        panLimitsY = new Vector2(0.5f, length + 0.5f);

        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        MoveOnScroll();
        MoveCardinalDirections();
    }
    
    /// <summary>
    /// Move based on the scroll wheel (z-direction)
    /// </summary>
    void MoveOnScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        var pos = transform.position;
        pos.z += scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, minDepthZ, maxDepthZ);
        transform.position = pos;
    }
    
    /// <summary>
    /// Move based on the WASD keys (x-, y- directions)
    /// </summary>
    void MoveCardinalDirections()
    {
        var pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += panSpeed * Time.deltaTime;
        }


        // Clamp based on limits
        pos.x = Mathf.Clamp(pos.x, panLimitsX.x, panLimitsX.y);
        pos.y = Mathf.Clamp(pos.y, panLimitsY.x, panLimitsY.y);
        transform.position = pos;

        if (Input.GetKey(KeyCode.C))
        {
            transform.position = centerPos;
        }
    }
}
