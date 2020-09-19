using System;
using System.IO;
using System.Xml;
using UnityEngine;

public class XMLData : ISaveData
{
    string _path = Path.Combine(Application.dataPath, "XMLData.xml");

    

    public PlayerData Load()
    {
        var result = new PlayerData();

        if (!File.Exists(_path))
            return result;

        using (XmlReader reader = XmlReader.Create(_path))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement("Name"))
                    result.name = reader.GetAttribute("value");

                if (reader.IsStartElement("Health"))
                    Int32.TryParse(reader.GetAttribute("value"), out result.health);

                if (reader.IsStartElement("IsVisible"))
                    Boolean.TryParse(reader.GetAttribute("value"), out result.isVisible);
            }
        }

            return result;
    }

    public void Save(PlayerData player)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement element;

        XmlNode rootNode = xmlDoc.CreateElement("Player");
        xmlDoc.AppendChild(rootNode);

        element = xmlDoc.CreateElement("Name");
        element.SetAttribute("value", player.name);
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("Health");
        element.SetAttribute("value", player.health.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("IsVisible");
        element.SetAttribute("value", player.isVisible.ToString());
        rootNode.AppendChild(element);

        xmlDoc.Save(_path);
    }
}
