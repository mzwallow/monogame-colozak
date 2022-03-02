using System;

namespace Colozak
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Colozak())
                game.Run();
        }
    }
}
