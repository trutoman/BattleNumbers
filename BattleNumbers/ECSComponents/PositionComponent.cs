using BattleNumbers.ECS;


namespace BattleNumbers.ECSComponents
{
    public class PositionComponent : ECSComponent
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PositionComponent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
