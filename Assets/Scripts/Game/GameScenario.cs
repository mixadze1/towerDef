using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GameScenario : ScriptableObject
{
    [SerializeField] private EnemyWave[] _waves;
    public State Begin() => new State(this);
    [Serializable]
    public struct State
    {
        private GameScenario _scenario;
        private int _index;
        private EnemyWave.State _wave;
        public State(GameScenario scenarion)
        {
            _scenario = scenarion;
            _index = 0;
            _wave = _scenario._waves[0].Begin();
        }
        public bool Progress()
        {
            float deltaTime = _wave.Progress(Time.deltaTime);
            while (deltaTime >= 0f)
            {
                if (++_index >= _scenario._waves.Length)
                {
                    return false;
                }
                _wave = _scenario._waves[_index].Begin();
                deltaTime = _wave.Progress(deltaTime);
            }
            return true;
        }
    }
}
