using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Gun : IGameEntity
    {
        private const int GUN_POS_X = 480;
        private const int GUN_POS_Y = 600;

        // public GunState CurrentState;

        // private int _rand;

        private Texture2D _texture, _cocoonTexture;
        private Vector2 _position;
        private float _rotation;

        public Gun(Texture2D texture)
        {
            _texture = texture;
            _position = new Vector2(GUN_POS_X, GUN_POS_Y);

            _cocoonTexture = GetRandomTexture();

            Globals.IsShooting = false;
        }

        public void Update(GameTime gameTime)
        {
            if (Globals.CurrentMouseState.Y <= 576)
            {
                _rotation = (float)Math.Atan2(
                    Globals.CurrentMouseState.Y - _position.Y,
                    Globals.CurrentMouseState.X - _position.X
                )  + (float)(Math.PI * 0.5f);

                if (!Globals.IsShooting && 
                    !Globals.CeilingCanDrop &&
                    Globals.CurrentMouseState.LeftButton == ButtonState.Pressed &&
                    Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    Globals.IsShooting = true;

                    Globals.CocoonManager.AddCocoonToGun(_cocoonTexture, _position, _rotation);
                    _cocoonTexture = GetRandomTexture();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                null,
                Color.White,
                _rotation,
                new Vector2(_texture.Width / 2, (_texture.Height / 3) * 2),
                1f,
                SpriteEffects.None,
                0f
            );

            if (!Globals.IsShooting && !Globals.CeilingCanDrop)
                spriteBatch.Draw(_cocoonTexture, new Vector2(456, 576), Color.White);
        }

        public Texture2D GetRandomTexture()
        {
            bool found = false;
            int _rand = 0;
            while (!found)
            {
                _rand = new Random().Next(8);
                for (int i = 0; i < Globals.CocoonManager.LastCocoonIndex; i++)
                {
                    if (Globals.CocoonManager.ActiveCocoons[i] == null)
                        continue;
                    else if (Globals.CocoonManager.CocoonsTexture[_rand] == Globals.CocoonManager.ActiveCocoons[i].Texture)
                        found = true;
                }
            }
            
            return Globals.CocoonManager.CocoonsTexture[_rand];
        }
    }
}