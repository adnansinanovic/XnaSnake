using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.LevelManagment;

namespace Snake
{
    class LevelIO
    {
        private Game _game;
        private SpriteBatch _spriteBatch;
        public LevelIO(Game game, SpriteBatch spriteBatch)
        {
            _game = game;
            _spriteBatch = spriteBatch;
        }

        public void SaveLevel(List<Vector2> data, string levelName, string snakeLengthGoal, string scoreGoal)
        {

            levelName = CheckLevelName(levelName);

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "", "");
            xmlDocument.AppendChild(xmlDeclaration);

            XmlElement root = xmlDocument.CreateElement("Level");
            xmlDocument.AppendChild(root);

            /////////P A R A M E T E R S//////////////

            XmlElement parameters = xmlDocument.CreateElement("Parameters");
            root.AppendChild(parameters);

            XmlElement pName = xmlDocument.CreateElement("LevelName");
            pName.InnerText = levelName;
            parameters.AppendChild(pName);

            XmlElement pSnakeLengthGoal = xmlDocument.CreateElement("SnakeLengthGoal");
            pSnakeLengthGoal.InnerText = snakeLengthGoal;
            parameters.AppendChild(pSnakeLengthGoal);

            XmlElement pScoreGoal  = xmlDocument.CreateElement("ScoreGoal");
            pScoreGoal.InnerText = scoreGoal;
            parameters.AppendChild(pScoreGoal);
            
            /////////O B S T A C L E S//////////////
            
            XmlElement obstacles = xmlDocument.CreateElement("Obstacles");
            root.AppendChild(obstacles);
            
            foreach (Vector2 vector in data)
            {
                XmlElement elPosition = xmlDocument.CreateElement("Position");
                elPosition.SetAttribute("X", ((int)vector.X).ToString());
                elPosition.SetAttribute("Y", ((int)vector.Y).ToString());
                obstacles.AppendChild(elPosition);
            }

            string fileName = string.Format(@"Levels\{0}.xml", levelName);
            Directory.CreateDirectory("Levels");
            xmlDocument.Save(@fileName);
        }

        private string CheckLevelName(string levelName)
        {            
            string[] files = Directory.GetFiles("Levels/", "*.xml", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (fileName.ToUpper() == levelName)
                    return DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");                
            }
            return levelName;
        }

        public Dictionary<string, Level> Load(Sprite texture)
        {
            Dictionary<string, Level> loadedLevels = new Dictionary<string, Level>();
            List<Obstacle> obstacles = new List<Obstacle>();

            List<string> allFiles = new List<string>(GetLevelFiles());
            allFiles.Sort();

            foreach (string file in allFiles)
            {
                XmlDocument doc = new XmlDocument();                
                doc.Load(string.Format("{0}", file));

                XmlNodeList levels = doc.GetElementsByTagName("Level");

                foreach (XmlElement level in levels)
                {
                    obstacles = new List<Obstacle>();
                    XmlNode parameters = level.GetElementsByTagName("Parameters")[0];
                    string levelName = parameters["LevelName"].InnerText;
                    string snakeLengthGoal = parameters["SnakeLengthGoal"].InnerText;
                    string scoreGoal = parameters["ScoreGoal"].InnerText;

                    XmlNodeList obstacleNode = ((XmlElement) level).GetElementsByTagName("Obstacles");
                    XmlNodeList positions = ((XmlElement) obstacleNode[0]).GetElementsByTagName("Position");

                    foreach (XmlElement position in positions)
                    {
                        string x = position.Attributes["X"].InnerText;
                        string y = position.Attributes["Y"].InnerText;
                        obstacles.Add(new Obstacle(_game, _spriteBatch, new Vector2(Int32.Parse(x), Int32.Parse(y)),texture)); 
                    }

                    loadedLevels.Add(levelName.ToUpper(), new Level(_game, _spriteBatch, levelName, Int32.Parse(snakeLengthGoal), Int32.Parse(scoreGoal), obstacles));
                }
            }
            return loadedLevels;
        }

        private string[] GetLevelFiles()
        {
            return Directory.GetFiles("Levels", "*.xml", SearchOption.AllDirectories);
        }
    }
}

