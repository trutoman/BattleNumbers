using BattleNumbers.ECS;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static BattleNumbers.ECSComponents.CollisionComponent;

namespace BattleNumbers.ECSComponents
{
    public class TokenArchetype : ECSArchetype
    {
        public TokenArchetype() : 
            base(new Type[] {
                typeof(TokenTypeComponent),
                typeof(Transform2DComponent),
                typeof(Interaction2DComponent),
                typeof(AnimatedSpriteComponent),
                typeof(CollisionComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            Texture2D texture = (Texture2D)args[0];
            SpriteData.SpriteData data = (SpriteData.SpriteData)args[1];
            SpriteSheet spriteSheet = new SpriteSheet(data, texture);
            AnimatedSpriteComponent animation = new AnimatedSpriteComponent(spriteSheet);
            CollisionComponent collision = new CollisionComponent(ColliderType.rigid);
            animation.Play(data.InitSequence);
            Vector2 initialSize = new Vector2(animation.CurrentAnimation.CurrentFrame.Width, animation.CurrentAnimation.CurrentFrame.Height);

            entity.AttachComponent((TokenTypeComponent)args[4]);
            entity.AttachComponent(new Transform2DComponent((Vector2)args[2], (Vector2)args[3], initialSize));
            entity.AttachComponent(new Interaction2DComponent());
            entity.AttachComponent(animation);
            entity.AttachComponent(collision);

            return entity;
        }
    }
}
