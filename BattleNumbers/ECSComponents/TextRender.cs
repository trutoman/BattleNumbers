using BattleNumbers.ECS;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public class TextRender : IECSComponent
    {
        public string Text { get; set; }
        public bool IsVisible { get; set; }
        public Color Color { get; set; }
        public float Depth { get; set; }
        public float Alpha { get; set; }

        public TextRender(Texture2D texture)
        {
            Text = string.Empty;
            IsVisible = true;
            Color = Color.White;
            Depth = 0f;
            Alpha = 1.0f;
        }
    }
}
