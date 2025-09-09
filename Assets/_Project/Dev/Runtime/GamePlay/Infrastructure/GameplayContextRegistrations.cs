using _Project.Dev.Runtime.Infrastructure.DI;
using _Project.Dev.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Dev.Runtime.GamePlay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GamePlayInputArgs args)
        {
            Debug.Log("Gameplay Context Registration Process");
        }
    }
}