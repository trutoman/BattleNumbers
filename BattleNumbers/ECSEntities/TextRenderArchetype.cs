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
            })
        { }

        public override ECSEntity CreateEntity(ECSEntity entity, params object[] args)
        {
            SpriteFont font = (SpriteFont)args[0];
            TextRenderComponent text = new TextRenderComponent(font);

            entity.AttachComponent(text);

            return entity;
        }
    }
}
