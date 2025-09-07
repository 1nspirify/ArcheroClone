using System.Collections;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.AssetsManagment;
using _Project.Dev.Runtime.Utilities.ConfigsManagment;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using UnityEngine;

public class Test : MonoBehaviour
{
    private DIContainer _container;

    private void Awake()
    { 
        _container = new ();

        _container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
        _container.RegisterAsSingle(CreateConfigsProviderService);
        _container.RegisterAsSingle(CreateResoursesAssetsLoader);

        ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

        coroutinesPerformer.StartPerform(LoadConfigs());
    }

    private ConfigsProviderService CreateConfigsProviderService(DIContainer c)
    {
        ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

        ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

        return new ConfigsProviderService(resourcesConfigsLoader);
    }

    private ResourcesAssetsLoader CreateResoursesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

    private CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
    {
        ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

        CoroutinesPerformer coroutinesPerformerPrefab =
            resourcesAssetsLoader.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

        return Instantiate(coroutinesPerformerPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();

            TestConfig config = configsProviderService.GetConfig<TestConfig>();
            Debug.Log(config.Damage);
        }
    }

    private IEnumerator LoadConfigs()
    {
        ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();

        Debug.Log("Da");
        yield return configsProviderService.LoadAsync();
        Debug.Log("Test");
    }
}