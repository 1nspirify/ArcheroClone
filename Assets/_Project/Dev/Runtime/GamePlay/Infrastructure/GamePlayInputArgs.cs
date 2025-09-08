using _Project.Dev.Runtime.Utilities.SceneManagement;

namespace _Project.Dev.Runtime.GamePlay.Infrastructure 
{
    public class GamePlayInputArgs : IInputSceneArgs
    {
        public GamePlayInputArgs(int levelIndex)
        {
            LevelIndex = levelIndex; 
        }

        public int LevelIndex { get; }
    }
}