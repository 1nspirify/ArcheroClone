using System;
using System.Collections;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.ConfigsManagment;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using _Project.Dev.Runtime.Utilities.LoadingScreen;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Dev.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log($"Project has been initialized. Setup Settings");
            SetupAppSettings();
            Debug.Log($"Services registrations of entire project");
            DIContainer container = new DIContainer();
            EntryPointRegistrations.Process(container);
            container.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(container));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();
            loadingScreen.Show();
            Debug.Log($"Services are initializing");
            yield return container.Resolve<ConfigsProviderService>().LoadAsync();
            yield return new WaitForSeconds(1f);
            Debug.Log($"Services Loading complete");
            loadingScreen.Hide();
            Debug.Log($"Scene is loading");
            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}