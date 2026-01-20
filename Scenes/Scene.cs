using GoingPostal.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal.Scenes;

public enum SceneType {MainMenu, Gameplay, Pause}

public abstract class Scene()
{
    public abstract void Load();

    public abstract void Init();
    public abstract void Update(float dt);

    public abstract void Draw();
}