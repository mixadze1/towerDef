using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLevelsButton : MonoBehaviour
{
    [SerializeField] private Transform _levels;
    [SerializeField] private GameScenario _scenarion;
    [SerializeField] private LevelBuilder _levelBuilder;

    public void StartLevel()
    {
        Game._instance._scenarion = _scenarion;
        _levels.gameObject.SetActive(false);
        _levelBuilder.InitLevel();
        Game._instance.BeginNewGame();
    }
}
