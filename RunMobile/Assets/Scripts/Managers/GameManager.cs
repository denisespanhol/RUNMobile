using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private Player _playerScript;

    [Header("UI")]
    [SerializeField] private GameObject _restartScreen;

    public void EndGame()
    {
        _restartScreen.SetActive(true);
    }
}
