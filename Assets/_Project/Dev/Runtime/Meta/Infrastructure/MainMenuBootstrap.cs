using System.Collections;
using _Project.Dev.Runtime.GamePlay.Infrastructure;
using _Project.Dev.Runtime.Infrastructure;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Meta.Features.Wallet;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Dev.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private WalletService _walletService;

        public override void ProcessRegisrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Initializing Menu scene");

            _walletService = _container.Resolve<WalletService>();

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
                coroutinesPerformer.StartPerform(
                    sceneSwitcherService.ProcessSwitchTo(Scenes.GamePlay, new GamePlayInputArgs(2)));
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log($"{_walletService.GetCurrency(CurrencyTypes.Gold).Value} + Gold left");  
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2)) 
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log($"{_walletService.GetCurrency(CurrencyTypes.Gold).Value} + Gold left");  
                }
            }
        }
    }
}