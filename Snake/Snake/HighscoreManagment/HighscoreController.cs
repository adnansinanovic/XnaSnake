using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Snake.HighscoreManagment
{
    public static class HighscoreController
    {
        private static List<HighscoreItem> _highscores;

        public static List<HighscoreItem> Highscores
        {
            get { return _highscores; }
        }

        static HighscoreController()
        {
            _highscores = new List<HighscoreItem>();
        }
       
        public static void Save(string playerName, string levelName, int snakeLength, int score)
        {            
            _highscores.Add(new HighscoreItem(playerName, score, snakeLength, levelName));            
            _highscores.Sort(new HighscoreItemComparer(SortMethod.Descending));
            
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "", "");
            xmlDocument.AppendChild(xmlDeclaration);

            XmlElement root = xmlDocument.CreateElement("Highscores");
            xmlDocument.AppendChild(root);

            foreach (HighscoreItem item in _highscores)
            {                        
                XmlElement highScoreRecord = xmlDocument.CreateElement("HighscoreRecord");
                root.AppendChild(highScoreRecord);

                XmlElement pName = xmlDocument.CreateElement("LevelName");
                pName.InnerText = item.Level;
                highScoreRecord.AppendChild(pName);

                XmlElement pSnakeLengthGoal = xmlDocument.CreateElement("SnakeLength");
                pSnakeLengthGoal.InnerText = item.SnakeLength.ToString();
                highScoreRecord.AppendChild(pSnakeLengthGoal);

                XmlElement pScoreGoal = xmlDocument.CreateElement("Score");
                pScoreGoal.InnerText = item.Score.ToString();
                highScoreRecord.AppendChild(pScoreGoal);

                XmlElement pPlayer = xmlDocument.CreateElement("PlayerName");
                pPlayer.InnerText = item.PlayerName;
                highScoreRecord.AppendChild(pPlayer);                            
            }

            
            string fileName = string.Format(@"Highscores.xml");            
            xmlDocument.Save(@fileName);
        }

        public static void Load()
        {
            _highscores.Clear();
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(string.Format("Highscores.xml"));

                XmlNodeList highscores = doc.GetElementsByTagName("Highscores");

                foreach (XmlElement highscore in highscores)
                {
                    XmlNodeList highscoreRecords = highscore.GetElementsByTagName("HighscoreRecord");
                    foreach (XmlNode highscoreRecord in highscoreRecords)
                    {
                        string levelName = highscoreRecord["LevelName"].InnerText;
                        int snakeLength = Int32.Parse(highscoreRecord["SnakeLength"].InnerText);
                        int scoreGoal = Int32.Parse(highscoreRecord["Score"].InnerText);
                        string player = highscoreRecord["PlayerName"].InnerText;
                        HighscoreItem item = new HighscoreItem(player, scoreGoal, snakeLength, levelName);
                        _highscores.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _highscores.Sort(new HighscoreItemComparer(SortMethod.Descending));
            }
        }

    }
}
