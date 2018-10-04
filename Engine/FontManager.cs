using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templeRun
{
    static class FontManager
    {
        static Dictionary<string,Font> fonts;
        static Font standardFont;

        public static void Init()
        {
            fonts = new Dictionary<string, Font>();
            standardFont = null;
        }

        public static Font AddFont(string textureName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            Font f = new Font(textureName, texturePath, numColumns, firstCharacterASCIIvalue, charWidth, charHeight);

            fonts.Add(textureName, f);

            if (standardFont == null)
            {//first font create is the standard one
                standardFont = f;
            }

            return f;
        }

        public static Font GetFont(string textureName="")
        {
            if (fonts.ContainsKey(textureName))
            {
                return fonts[textureName];
            }

            if (standardFont != null)
            {
                return standardFont;
            }

            return null;
        }
    }
}
