using Microsoft.Xna.Framework.Input;
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
        public static int NUM_TILE { get; } = 90; 

        public static int NUM_COCOONS { get; } = 8;
        public static int MAX_ACTIVE_COCOONS { get; } = 500;

        public static bool IsShooting { get; set; } = false;

        public static float Timer { get; set; } = 0f;
        public static bool CeilingCanDrop { get; set; } = false;

        public static MouseState CurrentMouseState { get; set; }
        public static MouseState PreviousMouseState { get; set; }

        public static GameState CurrentGameState { get; set; } = GameState.Menu;

        // Manager
        public static Board BoardManager;
        public static WallAndCeilingManager WallAndCeilingManager;
        public static CocoonManager CocoonManager;


    }
}