using BattleNumbers.ECS;
using BattleNumbers.ECSComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSEntities
{
    public class TextRenderArchetype : ECSArchetype
    {
        public TextRenderArchetype() :
            base(new Type[] {
                typeof(TextRenderComponent),
                typeof(Transform2DComponent)
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            SpriteFont font = (SpriteFont)args[0];
            string textValue = (string)args[1];            

            TextRenderComponent text = new TextRenderComponent(textValue, font);
            Transform2DComponent transform2D = new Transform2DComponent(new Vector2((float)args[2], (float)args[3]));

            entity.AttachComponent(transform2D);
            entity.AttachComponent(text);

            return entity;
        }
    }
}
