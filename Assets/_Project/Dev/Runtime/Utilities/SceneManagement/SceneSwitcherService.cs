using System;
using System.Collections;
using _Project.Dev.Runtime.Infrastructure;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.LoadingScreen;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Dev.Runtime.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;
        private readonly DIContainer _container;

        public SceneSwitcherService(SceneLoaderService sceneLoaderService, ILoadingScreen loadingScreen,
            DIContainer container)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _container = container;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();
            if (sceneBootstrap == null)
                throw new NullReferenceException(nameof(sceneBootstrap) + " was not found");

            yield return sceneBootstrap.Initialize(_container, sceneArgs);

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}