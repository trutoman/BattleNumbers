using BattleNumbers.ECS;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.ECSComponents
{
    public enum TokenType
    {
        Integer,
        Fraction,
        Operator,
    }

    public class TokenTypeComponent : IECSComponent
    {
        public  SpriteFont Font { set; get; }
        public TokenType Type { set; get; }

        public string Image { set; get; }

        private List<TokenTypeComponent> Antecesors;
        public TokenTypeComponent(TokenType type, string image, SpriteFont font, params object[] antecesors ) 
        {
            Font = font;
            Type = type;
            Image = image;

            foreach (var antecesor in antecesors)
            {
                Antecesors.Add((TokenTypeComponent)antecesor);
            }
        }

        public TokenTypeComponent(int number, SpriteFont font)
        {
            Font = font;
            Type = TokenType.Integer;
            Image = number.ToString();
            Antecesors = null;
        }
    }
}
