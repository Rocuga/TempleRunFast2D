using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace templeRun
{
    class TextChar : GameObject
    {
        protected char character;
        protected Vector2 textureOffset;
        protected Font font;
      
        public char Character { get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 spritePosition, char character, Font font) : base(spritePosition, font.TextureName, DrawManager.Layer.GUI, font.CharacterWidth, font.CharacterHeight)
        {
            this.font = font;
            this.character = character;
            sprite.pivot = Vector2.Zero;
            sprite.Camera = CameraManager.GetCamera("GUI");
            ComputeOffset();
        }

        protected void ComputeOffset()
        {
            textureOffset = font.GetOffset(this.character);
        }

        public override void Draw()
        {
            if(IsActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, (int)sprite.Width, (int)sprite.Height);
        }
    }
}
