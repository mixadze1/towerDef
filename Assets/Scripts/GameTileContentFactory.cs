using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : GameObjectFactory
{
    [SerializeField] private GameTileContent _destinationPrefab;
    [SerializeField] private GameTileContent _emptyPrefab;
    [SerializeField] private GameTileContent _wallPrefab;
    [SerializeField] private GameTileContent _spawnPrefab;
    [SerializeField] private GameTileContent _laserPrefab;
    [SerializeField] private GameTileContent _mortarPrefab;
    [SerializeField] private GameTileContent _turretPrefab;
    [SerializeField] private GameTileContent _turretTypeToPrefab;
    [SerializeField] private GameTileContent _electroMortar;
   [SerializeField] private Tower[] _towerPrefabs;
  
    public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }
    public GameTileContent Get(GameTileContentType type)
    {
        switch(type)
        {
            case GameTileContentType.Destination:
                return Get(_destinationPrefab);
            case GameTileContentType.Empty :
                return Get(_emptyPrefab);
            case GameTileContentType.Wall:
                return Get(_wallPrefab);
            case GameTileContentType.SpawnPoint:
                return Get(_spawnPrefab);
            case GameTileContentType.Laser:
                return Get(_laserPrefab);
            case GameTileContentType.Mortar:
                return Get(_mortarPrefab);
            case GameTileContentType.Turret:
                return Get(_turretPrefab);
            case GameTileContentType.TurretTypeTwo:
                return Get(_turretTypeToPrefab);
            case GameTileContentType.ElectroMortar:
                return Get(_electroMortar);
        }
        return null;
    }
    public Tower Get(TowerType type)
    {
        Tower prefab = _towerPrefabs[(int)type];
        return Get(prefab); 
    }
    private T Get<T>(T prefab) where T : GameTileContent
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }
 

   
}
