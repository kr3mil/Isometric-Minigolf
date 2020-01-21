using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool IsShooting = false;
    public static bool IsMine
    {
        get
        {
            return true;
        }
    }
}
