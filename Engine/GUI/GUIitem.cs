using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templeRun
{
    class GUIitem : GameObject
    {
        public bool IsSelected { get; set; }
        
        protected GameObject owner;
        protected Vector2 offset;

        public GUIitem(Vector2 position, string textureName, GameObject guiOwner, int width = 0, int height = 0) : base(position, textureName, DrawManager.Layer.GUI, width, height)
        {
            sprite.Camera = CameraManager.GetCamera("GUI");
            owner = guiOwner;
            offset = position - owner.Position;
        }

        public void SetColor(Vector4 color)
        {
            sprite.SetMultiplyTint(color);
        }
    }
}
