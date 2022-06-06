using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Game _game;
[SerializeField] private List<GameScenario> _scenarion;
    [SerializeField] TextMeshProUGUI _levelNumber;
    [SerializeField] private int _firstCoin;
    [SerializeField] private int _coin;
    public void InitLevel()
    {
        for (int i = 0; i < _scenarion.Count - 1; i++)
        {
            if (Game._instance._scenarion == _scenarion[i] )
            {
                _levelNumber.text = "Level " + (i+1).ToString(); 
                _game.BeginNewGame();
                return;
            }
        }
    }

    public void NextLevel()
    {
        for (int i = 0; i < _scenarion.Count - 1; i++)
        {
            if (Game._instance._scenarion == _scenarion[i])
            {
                if (_scenarion[i + 1] != null)
                {
                    Game._instance._scenarion = _scenarion[i + 1];
                    _levelNumber.text = "Level " + (i+2).ToString();
                    Game._instance.BeginNewGame();
                }

                else
                {
                    Game._instance._scenarion = _scenarion[0];
                    _levelNumber.text = "Level " + 1.ToString();
                    _game.BeginNewGame();
                }
                 
                return;
            }
            if (i == _scenarion.Count - 2)
            {
                _levelNumber.text = "Level " + 1.ToString();
                _game.BeginNewGame();
                return;
            }
        }
       
    }
}
