using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Dev.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace _Project.Dev.Runtime.Utilities.ConfigsManagment
{
    public class ResoursesConfigsLoader : IConfigsLoader
    {
        private readonly ResoursesAssetsLoader _resourses;

        private readonly Dictionary<Type, string> _configResoursesPaths = new()
        {
            {typeof(TestConfig), " TestConfig"},
        };

        public ResoursesConfigsLoader(ResoursesAssetsLoader resourses)
        {
            _resourses = resourses;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new Dictionary<Type, object>();

            foreach (KeyValuePair<Type, string> configResoursesPath in _configResoursesPaths)
            {
                ScriptableObject config = Resources.Load<ScriptableObject>(configResoursesPath.Value);
                loadedConfigs.Add(configResoursesPath.Key, config);
                yield return null;
            }

            onConfigLoaded?.Invoke(loadedConfigs);
        }
    }
}