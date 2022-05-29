﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GameBehaviorCollection
{
    private List<GameBehavior> _behaviors = new List<GameBehavior>();
    public bool IsEmpty => _behaviors.Count == 0;
    public void Add(GameBehavior enemy)
    {
        _behaviors.Add(enemy);
    }
    public void GameUpdate()
    {
        for (int i = 0; i < _behaviors.Count; i++)
        {
            if (!_behaviors[i].GameUpdate())
            {
                int lastIndex = _behaviors.Count - 1;
                _behaviors[i] = _behaviors[lastIndex];
                _behaviors.RemoveAt(lastIndex);
                i -= 1;
            }
        }
    }
    public void Clear()
    {
        for (int i = 0; i < _behaviors.Count; i++)
        {
            _behaviors[i].Recycle(); 
        }
        _behaviors.Clear();
    }
}

