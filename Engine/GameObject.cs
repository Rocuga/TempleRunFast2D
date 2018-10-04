using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templeRun
{
    class GameObject:IUpdatable,IDrawable, IActivable
    {
        protected Sprite sprite;
        protected Texture texture;
        protected DrawManager.Layer layer;

        public Vector2 Velocity {
            get { return (RigidBody != null ? RigidBody.Velocity : Vector2.Zero); }
            set { if (RigidBody != null) RigidBody.Velocity = value; }
        }

        public RigidBody RigidBody { get; protected set; }

        public Animation Animation { get; protected set; }
        protected List<Animation> animations { get; set; }

        public int Width { get { return (int)(sprite.Width * sprite.scale.X); } }
        public int Height { get { return (int)(sprite.Height * sprite.scale.Y); } }

        public virtual Vector2 Position { get { return RigidBody !=null? RigidBody.Position : sprite.position; }
            set { sprite.position = value;
                if(RigidBody!=null)
                    RigidBody.Position = value;
            }
        }
        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }

        public DrawManager.Layer Layer { get { return layer; } }

        public bool IsActive { get; set; }

        public GameObject()
        {
            //position.X = 0;
            //position.Y = 0;
        }

        public GameObject(Vector2 spritePosition, string textureName, DrawManager.Layer drawLayer = DrawManager.Layer.Playground, int spriteWidth=0, int spriteHeight=0)
        {
            //texture = GfxManager.GetTexture(textureName);
            Tuple<Texture, List<Animation>> ss = GfxManager.GetSpritesheet(textureName);
            texture = ss.Item1;
            animations = ss.Item2;
            Animation = animations[0];
            sprite = new Sprite(Animation.FrameWidth, Animation.FrameHeight);
            //sprite = new Sprite(spriteWidth>0 ? spriteWidth : texture.Width, spriteHeight>0 ? spriteHeight : texture.Height);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.position = spritePosition;
            layer = drawLayer;
            IsActive = true;
            UpdateManager.AddItem(this);
            DrawManager.AddItem(this);
        }

        public virtual void Destroy()
        {
            UpdateManager.RemoveItem(this);
            DrawManager.RemoveItem(this);
            if (RigidBody != null)
            {
                PhysicsManager.RemoveItem(RigidBody);
            }
        }
        
        public GameObject(Sprite spriteRef)
        {
            sprite = spriteRef;
        }

        public virtual void SetCamera(Camera camera)
        {
            sprite.Camera = camera;
        }

        public void Translate(float deltaX, float deltaY)
        {
            sprite.position.X += deltaX;
            sprite.position.Y += deltaY;
        }

        public void SetSprite(Sprite newSprite)
        {
            sprite = newSprite;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        //public virtual void Draw()
        //{
        //    if(IsActive)
        //        sprite.DrawTexture(texture);
        //}

        //public virtual void Update()
        //{
        //    if (IsActive && RigidBody !=null)
        //    {
        //        sprite.position = RigidBody.Position;
        //    }
        //}

        public virtual void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture,
                     Animation.OffsetX, Animation.OffsetY,
                     Animation.FrameWidth, Animation.FrameHeight);
        }

        public virtual void Update()
        {
            if (IsActive && RigidBody != null)
            {
                sprite.position = RigidBody.Position;
            }
            if (Animation.IsActive)
            {
                Animation.Update();
            }
        }

        public virtual void OnCollide(Collision collisionInfo)
        {

        }
    }
}
