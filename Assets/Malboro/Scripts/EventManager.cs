using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public static Action GameOver;
    public static Action StartGame;
    public static Action<bool, int> CollectItems;

    public bool isStartGame = false;
    public bool isGameOver = false;
    public bool isUIOpen = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

   

}
