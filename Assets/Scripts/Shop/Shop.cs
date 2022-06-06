using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
   [SerializeField] private List<Transform> _shopItem;
    private int i = 0;

    public void LeftButton()
    {        
        try
        {
            _shopItem[i - 1].gameObject.SetActive(true);
            _shopItem[i].gameObject.SetActive(false);
            i--;
        }
        catch 
        {
            return;
        }
            
    }
    public void RightButton()
    {
        try
        {
            _shopItem[i + 1].gameObject.SetActive(true);
            _shopItem[i].gameObject.SetActive(false);
            i++;
        }
        catch
        {
            return;
        }
    }
}
