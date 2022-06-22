using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
  [CreateAssetMenu]
    public class EnemyFactory : GameObjectFactory
    {
    [Serializable]
    class EnemyConfig
    {
        public Enemy Prefab;
        [FloatRangeSlider(0.5f, 2f)]
        public FloatRange Scale = new FloatRange(1f);
        [FloatRangeSlider(-0.4f, 0.4f)]
        public FloatRange PathOffset = new FloatRange(0);
        [FloatRangeSlider(0.2f, 5f)]
        public FloatRange Speed = new FloatRange(0);
        [FloatRangeSlider(10f, 1000f)]
        public FloatRange Health = new FloatRange(0);
        [FloatRangeSlider(1f, 15f)]
        public FloatRange Damage = new FloatRange(0);
    }

    [SerializeField] private EnemyConfig _small, _medium, _large, _ultraLarge;

   

    public Enemy Get(EnemyType type)
        {
        var config = GetConfig(type);
            Enemy instance = CreateGameObjectInstance(config.Prefab);
            instance.OriginFactory = this;
            instance.Initialize(config.Scale.RandomValueInRange, config.PathOffset.RandomValueInRange, 
                config.Speed.RandomValueInRange, config.Health.RandomValueInRange, config.Damage.RandomValueInRange);
            return instance; 
        }
    private EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Large:
                return _large;
            case EnemyType.Small:
                return _small;
            case EnemyType.Medium:
               return _medium;
            case EnemyType.UltraLarge:
                return _ultraLarge;
       }
        return _medium;
    }
        public void Reclaim(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
