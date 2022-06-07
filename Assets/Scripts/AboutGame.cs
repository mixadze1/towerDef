using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutGame : MonoBehaviour
{
    [SerializeField] private List<Transform> _aboutGame;
    private int i = 0;
    public void LeftButton()
    {
        try
        {
            _aboutGame[i - 1].gameObject.SetActive(true);
            _aboutGame[i].gameObject.SetActive(false);
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
            _aboutGame[i + 1].gameObject.SetActive(true);
            _aboutGame[i].gameObject.SetActive(false);
            i++;
        }
        catch
        {
            return;
        }
    }
}
