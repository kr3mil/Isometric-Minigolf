using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static string GameVersion = "0.1";
    public static bool IsShooting = false;
    public static bool IsMine
    {
        get
        {
            return true;
        }
    }
    public static bool IsOffline = true;
}
