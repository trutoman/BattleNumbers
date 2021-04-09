using BattleNumbers.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class TextRenderComponent : IECSComponent
    {
        public SpriteFont Font;
        public string Text { get; set; }
        public bool IsVisible { get; set; }
        public Color Color { get; set; }
        public float Depth { get; set; }
        public float Alpha { get; set; }

        public TextRenderComponent(SpriteFont font)
        {
            Font = font;
            Text = string.Empty;
            IsVisible = true;
            Color = Color.White;
            Depth = 1f;
            Alpha = 1.0f;
        }
    }
}
