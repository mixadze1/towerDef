using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryLose : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(Const.Scenes.MAIN_MENU);
    }
}
