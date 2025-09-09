using System;
using System.Collections;
using _Project.Dev.Runtime.Infrastructure;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Dev.Runtime.GamePlay.Infrastructure
{
    public class GamePlayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GamePlayInputArgs _inputArgs ;

        public override void ProcessRegisrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GamePlayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GamePlayInputArgs)} type");
            
            _inputArgs = gameplayInputArgs; 
            
            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            
            Debug.Log($"you've come {_inputArgs.LevelIndex} level");


            Debug.Log($"Initializing GamePlay scene");

            yield break;
        }
 
        public override void Run()
        {
            Debug.Log($"Running GamePlay scene");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}