using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Dev.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace _Project.Dev.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configResoursesPaths = new()
        {
          
        };
  
        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new Dictionary<Type, object>();

            foreach (KeyValuePair<Type, string> configResoursesPath in _configResoursesPaths)
            {  
                ScriptableObject config = _resources.Load<ScriptableObject>(configResoursesPath.Value);
                loadedConfigs.Add(configResoursesPath.Key, config);
                yield return null;
            }

            onConfigLoaded?.Invoke(loadedConfigs);
        } 
    }
}