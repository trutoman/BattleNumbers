using System;

namespace BattleNumbers
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (BattleNumbers game = new BattleNumbers())
                game.Run();
        }
    }
}
