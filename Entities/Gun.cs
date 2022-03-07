using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Gun : IGameEntity
    {
        public GunState CurrentState;

        private int _rand;

        private Texture2D _texture, _cocoonTexture;
        private Vector2 _position;
        private float _rotation;

        public Gun(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;

            _cocoonTexture = GetRandomTexture();

            CurrentState = GunState.Aiming;
        }

        public void Update(GameTime gameTime)
        {
            if (Globals.CurrentMouseState.Y <= 576)
            {
                _rotation = (float)Math.Atan2(
                    Globals.CurrentMouseState.Y - _position.Y,
                    Globals.CurrentMouseState.X - _position.X
                )  + (float)(Math.PI * 0.5f);

                if (CurrentState == GunState.Aiming &&
                    Globals.CurrentMouseState.LeftButton == ButtonState.Pressed &&
                    Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    CurrentState = GunState.Shooting;

                    Globals.CocoonManager.AddCocoonToGun(_cocoonTexture, _position, _rotation);
                    _cocoonTexture = GetRandomTexture();

                    CurrentState = GunState.Aiming;
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

            if (CurrentState == GunState.Aiming)
                spriteBatch.Draw(_cocoonTexture, new Vector2(456, 576), Color.White);
        }

        public Texture2D GetRandomTexture()
        {
            bool found = false;
            while (!found)
            {
                _rand = new Random().Next(7);
                for (int i = 0; i < Globals.CocoonManager.LastCocoon; i++)
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