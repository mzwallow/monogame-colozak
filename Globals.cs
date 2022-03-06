using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Colozak.Entities;

namespace Colozak
{
    public static class Globals
    {
        public static int SCREEN_WIDTH { get; } = 960;
        public static int SCREEN_HEIGHT { get; } = 720;
        
        public static int TILE_SIZE { get; } = 48;
        public static int MAX_ACTIVE_COCOONS { get; } = 1000;
        public static int LastCocoon { get; set; } = 0;
        public static Cocoon[] ActiveCocoons;
        public static CocoonManager CocoonManager;
        public static Texture2D[] CocoonsTexture;

        public static Board Board;

        // public static void Initialize()
        // {
        //     SCREEN_WIDTH = 960;
        //     SCREEN_HEIGHT = 720;
        //     TILE_SIZE = 48;

        //     MAX_ACTIVE_COCOONS = 90;
        //     LastCocoon = 0;
        //     ActiveCocoons = new Cocoon[MAX_ACTIVE_COCOONS];
        //     CocoonManager = new CocoonManager();
        //     CocoonsTexture = new Texture2D[8];

        //     Board = new Board();

        //     // Debug
        //     Debug.WriteLine("Globals Ininitialized");
        // }
    }
}