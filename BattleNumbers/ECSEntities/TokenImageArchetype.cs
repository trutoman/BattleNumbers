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
    public class TokenImageArchetype : ECSArchetype
    {
        public TokenImageArchetype() : 
            base(new Type[] {
                typeof(TokenTypeComponent),
                typeof(Transform2DComponent),
                typeof(Interaction2DComponent),
                typeof(SpriteComponent),
                typeof(CollisionComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            Texture2D texture = (Texture2D)(args[0]);
            TextureRegion2D textureRegion = new TextureRegion2D (texture);
            CollisionComponent collision = new CollisionComponent(ColliderType.rigid);
            SpriteComponent renderer = new SpriteComponent(textureRegion);
            Vector2 initialSize = new Vector2(texture.Width, texture.Height);

            entity.AttachComponent((TokenTypeComponent)args[3]);
            entity.AttachComponent(new Transform2DComponent((Vector2)args[1], (Vector2)args[2], initialSize));
            entity.AttachComponent(new Interaction2DComponent());
            entity.AttachComponent(renderer);
            entity.AttachComponent(collision);

            return entity;
        }
    }
}
