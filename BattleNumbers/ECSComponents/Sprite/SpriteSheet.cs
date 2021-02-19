using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNumbers.ECSComponents.Sprite
{
    public class SpriteSheet
    {
        private readonly Dictionary<string, TextureRegion2D> _regionsByName;
        private readonly List<TextureRegion2D> _regionsByIndex;

        public string Name { get; }
        public Texture2D Texture { get; }

        public Dictionary<string, SpriteSheetAnimationCycle> Cycles { get; }

        public SpriteSheet(SpriteData.SpriteData data, Texture2D texture)
        {
            Name = data.SpriteSheetName;
            Texture = texture;

            _regionsByName = new Dictionary<string, TextureRegion2D>();
            _regionsByIndex = new List<TextureRegion2D>();
            Cycles = new Dictionary<string, SpriteSheetAnimationCycle>();

            Dictionary<string, Rectangle> currentDict = data.GetRegions();
            foreach (var region in currentDict)
            {
                CreateRegion(region.Key, region.Value.X, region.Value.Y, region.Value.Width, region.Value.Height);
            }

            foreach (var sequence in data.Sequences)
            {
                SpriteSheetAnimationCycle cycle = new SpriteSheetAnimationCycle(sequence.FromFrame, sequence.ToFrame);
                cycle.FrameDuration = sequence.FrameDuration;
                cycle.IsLooping = sequence.IsLooping;
                cycle.IsReversed = sequence.IsReversed;
                this.Cycles.Add(sequence.SequenceName, cycle);
            }
        }

        public TextureRegion2D CreateRegion(string name, int x, int y, int width, int height)
        {
            if (_regionsByName.ContainsKey(name))
                throw new InvalidOperationException($"Region {name} already exists in the region sheet");

            var region = new TextureRegion2D(name, Texture, x, y, width, height);
            AddRegion(region);
            return region;
        }

        private void AddRegion(TextureRegion2D region)
        {
            _regionsByName.Add(region.Name, region);
            _regionsByIndex.Add(region);
        }

        public TextureRegion2D GetRegion(int index)
        {
            if (index < 0 || index > _regionsByIndex.Count)
                throw new IndexOutOfRangeException();

            return _regionsByIndex[index];
        }

        public TextureRegion2D GetRegion(string name)
        {
            if (_regionsByName.TryGetValue(name, out TextureRegion2D region))
                return region;

            throw new KeyNotFoundException(name);
        }

        public TextureRegion2D this[string name] => GetRegion(name);
        public TextureRegion2D this[int index] => GetRegion(index);

        public SpriteSheetAnimation CreateAnimation(string name)
        {
            var cycle = Cycles[name];
            var frames = cycle.Frames.Select(f => _regionsByIndex[f]).ToArray();

            return new SpriteSheetAnimation(name, frames, cycle.FrameDuration,
                cycle.SpriteEffect, cycle.IsLooping, cycle.IsReversed);
        }
    }
}
