using BattleNumbers.ECS;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class TokenArchetype : ECSArchetype
    {
        public TokenArchetype() : 
            base(new Type[] {
                typeof(TokenTypeComponent),
                typeof(Transform2DComponent),
                typeof(RendererComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            Texture2D texture = (Texture2D)args[0];

            entity.AttachComponent(new TokenTypeComponent());
            entity.AttachComponent(new Transform2DComponent());
            entity.AttachComponent(new RendererComponent(texture));
            return entity;
        }
    }
}
