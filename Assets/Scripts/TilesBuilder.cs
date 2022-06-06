using System;
using System.Collections.Generic;
using UnityEngine;

public class TilesBuilder : MonoBehaviour
{
    [SerializeField]
    private List<BuildButton> _buttons;
    [SerializeField]
    private ContentPrice _contentPrice;
    private GameTileContentFactory _contentFactory;
    private Camera _camera;
    private GameBoard _gameBoard;
    private int _correctPrice;
    private bool _isEnabled;

    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private GameTileContent _pendingTile;
    private bool _isDestroyAllowed;

    public int PriceMortar;
    public int PriceWall;
    public int PriceLaser;
    public int PriceTurret;

    private void Awake()
    {
        _buttons.ForEach(b => b.AddListener(OnBuildingSelected));
    }

    public void Initialize(GameTileContentFactory contentFactory, Camera camera, GameBoard gameBoard,
        bool isDestroyAllowed)
    {
        _contentFactory = contentFactory;
        _isDestroyAllowed = isDestroyAllowed;
        _camera = camera;
        _gameBoard = gameBoard;
        _contentPrice.Initialize();
    }

    private void Update()
    {

        if (_pendingTile == null)
        {
            ProcessDestroying();
        }
        else
        {
            ProcessBuilding();
        }
    }

    private void ProcessBuilding()
    {
        var plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(TouchRay, out var position))
        {
            _pendingTile.transform.position = TouchRay.GetPoint(position);
        }

        if (IsPointerUp())
        {
            var tile = _gameBoard.GetTile(TouchRay);
            GUIManager.instance.Coin -= _correctPrice;        
            
            if (tile == null || _gameBoard.TryBuild(tile, _pendingTile) == false)
            {
                GUIManager.instance.Coin += _correctPrice;
                Destroy(_pendingTile.gameObject);
            }

            _pendingTile = null;
        }
    }

    private void ProcessDestroying()
    {
        if (_isDestroyAllowed == false)
            return;
        if (IsPointerUp())
        {
           
            var tile = _gameBoard.GetTile(TouchRay);
            if (tile == null ||tile.Content.Type == GameTileContentType.Destination || tile.Content.Type == GameTileContentType.SpawnPoint)
                return;
                if (tile != null)
            {
                _gameBoard.DestroyTile(tile);
            }
        }
    }

    private bool IsPointerUp()
    {
        if (Input.GetMouseButtonUp(0))
            return true;
        else
            return false;
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    private void OnBuildingSelected(GameTileContentType type)
    {
        Debug.Log(GUIManager.instance.Coin);
        if (type == GameTileContentType.Turret && GUIManager.instance.Coin >= PriceTurret)
        {
            _correctPrice = PriceTurret;
            _pendingTile = _contentFactory.Get(type);
        }
        if (type == GameTileContentType.Laser && GUIManager.instance.Coin >= PriceLaser)
        {
            _correctPrice = PriceLaser;
           _pendingTile = _contentFactory.Get(type);
        }


        if (type == GameTileContentType.Mortar && GUIManager.instance.Coin >= PriceMortar)
        {
            _correctPrice = PriceMortar;
            _pendingTile = _contentFactory.Get(type);
        }

        if (type == GameTileContentType.Wall && GUIManager.instance.Coin >= PriceWall)
        {
            _correctPrice = PriceWall;
            _pendingTile = _contentFactory.Get(type);
        }
        if (type == GameTileContentType.TurretTypeTwo && GUIManager.instance.Coin >= 100)
        {
            _correctPrice = PriceWall;
            _pendingTile = _contentFactory.Get(type);
        }

        else
            return;
    }
}
