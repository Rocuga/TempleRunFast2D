using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templeRun
{
    class TextObject:IActivable
    {
        protected List<TextChar> sprites;
        protected string text;
        protected bool isActive;
        protected Font font;
        protected int hSpace;

        public Vector2 Position;
        int width;
        int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public bool IsActive {
            get { return isActive; }
            set { isActive = value; UpdateCharStatus(value); }
        }

        public string Text
        {
            get { return text; }
            set { SetText(value); }
        }

        public TextObject(Vector2 spritePos, string textString = "", Font font=null, int horizontalSpace=0)
        {
            if (font == null)
            {
                font = FontManager.GetFont("stdFont");
            }
            this.font = font;
            hSpace = horizontalSpace;

            Position = spritePos;
            sprites = new List<TextChar>();
            if (textString != "")
                SetText(textString);
        }

        private void SetText(string newText)
        {
            if (newText != text)
            {
                text = newText;
                int numChars = text.Length;
                int charX = (int)Position.X;
                int charY = (int)Position.Y;

                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];

                    if(i > sprites.Count - 1)
                    {
                        sprites.Add(new TextChar(new Vector2(charX, charY), c, font));
                    }
                    else if (c != sprites[i].Character)
                    {
                        sprites[i].Character = c;
                    }

                    charX += sprites[i].Width + hSpace;
                }

                if(sprites.Count > text.Length)
                {
                    int count = sprites.Count - text.Length;
                    int from = text.Length;

                    for (int i = from; i < sprites.Count; i++)
                    {
                        sprites[i].Destroy();
                    }

                    sprites.RemoveRange(from, count);
                }
            }
        }

        protected virtual void UpdateCharStatus(bool activeStatus)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].IsActive = activeStatus;
            }
        }
    }
}
