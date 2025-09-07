using UnityEngine;

namespace _Project.Dev.Runtime.Utilities.AssetsManagment
{
    public class ResourcesAssetsLoader 
    {
        public T Load<T>(string resourcePath) where T :  Object => Resources.Load<T>(resourcePath);
    }
}
 