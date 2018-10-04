using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using OpenTK;

namespace templeRun
{
    static class GfxManager
    {
        //static Dictionary<string, Texture> textures;
        static Dictionary<string, Tuple<Texture, List<Animation>>> spritesheets;

        public static void Init()
        {
            //textures = new Dictionary<string, Texture>();
            spritesheets = new Dictionary<string, Tuple<Texture, List<Animation>>>();
        }

        private static Animation LoadAnimation(
          XmlNode animationNode, int width, int height)
        {
            XmlNode currNode = animationNode.FirstChild;
            bool loop = bool.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            float fps = float.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int rows = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int cols = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startX = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startY = int.Parse(currNode.InnerText);

            return new Animation(width, height, cols, rows, fps, loop, startX, startY);
        }

        static void LoadSpritesheet(XmlNode spritesheetNode)
        {
            XmlNode nameNode = spritesheetNode.FirstChild;

            string name = nameNode.InnerText;
            XmlNode filenameNode = nameNode.NextSibling;
            Texture texture = new Texture(filenameNode.InnerText);

            XmlNode frameNode = filenameNode.NextSibling;


            if (frameNode.HasChildNodes)
            {
                List<Animation> animations = new List<Animation>();

                int width = int.Parse(frameNode.FirstChild.InnerText);
                int height = int.Parse(frameNode.LastChild.InnerText);
                XmlNode animationsNode = frameNode.NextSibling;

                foreach (XmlNode animationNode in animationsNode.ChildNodes)
                {
                    animations.Add(LoadAnimation(animationNode, width, height));
                }
                AddSpritesheet(name, texture, animations);
            }
            else
            {
                AddSpritesheet(name, texture);
                //animations.Add(new Animation(texture.Width, texture.Height));
            }
        }

        //public static void Load(string fileName = "Assets/SpriteSheetConfig.xml")
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(fileName);

        //    XmlNode root = doc.DocumentElement;

        //    foreach (XmlNode spritesheetNode in root.ChildNodes)
        //    {
        //        LoadSpritesheet(spritesheetNode);
        //    }
        //}

        public static List<string> LoadTileSet(string fileName = "Assets/tileset.trun.tsx")
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNode root = doc.DocumentElement;
            List<string> nameList = new List<string>();
            XmlNode tileNode = root.SelectSingleNode("tile");

            while (tileNode != null)
            {
                XmlNode imageNode = tileNode.FirstChild;
                XmlAttribute attribute = imageNode.Attributes["source"];
                string name = attribute.Value;

                Texture texture = new Texture("Assets/" + name);

                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] == '.')
                    {
                        name = name.Remove(i);
                        break;
                    }
                }

                AddSpritesheet(name, texture);
                nameList.Add(name);
                tileNode = tileNode.NextSibling;
            }
            return nameList;


        }

        public static void LoadTiledMap(string fileName, ref int mapWidth, ref int mapHeight)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode currNode = doc.DocumentElement;
            mapWidth = int.Parse(currNode.Attributes["width"].Value);
            mapHeight = int.Parse(currNode.Attributes["height"].Value);
            int tileWidth = int.Parse(currNode.Attributes["tilewidth"].Value);
            int tileHeight = int.Parse(currNode.Attributes["tileheight"].Value);

            currNode = currNode.FirstChild;
            string source = currNode.Attributes["source"].Value;
            List<string> tileNames = LoadTileSet("Assets/" + source);

            currNode = currNode.NextSibling.FirstChild;

            string map = currNode.InnerText;
            string[] mapIndex = map.Split(',');

            for (int i = 0; i < mapIndex.Length; i++)
            {
                int index = int.Parse(mapIndex[i]);
                if (index > 0)
                {
                    Vector2 pos = new Vector2(
                        tileWidth * (i % mapWidth) + tileWidth / 2,
                        tileHeight * (i / mapWidth) + tileHeight / 2);
                    string textureName = tileNames[--index];
                    new Tile(pos, textureName);
                }

            }

            mapWidth = mapWidth * tileWidth;
            mapHeight = mapHeight * tileHeight; 

        }

        public static void AddSpritesheet(string name, Texture t, List<Animation> a)
        {
            spritesheets.Add(name, new Tuple<Texture, List<Animation>>(t, a));
        }

        public static void AddSpritesheet(string name, Texture t)
        {
            List<Animation> a = new List<Animation>();
            a.Add(new Animation(t.Width, t.Height));
            AddSpritesheet(name, t, a);
        }

        public static Tuple<Texture, List<Animation>> GetSpritesheet(string name)
        {
            if (spritesheets.ContainsKey(name))
            {
                Tuple<Texture, List<Animation>> ss = spritesheets[name];

                List<Animation> animCopy = new List<Animation>();
                for (int i = 0; i < ss.Item2.Count; i++)
                {
                    animCopy.Add((Animation)ss.Item2[i].Clone());
                }
                return new Tuple<Texture, List<Animation>>(ss.Item1, animCopy);
            }
            return null;
        }

        //    public static Texture AddTexture(string name, string filePath)
        //    {
        //        Texture t = new Texture(filePath);
        //        textures.Add(name, t);

        //        return t;
        //    }

        //    public static Texture GetTexture(string name)
        //    {
        //        if (textures.ContainsKey(name))
        //        {
        //            return textures[name];
        //        }
        //        return null;
        //    }

        //    public static void RemoveAll()
        //    {
        //        textures.Clear();
        //    }
    }
}
