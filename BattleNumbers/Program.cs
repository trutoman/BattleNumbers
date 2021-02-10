using System;

namespace BattleNumbers
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BattleNumbers())
                game.Run();
        }
    }
}
