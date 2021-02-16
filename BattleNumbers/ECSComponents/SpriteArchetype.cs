using BattleNumbers.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class SpriteArchetype : ECSArchetype
    {
        public SpriteArchetype() :
            base(new Type[] {
                typeof(TokenTypeComponent),
                typeof(Transform2DComponent),
                typeof(SpriteArchetype)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {            
            entity.AttachComponent(new Transform2DComponent());
            entity.AttachComponent(new SpriteComponent());
            return entity;
        }
    }
}
