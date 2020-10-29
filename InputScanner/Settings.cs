using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace InputScanner
{
    [XmlRoot("settings")]
    public class Settings
    {
        [XmlRoot("layer")]
        public class Layer
        {
            [XmlAttribute("kind")]
            public string Kind { get; set; }
            [XmlAttribute("key-code")]
            public long KeyCode { get; set; }
            [XmlAttribute("top")]
            public int Top { get; set; }
            [XmlAttribute("left")]
            public int Left { get; set; }
            [XmlAttribute("width")]
            public int Width { get; set; }
            [XmlAttribute("height")]
            public int Height { get; set; }
        }

        [XmlRoot("layer-set")]
        public class LayerSet
        {
            [XmlAttribute("name")]
            public string Name { get; set; }
            [XmlElement("layers")]
            public List<Layer> Layers { get; set; }

            public LayerSet()
            {
                Layers = new List<Layer>();
            }
        }

        private readonly static string LOCATION;

        [XmlElement("layer-set")]
        public List<LayerSet> LayerSets { get; set; }

        static Settings()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            LOCATION = Path.GetDirectoryName(assembly.Location);
        }

        public Settings()
        {
            LayerSets = new List<LayerSet>();
        }

        public static Settings Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            Settings settings = null;
            try
            {
                string filename = Path.Combine(LOCATION, "settings.xml");
                if (File.Exists(filename))
                {
                    using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        using (XmlReader reader = XmlReader.Create(stream))
                        {
                            settings = (Settings)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch { }

            if (settings == null)
            {
                settings = new Settings();
            }
            return settings;
        }

        public static void Save(Settings settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            try
            {
                string filename = Path.Combine(LOCATION, "settings.xml");
                using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true, IndentChars = "  ", Encoding = new UTF8Encoding(false) }))
                    {
                        serializer.Serialize(writer, settings);
                    }
                }
            }
            catch { }
        }
    }
}
