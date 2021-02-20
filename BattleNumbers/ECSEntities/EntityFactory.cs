using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using BattleNumbers.ECSComponents.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSEntities
{
    public class EntityFactory
    {
        protected ECSWorld World;
        protected GameContent Content;        

        public void LoadContent(ECSWorld world, GameContent gameContent)
        {
            this.World = world;
            this.Content = gameContent;
        }

        public ECSEntity CreateRenderEntity(Rectangle bounds, Texture2D texture)
        {
            ECSEntity entity = World.CreateEntity(new RenderArchetype(), texture);

            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            //transform.Bounds = bounds;

            return entity;
        }

        public ECSEntity CreateAnimatedSpriteEntity(Rectangle bounds, Texture2D sheet, SpriteData.SpriteData sheetData)
        {
            ECSEntity entity = World.CreateEntity(new AnimatedSpriteArchetype(), sheet, sheetData);

            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            AnimatedSpriteComponent animation = entity.GetComponent<AnimatedSpriteComponent>();

            //transform.Bounds = bounds;

            animation.Play("spinning");

            return entity;
        }

        public ECSEntity CreateTokenEntity(Rectangle bounds, Texture2D sheet, SpriteData.SpriteData sheetData)
        {

            ECSEntity entity = World.CreateEntity(new TokenArchetype(), sheet, sheetData);

            TokenTypeComponent token = entity.GetComponent<TokenTypeComponent>();
            Transform2DComponent transform = entity.GetComponent<Transform2DComponent>();
            AnimatedSpriteComponent animation = entity.GetComponent<AnimatedSpriteComponent>();

            transform.Bounds = bounds;

            return entity;
        }
    }
}
