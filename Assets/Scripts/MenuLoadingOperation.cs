using System;
using Common;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class MenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Main menu loading...";

        public async Task Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Const.Scenes.MAIN_MENU,
                LoadSceneMode.Additive);
            while (loadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }
}