using BattleNumbers.ECS;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents.Sprite
{
    public class AnimatedSpriteArchetype : ECSArchetype
    {
        public AnimatedSpriteArchetype() :
            base(new Type[] {                
                typeof(Transform2DComponent),
                typeof(AnimatedSpriteComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            Texture2D texture = (Texture2D)args[0];
            SpriteData.SpriteData data = (SpriteData.SpriteData)args[1];
            SpriteSheet spriteSheet = new SpriteSheet(data.FileName, texture, data.GetRegions("spinning"));
            entity.AttachComponent(new Transform2DComponent());            
            entity.AttachComponent(new AnimatedSpriteComponent(spriteSheet));
            return entity;
        }
    }
}
