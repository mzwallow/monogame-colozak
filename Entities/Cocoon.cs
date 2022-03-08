using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Cocoon : IGameEntity
    {
        private const int COCOON_WIDTH = 48;
        private const int COCOON_HEIGHT = 48;

        private const float SPEED = 500f;

        public bool IsMoving = false;
        public bool IsChecked = false;
        public bool IsCheckedDown = false;
        public bool ToDestroy = false;

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

        /// <summary>
        /// Constructs a cocoon to the board
        /// </summary>
        public Cocoon(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        /// <summary>
        /// Constructs a cocoon to a Gun
        /// </summary>
        public Cocoon(Texture2D texture, Vector2 position, float rotation)
        {
            Texture = texture;
            Position = position;
            _rotation = rotation;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check shooting Cocoon
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

        /// <summary>
        /// Check is shooting cocoon hit walls, ceiling, other cocoons, and lose collider
        /// </summary>
        private void CheckCollisions()
        {
            Circle _cocoonCollisionBox = this.CollisionBox;
            Rectangle _wallLeftCollisionBox = Globals.ColliderManager.WallLeft.CollisionBox;
            Rectangle _wallRightCollisionBox = Globals.ColliderManager.WallRight.CollisionBox;
            Rectangle _ceilingCollisionBox = Globals.ColliderManager.Ceiling.CollisionBox;
            Rectangle _loseLineCollisionBox = Globals.ColliderManager.LoseLine.CollisionBox;
                
            // Check if hit wall then change shooting cocoon's direction
            if (_cocoonCollisionBox.Intersects(_wallLeftCollisionBox) || _cocoonCollisionBox.Intersects(_wallRightCollisionBox))
                _rotation *= -1;

            // Check if hit ceiling or other cocoons
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c != null && !c.IsMoving && (_cocoonCollisionBox.Intersects(c.CollisionBox) || _cocoonCollisionBox.Intersects(_ceilingCollisionBox)))
                {
                    Globals.CocoonManager.SetCocoonPosition(this);
                    Globals.CocoonManager.ToDestroyCocoons.Add(this);
                    this.IsChecked = true;
                    Globals.CocoonManager.CheckCocoon(this);

                    // When have matched color cocoons greater or equal than 3, destroyed them
                    if (Globals.CocoonManager.ToDestroyCocoons.Count >= 3)
                    {
                        foreach (Cocoon _c in Globals.CocoonManager.ToDestroyCocoons)
                        {
                            _c.ToDestroy = true;
                            Globals.BoardManager.Grids[_c.BoardIndex].IsEmpty = true;
                        }
                    }

                    for (int i = 0; i < Globals.CocoonManager.LastCocoonIndex; i++)
                    {
                        if (Globals.CocoonManager.ActiveCocoons[i] == null)
                            continue;

                        if (Globals.CocoonManager.ActiveCocoons[i].IsChecked)
                            Globals.CocoonManager.ActiveCocoons[i].IsChecked = false;

                        if (Globals.CocoonManager.ActiveCocoons[i].ToDestroy)
                            Globals.CocoonManager.ActiveCocoons[i] = null;
                    }

                    Globals.CocoonManager.ToDestroyCocoons.Clear();

                    // Check for dangling cocoon
                    if (!IsCheckedDown)
                    {
                        foreach (Cocoon _c in Globals.CocoonManager.ActiveCocoons)
                        {
                            if (_c == null)
                                continue;

                            // Keep cocoons at ceiling position
                            if (_c.BoardIndex < 8)
                                Globals.CocoonManager.ToKeepCocoons.Add(_c);
                        }
                        
                        // Keep cocoons that have been checked
                        foreach (Cocoon _c in Globals.CocoonManager.ToKeepCocoons)
                        {
                            if (!_c.IsChecked)
                            {
                                _c.IsChecked = true;
                                Globals.CocoonManager.CheckCocoonDown(_c);
                            }
                        }

                        // Destorys all cocoons that not have been checked
                        for (int i = 0; i < Globals.CocoonManager.LastCocoonIndex; i++)
                        {
                            if (Globals.CocoonManager.ActiveCocoons[i] == null)
                                continue;

                            if (!Globals.CocoonManager.ActiveCocoons[i].IsChecked)
                            {
                                Globals.CocoonManager.ActiveCocoons[i].ToDestroy = true;
                                Globals.BoardManager.Grids[Globals.CocoonManager.ActiveCocoons[i].BoardIndex].IsEmpty = true;
                                Globals.CocoonManager.ActiveCocoons[i] = null;
                            }
                        }

                        // Reset check status of all currently active cocoons
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