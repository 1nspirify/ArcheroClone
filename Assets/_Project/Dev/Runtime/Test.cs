using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Dev.Runtime.Utilities.AssetsManagment;
using _Project.Dev.Runtime.Utilities.ConfigsManagment;
using _Project.Dev.Runtime.Utilities.CoroutinesManagment;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CoroutinesPerformer _coroutinesPerformerPrefab;

    private ICoroutinesPerformer _coroutinesPerformer;
    private ResoursesAssetsLoader _resoursesAssetsLoader;
    private ConfigsProviderService _configsProviderService;

    private void Awake()
    {
        _resoursesAssetsLoader = CreateResoursesAssetsLoader();

        _coroutinesPerformer = CreateCoroutinesPerformer();
        
        _configsProviderService = CreateConfigsProviderService(); 
         
        _coroutinesPerformer.StartPerform(LoadConfigs());
    }

    private ConfigsProviderService CreateConfigsProviderService()
    {
        ResoursesConfigsLoader resoursesConfigsLoader = new ResoursesConfigsLoader(_resoursesAssetsLoader);
        return new ConfigsProviderService(resoursesConfigsLoader);
    }
    
    private ResoursesAssetsLoader CreateResoursesAssetsLoader() => new ResoursesAssetsLoader();

    private CoroutinesPerformer CreateCoroutinesPerformer()
    {
        CoroutinesPerformer coroutinesPerformer =
            _resoursesAssetsLoader.Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

        return Instantiate(coroutinesPerformer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestConfig testConfig = _configsProviderService.GetConfig<TestConfig>();
            Debug.Log(testConfig.Damage );
        } 
    }

    private IEnumerator LoadConfigs()
    {
        Debug.Log("Da");
        yield return _configsProviderService.LoadAsync();
        Debug.Log("Test");
    }
}