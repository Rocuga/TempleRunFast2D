using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace templeRun
{
    class Groundable : GameObject
    {
        public bool IsGrounded
        {
            get { return !RigidBody.IsGravityAffected; }
            set { RigidBody.IsGravityAffected = !value; }
        }

        public Groundable(Vector2 spritePosition, string textureName, DrawManager.Layer drawLayer = DrawManager.Layer.Playground, int spriteWidth = 0, int spriteHeight = 0)
            : base(spritePosition, textureName, drawLayer, spriteWidth, spriteHeight)
        {

        }

        public virtual void OnGrounded()
        {
            IsGrounded = true;
            Velocity = new Vector2(Velocity.X, 0);
        }

        public virtual void OnGroundableCollision()
        {

        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Groundable)
            {
                float deltaX = collisionInfo.Delta.X;
                float deltaY = collisionInfo.Delta.Y;

                if (deltaX < deltaY)
                {//collision from right or left

                    if (Position.X < collisionInfo.Collider.X)
                    {//from left

                        deltaX = -deltaX;
                    }

                    Position = new Vector2(Position.X + deltaX, Position.Y);
                    Velocity = new Vector2(0, Velocity.Y);
                    OnGroundableCollision();
                }
                else
                {//collision from top or botton

                    if (!IsGrounded && ((Groundable)collisionInfo.Collider).IsGrounded)
                    {
                        if (Position.Y < collisionInfo.Collider.Y)
                        {//from top

                            deltaY = -deltaY;
                            OnGrounded();
                        }

                        Position = new Vector2(Position.X, Position.Y + deltaY);
                        Velocity = new Vector2(Velocity.X, 0);
                        OnGroundableCollision();
                    }

                }
            }
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                //if (!IsGrounded)
                //{//is falling
                //    //float minY = ((PlayScene)Game.CurrentScene).MinY;
                //    //if (Position.Y + RigidBody.HalfHeight > minY)
                //    //{
                //    //    Position = new Vector2(Position.X, minY - RigidBody.HalfHeight);
                //    //    OnGrounded();
                //    //}
                //}
                //else
                //{//if collides, grounded will be true again

                //    //IsGrounded = false;
                //}

            }
        }
    }
}
