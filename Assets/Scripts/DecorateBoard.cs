using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateBoard : MonoBehaviour
{
    [SerializeField] private MaterialMover _waterPrefab;

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
                water.transform.position = new Vector3( x * offset.x , -0.3f, - 20 + size.y/3f + y * offset.y);
            }
        }
    }
}
