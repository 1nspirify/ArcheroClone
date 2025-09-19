using System;
using System.Collections.Generic;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Meta.Features.Wallet;
using _Project.Dev.Runtime.Utilities.AssetsManagment;
using _Project.Dev.Runtime.Utilities.ConfigsManagment;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using _Project.Dev.Runtime.Utilities.LoadingScreen;
using _Project.Dev.Runtime.Utilities.Reactive;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using Object = UnityEngine.Object;

namespace _Project.Dev.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(CreateResoursesAssetsLoader);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
            
            container.RegisterAsSingle(CreateWalletService);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[type] = new ReactiveVariable<int>(); 
            
            return new WalletService(currencies);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(c.Resolve<SceneLoaderService>(), c.Resolve<ILoadingScreen>(), c);

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static ResourcesAssetsLoader CreateResoursesAssetsLoader(DIContainer c) => new();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab =
                resourcesAssetsLoader.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

            return Object.Instantiate(coroutinesPerformerPrefab);
        }

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandardLoadingScreen standardLoadingScreenPrefab =
                resourcesAssetsLoader.Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

            return Object.Instantiate(standardLoadingScreenPrefab);
        }
    }
}