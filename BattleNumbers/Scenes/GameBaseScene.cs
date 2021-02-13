using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using SpriteData;
using BattleNumbers.Scenes;

namespace BattleNumbers.Scene
{
    public class GameBaseScene : IScene
    {
        private BattleNumbers myGame;

        private Sprite actor1;
        private GameContent gameContent;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        private bool _Visible;
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        private int _DrawOrder;
        public int DrawOrder
        {
            get { return _DrawOrder; }
            set { _DrawOrder = value; }
        }

        private int _UpdateOrder;
        public int UpdateOrder
        {
            get { return _UpdateOrder; }
            set { _UpdateOrder = value; }
        }

        private SpriteData.SpriteData _Data;
        public SpriteData.SpriteData Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private bool _isPaused;        
        public bool isPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        public GameBaseScene(BattleNumbers game)
        {
            this.myGame = game;
            this.Enabled = true;
            this.Visible = true;
            this.isPaused = false;
        }

        public void LoadContent()
        {
            actor1 = new Sprite(this.myGame.GetGameContent().daiManjiSheet, this.myGame.GetGameContent().daiManjiData, this.myGame.GetSpriteBatch());
            actor1.FramesPerSecond(40);
        }

        public void UnloadContent()
        {

        }

        public void Pause(bool value)
        {
            this.isPaused = value;
        }

        public void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                if (!this.isPaused)
                {
                    actor1.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            actor1.Draw(Color.White);
        }

        public void Dispose()
        {
            
        }
    }
}
