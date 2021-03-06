using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Loading;
using AppInfo;
using UnityEngine;
using Common;

namespace Login
{
    public class LoginOperation : ILoadingOperation
    {
        public string Description => "Login to server...";

        public object Constants { get; private set; }

        private readonly AppInfoContainer _appInfoContainer;

        private Action<float> _onProgress;

        public LoginOperation(AppInfoContainer appInfoContainer)
        {
            _appInfoContainer = appInfoContainer;
        }

        public async Task Load(Action<float> onProgress)
        {
            _onProgress = onProgress;
            _onProgress?.Invoke(0.3f);

            _appInfoContainer.UserInfo = await GetUserInfo(DeviceInfoProvider.GetDeviceId());

            _onProgress?.Invoke(1f);
        }

        private async Task<UserInfoContainer> GetUserInfo(string deviceId)
        {
            UserInfoContainer result = null;

            //Fake login
            if (PlayerPrefs.HasKey(deviceId))
            {
                result = JsonUtility.FromJson<UserInfoContainer>(PlayerPrefs.GetString(deviceId));
            }
            await Task.Delay(TimeSpan.FromSeconds(1.5f));
            _onProgress?.Invoke(0.6f);
            //Fake login

            if (result == null)
            {
                result = await ShowLoginWindows();
            }

            PlayerPrefs.SetString(deviceId, JsonUtility.ToJson(result));

            return result;
        }
        private async Task<UserInfoContainer> ShowLoginWindows()
        {
            var loadOp = SceneManager.LoadSceneAsync(Const.Scenes.LOGIN, LoadSceneMode.Additive);
            while (loadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            var loginScene = SceneManager.GetSceneByName(Const.Scenes.LOGIN);
            var rootObjects = loginScene.GetRootGameObjects();

            LoginWindow loginWindow = null;

            foreach(var go in rootObjects)
            {
                if(go.TryGetComponent(out loginWindow))
                {
                    break;
                }
            }
            var result = await loginWindow.ProcessLogin();
            var unloadOp = SceneManager.UnloadSceneAsync(Const.Scenes.LOGIN);
            while (unloadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            return result;  
        }
    }
}