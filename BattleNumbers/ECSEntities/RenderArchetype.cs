using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSEntities
{
    public class RenderArchetype : ECSArchetype
    {
        public RenderArchetype() :
            base(new Type[] {
                typeof(Transform2DComponent),
                typeof(AnimatedSpriteComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            Texture2D texture = (Texture2D)args[0];                       
            entity.AttachComponent(new Transform2DComponent());
            entity.AttachComponent(new RendererComponent(texture));
            return entity;
        }
    }
}
