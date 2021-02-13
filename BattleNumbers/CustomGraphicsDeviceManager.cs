using Microsoft.Xna.Framework;

namespace BattleNumbers
{


    public class CustomGraphicsDeviceManager : GraphicsDeviceManager
    {
        private bool isWideScreenOnly;
        public bool IsWideScreenOnly
        {
            get { return isWideScreenOnly; }
            set { isWideScreenOnly = value; }
        }

        static float WideScreenRatio = 1.77f;

        public CustomGraphicsDeviceManager(Game game)
            : base(game)
        {

        }
    }
}
