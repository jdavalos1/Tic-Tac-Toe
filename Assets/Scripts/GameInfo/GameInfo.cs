using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    void Awake()
    {
        // We need the gameinfo to surivive when we load the starting game
        DontDestroyOnLoad(gameObject);
    }

    [Min(3)]
    public int rowsCols = 3;
}
