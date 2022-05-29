using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private GameBoard _board;
    [SerializeField] private DecorateBoard _decorate;

    [SerializeField] private Camera _camera;
    [SerializeField] private GameTileContentFactory _contentFactory;


    [SerializeField] private WarFactory _warFactory;

    [SerializeField] private GameScenario _scenarion;


    [SerializeField, Range(10, 120)] private int _startingPlayerHealth = 100;

    [SerializeField, Range(1f, 15f)] private float _prepareTime = 10f;

    private GameScenario.State _activateScenario;

    private GameBehaviorCollection _enemies = new GameBehaviorCollection();
    private GameBehaviorCollection _nonEnemies = new GameBehaviorCollection();
    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private TowerType _currentTowerType;

    private static Game _instance;

    private Coroutine _prepareRoutine;

    private bool _scenarioInProcess;
    private bool _isPaused;

    private int _currentPlayerHealth;

    private void OnEnable()
    {
        _instance = this;
    }

    private void Start()
    {
        _decorate.Initialize(_boardSize);
        _board.Initialize(_boardSize, _contentFactory);
        BeginNewGame();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0f : 1f;
        }
        if (Input.GetKey(KeyCode.R))
        {
            BeginNewGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentTowerType = TowerType.Laser;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentTowerType = TowerType.Mortar;
        }
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HanglerAlternativeTouch();
        }
        if (_scenarioInProcess)
        {
            if (_currentPlayerHealth <= 0)
            {
                Debug.Log("defeat");
                BeginNewGame();
            }
            if (!_activateScenario.Progress() && _enemies.IsEmpty)
            {
                Debug.Log("yaPopedil");
                BeginNewGame();
            }
        }
        _enemies.GameUpdate();
        Physics.SyncTransforms();
        _board.GameUpdate();
        _nonEnemies.GameUpdate();
    }

    public static void SpawnEnemy(EnemyFactory factory, EnemyType type)
    {
        GameTile spawnPoint = _instance._board.GetSpawnPoint(Random.Range(0, _instance._board.SpawnPointCount));
        Enemy enemy = factory.Get(type);
        enemy.SpawnOn(spawnPoint);
        _instance._enemies.Add(enemy);
    }

    private void HandleTouch()
    {
        GameTile tile = _board.GetTile(TouchRay);

        if (tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _board.ToggleTower(tile, _currentTowerType);
            }
            else
            {
                _board.ToggleWall(tile);
            }

        }

    }

    private void HanglerAlternativeTouch()
    {
        GameTile tile = _board.GetTile(TouchRay);

        if (tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _board.ToggleDestination(tile);
            }
            else
            {
                _board.ToggleSpawnPoint(tile);
            }
        }

    }

    public static Shell SpawnShell()
    {
        Shell shell = _instance._warFactory.Shell;
        _instance._nonEnemies.Add(shell);
        return shell;
    }
    public static Explosion SpawnExplosion()
    {
        Explosion shell = _instance._warFactory.Explosion;
        _instance._nonEnemies.Add(shell);
        return shell;
    }

    private void BeginNewGame()
    {
        _scenarioInProcess = false;
        if (_prepareRoutine != null)
        {
            StopCoroutine(_prepareRoutine);
        }
        _enemies.Clear();
        _nonEnemies.Clear();
        _board.ClearList();
        _currentPlayerHealth = _startingPlayerHealth;
        _prepareRoutine = StartCoroutine(PrepareRoutine());
    }

    public static void EnemyReachedDestination()
    {
        _instance._currentPlayerHealth -= 5;
    }

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);
        _activateScenario = _scenarion.Begin();
        _scenarioInProcess = true;
    }
}