using System.Collections.Generic;
using System.Xml;

namespace Common.TranslationManager
{
    public class TranslationController
    {
        private Dictionary<string, Dictionary<string, string>> _translations = new Dictionary<string, Dictionary<string, string>>();
        
        private string _currentLanguage;

        #region Singleton
        private static TranslationController _instance;

        public static TranslationController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TranslationController();

                return _instance;
            }
        }

        private TranslationController()
        {
            Load();
            _currentLanguage = "";

        }
        #endregion

        public List<string> GetLanguages()
        {
            List<string> languages = new List<string>();
            foreach (string language in _translations.Keys)
                languages.Add(language);

            return languages;
        }

        public void SetLanguage(string language)
        {
            _currentLanguage = language;
        }

        public string GetMessage(string key)
        {
            return GetMessage(_currentLanguage, key);
        }

        public string GetMessage(string language, string key)
        {
            if (_translations.ContainsKey(language.ToUpper()) && _translations[language].ContainsKey(key.ToUpper()))
                return _translations[language.ToUpper()][key.ToUpper()];

            return key;
        }

        private void Load()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Translations.xml");

            XmlNodeList languages = doc.GetElementsByTagName("Language");

            foreach (XmlElement language in languages)
            {                
                XmlNodeList messages = language.GetElementsByTagName("Message");

                string languageName =  language.Attributes["Name"].InnerText.ToUpper();                

                if (!_translations.ContainsKey(languageName))
                    _translations.Add(languageName, new Dictionary<string, string>());

                foreach (XmlNode node1 in messages)
                {
                    XmlElement message = (XmlElement)node1;
                    
                    string key = message.Attributes["Key"].InnerText.ToUpper();                    
                    string msg = message.InnerText;

                    if (!_translations[languageName].ContainsKey(key))
                        _translations[languageName].Add(key, msg);                    
                }
            }
        }

    }
}
