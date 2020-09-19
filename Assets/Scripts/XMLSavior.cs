using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Xml.Serialization;


[Serializable]
public struct SVector3
{
    public float x;
    public float y;
    public float z;

    public SVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator SVector3(Vector3 val) 
        => new SVector3(val.x, val.y, val.z);

    public static implicit operator Vector3(SVector3 val)
        => new Vector3(val.x, val.y, val.z);
}

[Serializable]
public struct SQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SQuaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public static implicit operator SQuaternion(Quaternion val)
        => new SQuaternion(val.x, val.y, val.z, val.w);

    public static implicit operator Quaternion(SQuaternion val)
        => new Quaternion(val.x, val.y, val.z, val.w);
}

public struct SGameObject
{
    public string name;
    public SVector3 position;
    public SVector3 scale;
    public SQuaternion rotation;
}

public class SaveLvl
{
    [MenuItem("Сохранение шаблона / Сохранить уровень", false, 1)]
    private static void SaveScene()
    {      
        Scene scene = SceneManager.GetActiveScene();
        List<GameObject> rootObject = new List<GameObject>();
        scene.GetRootGameObjects(rootObject);
        List<SGameObject> levelObjectList = new List<SGameObject>();
        string savePath = Path.Combine(Application.dataPath, "EditorData.xml");

        foreach(var obj in rootObject)
        {
            Debug.Log(1);

            var temp = obj.transform;
            levelObjectList.Add(new SGameObject
            {
                name = obj.name,
                position = temp.position,
                rotation = temp.rotation,
                scale = temp.localScale
            });
        }
        XMLSavior.Save(levelObjectList.ToArray(), savePath);
    }
}

public class LoadLvl
{
    [MenuItem("Сохранение шаблона / Загрузить уровень", false, 1)]
    public static void Load()
    {
        XMLLoader.Load();       
    }
}

public class XMLSavior : MonoBehaviour
{
    private static XmlSerializer _formatter;

    static XMLSavior()
    {
        _formatter = new XmlSerializer(typeof(SGameObject[]));
    }

    public static void Save(SGameObject[] levelObj, string path)
    {
        if (levelObj == null & String.IsNullOrEmpty(path))
        {
            Debug.Log("Не задан путь или массив пуст");
            return;
        }
        if (levelObj.Length <= 0)
            return;

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            _formatter.Serialize(fs, levelObj);
        }
    }   
}

public class XMLLoader : MonoBehaviour
{
    private static XmlSerializer _formatter;

    static XMLLoader()
    {
        _formatter = new XmlSerializer(typeof(SGameObject[]));
    }

    public static void Load()
    {
        SGameObject[] result;

        using (FileStream fs = new FileStream(Path.Combine(Application.dataPath, "EditorData.xml"), FileMode.Open))
        {
            result = (SGameObject[])_formatter.Deserialize(fs);
        }

        foreach (var o in result)
        {
            var _prefab = Resources.Load<GameObject>(o.name);
            Debug.Log(o.name);
            if (_prefab != null)
            {
                GameObject temp = Instantiate(_prefab, o.position, o.rotation);
                temp.name = o.name;
            }
        }
    }
}
