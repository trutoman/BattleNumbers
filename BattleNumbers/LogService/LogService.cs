using BattleNumbers.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.LogService
{
    public class LogService : ILogService
    {
        BattleNumbers game;
        public bool active { get; set; }
        public string text { get; set; }        
        public SpriteFont font { get; set; }
        public int entityId { get; set; }

        public LogService(BattleNumbers game)
        {
            game = game;
            text = string.Empty;
            active = false;
        }

        public void Initialize(SpriteFont font)
        {
            this.active = true;
            this.font = font;
        }

        public void RegisterEntity(int id)
        {
            entityId = id;
        }

        public void Clean()
        {
            this.text = string.Empty;
        }

        public void AddText(string text)
        {
            this.text += text;
        }

        public void AddNewText(string text)
        {
            this.text += "\n";
            AddText(text);
        }

        public void Activate(bool activation)
        {
            active = activation;
        }
    }
}
