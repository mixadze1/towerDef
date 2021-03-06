using System;
using System.Threading.Tasks;
using Common;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class GameLoadingOperation : ILoadingOperation
    {
        public string Description => "Game Loading.. ";

        public async Task Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Const.Scenes.GAME, LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }

}
