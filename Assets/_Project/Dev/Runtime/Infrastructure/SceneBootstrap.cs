using System.Collections;
using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Dev.Runtime.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegisrations(DIContainer container, IInputSceneArgs sceneArgs = null); 
        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
} 