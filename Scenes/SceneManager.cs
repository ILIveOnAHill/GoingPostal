using System.Collections.Generic;

namespace GoingPostal.Scenes;

public class SceneManager
{
    public static readonly Stack<Scene> SceneStack = new();

    public static void Switch(SceneType nextScene)
    {
        if (SceneStack.Count == 0)
        {
            var s = new MainMenuScene();
            SceneStack.Push(s);
            s.Load();
            s.Init(); 
        }
        else if (SceneStack.Peek() is MainMenuScene && nextScene == SceneType.Gameplay)
        {
            var s = new GameplayScene();
            SceneStack.Push(s);
            s.Init();
            s.Load();
           
        }
        else if (SceneStack.Peek() is GameplayScene && nextScene == SceneType.MainMenu)
        {
            SceneStack.Pop();
        }
    }
}