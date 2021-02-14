using BattleNumbers.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleNumbers.ECSComponents
{
    public class RendererComponent : ECSComponent
    {
        public Texture2D MainTexture { get; set; }
        public bool IsVisible { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }
        public float Depth { get; set; }
        public float Alpha { get; set; }

        public RendererComponent(Texture2D texture)
        {
            MainTexture = texture;
            IsVisible = true;
            Color = Color.White;
            Effects = SpriteEffects.None;
            Depth = 0f;
            Alpha = 1.0f;
        }
    }
}
