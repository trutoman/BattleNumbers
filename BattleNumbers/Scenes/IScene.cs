using Microsoft.Xna.Framework;
using System;

namespace BattleNumbers.Scenes
{
    public interface IScene : IDisposable, IUpdateable, IDrawable
    {        
        void LoadContent();
        void UnloadContent();
    }
}