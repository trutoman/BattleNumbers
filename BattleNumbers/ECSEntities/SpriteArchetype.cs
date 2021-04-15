using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSEntities
{
    public class SpriteArchetype : ECSArchetype
    {
        public SpriteArchetype() :
            base(new Type[] {
                typeof(Transform2DComponent),
                typeof(SpriteComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            TextureRegion2D texture = (TextureRegion2D)args[0];                       
            entity.AttachComponent(new Transform2DComponent());
            entity.AttachComponent(new SpriteComponent(texture));
            return entity;
        }
    }
}
