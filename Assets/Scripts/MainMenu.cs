using System.Collections.Generic;
using Loading;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _quickGameBtn;

        private void Start()
        {
            _quickGameBtn.onClick.AddListener(OnQuickGameBtnClicked);
        }

        private void OnQuickGameBtnClicked()
        {
            var loadingOperation = new Queue<ILoadingOperation>();
            loadingOperation.Enqueue(new GameLoadingOperation());
            LoadingScreen.Instance.Load(loadingOperation);
        }
    } 
}