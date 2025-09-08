using System.Collections;
using _Project.Dev.Runtime.GamePlay.Infrastructure;
using _Project.Dev.Runtime.Infrastructure;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Dev.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            Debug.Log($"Initializing Menu scene");

            yield break;
        }

        public override void Run()
        {
            Debug.Log($"Running Menu scene");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.GamePlay, new GamePlayInputArgs(2))); 
            }
        }
    }
}