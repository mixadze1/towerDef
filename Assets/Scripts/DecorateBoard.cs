using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateBoard : MonoBehaviour
{
    [SerializeField] private MaterialMover _waterPrefab;
    [SerializeField] private Transform[] _prefabFire;
    [SerializeField] private Transform[] _prefabZombieHouse;

    private MaterialMover[] _water;

    private float offset;
    private Vector2Int _size;
    public void Initialize(Vector2Int size)
    {
        _water = new MaterialMover[size.y];
        _size = size; 
        Vector2 offset = new Vector2( size.x/3, size.y/3f);
        for (int i =0, x = 0; x < 1;  x++)
        {
            for (int y = 0; y < size.y  ; y++, i++)
            {
                MaterialMover water = _water[i] = Instantiate(_waterPrefab);
                water.transform.localScale = new Vector3(size.x * 5, size.y/3f, 1f);
                water.transform.position = new Vector3( x * offset.x , -0.7f, - 20 + size.y/3f + y * offset.y);
            }
        }
        Decorate();
    }
    private void Decorate()
    {
        for (int i = 0; i < _prefabFire.Length; i++)
        {
            Instantiate(_prefabFire[i]);
            _prefabFire[i].localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (i % 2 == 0)
            {  
                _prefabFire[i].localPosition = new Vector3(-1 * _size.x / 2 - 1, 0, _size.y / 2 + 1);
            }
            else
                _prefabFire[i].localPosition = new Vector3( - 1 * _size.x / 2 - 1, 0, -1 *_size.y / 2 - 1);
        }
        for (int i = 0; i < _prefabZombieHouse.Length; i++)
        {
            Instantiate(_prefabZombieHouse[i]);
            _prefabZombieHouse[i].localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _prefabZombieHouse[i].localRotation =  Quaternion.Euler(-90f, i * 90f, 0f);
            _prefabZombieHouse[i].localPosition = new Vector3(-1.25f * _size.x / 2, 0,(3*i /2) - _size.y / 2);
        }
        
    }
}
