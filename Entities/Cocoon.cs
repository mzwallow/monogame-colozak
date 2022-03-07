using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Cocoon : IGameEntity
    {
        public const int COCOON_WIDTH = 48;
        public const int COCOON_HEIGHT = 48;

        private const float SPEED = 1000f;

        public bool IsChecked = false;
        public bool IsCheckedDown = false;
        public bool ToDestroy = false;

        public bool IsMoving = false;

        public int BoardIndex;

        public Texture2D Texture;

        public Vector2 Position;

        private float _rotation;
        private Vector2 _angleVector;

        public Circle CollisionBox
        {
            get
            {
                Circle collisionBox = new Circle(
                    new Vector2(Position.X, Position.Y),
                    (float)(COCOON_WIDTH / 2)
                );

                return collisionBox;
            }
        }

        public Cocoon(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public Cocoon(Texture2D texture, Vector2 position, float rotation)
        {
            Texture = texture;
            Position = position;
            _rotation = rotation;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsMoving)
            {
                _angleVector.X = (float)Math.Sin(_rotation) * SPEED;
                _angleVector.Y = -(float)Math.Cos(_rotation) * SPEED;
                Position += _angleVector * dt;
                CheckCollisions();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(COCOON_WIDTH / 2, COCOON_HEIGHT / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
        }

        private void CheckCollisions()
        {
            Circle _cocoonCollisionBox = CollisionBox;
            Rectangle _wallLeftCollisionBox = Globals.WallAndCeilingManager.WallLeft.CollisionBox;
            Rectangle _wallRightCollisionBox = Globals.WallAndCeilingManager.WallRight.CollisionBox;
            Rectangle _ceilingCollisionBox = Globals.WallAndCeilingManager.Ceiling.CollisionBox;
                
            if (_cocoonCollisionBox.Intersects(_wallLeftCollisionBox) || _cocoonCollisionBox.Intersects(_wallRightCollisionBox))
                _rotation *= -1;

            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c != null && !c.IsMoving && (_cocoonCollisionBox.Intersects(c.CollisionBox) || _cocoonCollisionBox.Intersects(_ceilingCollisionBox)))
                {
                    Globals.CocoonManager.CheckCocoonPosition(this);
                    Globals.CocoonManager.ToDestroyCocoons.Add(this);
                    IsChecked = true;
                    Globals.CocoonManager.CheckCocoon(this);

                    if (Globals.CocoonManager.ToDestroyCocoons.Count >= 3)
                    {
                        foreach (Cocoon _c in Globals.CocoonManager.ToDestroyCocoons)
                        {
                            _c.ToDestroy = true;
                            Globals.Board.Grids[_c.BoardIndex].IsEmpty = true;
                        }
                    }

                    for (int i = 0; i < Globals.CocoonManager.LastCocoon; i++)
                    {
                        if (Globals.CocoonManager.ActiveCocoons[i] == null)
                            continue;

                        if (Globals.CocoonManager.ActiveCocoons[i].IsChecked)
                            Globals.CocoonManager.ActiveCocoons[i].IsChecked = false;

                        if (Globals.CocoonManager.ActiveCocoons[i].ToDestroy)
                            Globals.CocoonManager.ActiveCocoons[i] = null;
                    }

                    Globals.CocoonManager.ToDestroyCocoons.Clear();

                    if (!IsCheckedDown)
                    {
                        foreach (Cocoon _c in Globals.CocoonManager.ActiveCocoons)
                        {
                            if (_c == null)
                                continue;

                            if (_c.BoardIndex < 8)
                                Globals.CocoonManager.ToKeepCocoons.Add(_c);
                        }

                        foreach (Cocoon _c in Globals.CocoonManager.ToKeepCocoons)
                        {
                            if (!_c.IsChecked)
                            {
                                _c.IsChecked = true;
                                Globals.CocoonManager.CheckCocoonDown(_c);
                            }
                        }

                        for (int i = 0; i < Globals.CocoonManager.LastCocoon; i++)
                        {
                            if (Globals.CocoonManager.ActiveCocoons[i] == null)
                                continue;

                            if (!Globals.CocoonManager.ActiveCocoons[i].IsChecked)
                            {
                                Globals.CocoonManager.ActiveCocoons[i].ToDestroy = true;
                                Globals.Board.Grids[Globals.CocoonManager.ActiveCocoons[i].BoardIndex].IsEmpty = true;
                                Globals.CocoonManager.ActiveCocoons[i] = null;
                            }
                        }

                        foreach (Cocoon _c in Globals.CocoonManager.ActiveCocoons)
                        {
                            if (_c == null)
                                continue;

                            if (_c.IsChecked)
                                _c.IsChecked = false;
                        }

                        Globals.CocoonManager.ToKeepCocoons.Clear();
                        IsCheckedDown = false;
                    }

                    return;
                }
            }
        }
    }
}