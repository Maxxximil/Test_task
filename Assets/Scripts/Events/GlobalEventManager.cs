using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent onSpawnPlayer = new UnityEvent();
    public static UnityEvent onShowUI = new UnityEvent();
    public static UnityEvent<GameObject, string> onGameEnd = new UnityEvent<GameObject, string>();

    public static void SendSpawnPlayer()
    {
        onSpawnPlayer.Invoke();
    }

    public static void SendShowUI()
    {
        onShowUI.Invoke();
    }

    public static void SendGameEnd(GameObject typeOfGameEnding, string nameOfGameEnding)
    {
        onGameEnd.Invoke(typeOfGameEnding, nameOfGameEnding);
    }


}
