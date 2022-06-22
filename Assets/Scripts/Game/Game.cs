using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using propertiesTower;
public class Game : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private GameBoard _board;
    [SerializeField] private DecorateBoard _decorate;

    [SerializeField] private Camera _camera;
    [SerializeField] private GameTileContentFactory _contentFactory;


    [SerializeField] private WarFactory _warFactory;

    [SerializeField] public GameScenario _scenarion;
    [SerializeField] private TilesBuilder _tilesBuilder;

    [SerializeField, Range(10, 120)] private int _startingPlayerHealth = 100;

    [SerializeField, Range(1f, 15f)] private float _prepareTime = 10f;

    [SerializeField] private Transform _windowWarning;
    [SerializeField] private Transform _windowVictory;
    [SerializeField] private Transform _windowLose; 

    [SerializeField] public TextMeshProUGUI HealthText;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField, Range(100f, 1500f)] public int _coin;
    [SerializeField] private int _dollar = 500;
    private GameScenario.State _activateScenario;

    private GameBehaviorCollection _enemies = new GameBehaviorCollection();
    private GameBehaviorCollection _nonEnemies = new GameBehaviorCollection();
    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private TowerType _currentTowerType;

    public static Game _instance;

    private Coroutine _prepareRoutine;

    private bool _scenarioInProcess;
    private bool _isGetReady;

    public int CurrentPlayerHealth;

    private int _correctLevel = 0;

    private void OnEnable()
    {
        _instance = this;
    }

    private void Start()
    {
        _decorate.Initialize(_boardSize);
        _board.Initialize(_boardSize, _contentFactory);
        _tilesBuilder.Initialize(_contentFactory, _camera, _board, true);
        HealthText.text = _startingPlayerHealth.ToString();
        BeginNewGame(); 
        _levelBuilder.InitLevel();
        GUIManager.instance.Coin = _coin;
        CalculateDollar();
    }

    private void Update()
    {
        if (_scenarioInProcess)
        {
            if (CurrentPlayerHealth <= 0)
            {
                _windowLose.gameObject.SetActive(true);
            }
            if (!_activateScenario.Progress() && _enemies.IsEmpty && _windowLose.gameObject.activeSelf == false)
            {
                _windowVictory.gameObject.SetActive(true);
            }
        }
        if (_isGetReady)
        {
            _prepareRoutine = StartCoroutine(PrepareRoutine());
            _isGetReady = false;
        }

        _enemies.GameUpdate();
        Physics.SyncTransforms();
        _board.GameUpdate();
        _nonEnemies.GameUpdate();
    }

    private void CalculateDollar()
    {
        if (PlayerPrefs.GetInt(Dollar.DECIMAL) != 0)
        {
            GUIManager.instance.Dollar = PlayerPrefs.GetInt(Dollar.DECIMAL);
        }
        else
        {
            GUIManager.instance.Dollar = _dollar;
        }
    }

    public static void SpawnEnemy(EnemyFactory factory, EnemyType type)
    {
        GameTile spawnPoint = _instance._board.GetSpawnPoint(Random.Range(0, _instance._board.SpawnPointCount));
        Enemy enemy = factory.Get(type);
        enemy.SpawnOn(spawnPoint);
        _instance._enemies.Add(enemy);
    }

    public void StartGame()
    {
        _isGetReady =true;
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

    public void BeginNewGame()
    {
        GUIManager.instance.Coin = _coin;
        _scenarioInProcess = false;
        if (_prepareRoutine != null)
        {
            StopCoroutine(_prepareRoutine);
        }
        _enemies.Clear();
        _nonEnemies.Clear();
        _board.ClearList();
        CurrentPlayerHealth = _startingPlayerHealth; 
        _instance.HealthText.text = _instance.CurrentPlayerHealth.ToString();
        _windowWarning.gameObject.SetActive(true);
      
    }

    public static void EnemyReachedDestination(int damage)
    {
        if (_instance.CurrentPlayerHealth > 0)
        {
            _instance.CurrentPlayerHealth -= damage;
            _instance.HealthText.text = _instance.CurrentPlayerHealth.ToString();
        }

        if (_instance.CurrentPlayerHealth <= 0)
        {
            _instance.CurrentPlayerHealth = 0;
            _instance.HealthText.text = _instance.CurrentPlayerHealth.ToString();
        }
         
    }

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);

        _activateScenario = _scenarion.Begin();
        _correctLevel++;
        _scenarioInProcess = true;
    }
}