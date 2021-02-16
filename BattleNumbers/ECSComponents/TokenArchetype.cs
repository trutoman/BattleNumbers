using BattleNumbers.ECS;
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
            entity.AttachComponent(new TokenTypeComponent());
            entity.AttachComponent(new Transform2DComponent());
            entity.AttachComponent(new RendererComponent());
            return entity;
        }
    }
}
