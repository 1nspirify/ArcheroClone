using _Project.Dev.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Dev.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("MainMenu  Context Registration Process");
        }
    }
}