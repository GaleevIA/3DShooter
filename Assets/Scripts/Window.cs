using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Window : EditorWindow
{
    public GameObject botPref;
    public GameObject patrolWayPref;
    public string name = "bot";
    public int objectCounter;
    private float radius = 20f;

    [MenuItem("Создание префабов / Окно генератора ботов")]
    public static void ShowWindow()
    {
        GetWindow(typeof(Window));
    }

    private void OnGUI()
    {
        GUILayout.Label("Настройки", EditorStyles.boldLabel);

        botPref = EditorGUILayout.ObjectField("Префаб бота", botPref, typeof(GameObject), true) as GameObject;
        patrolWayPref = EditorGUILayout.ObjectField("Префаб патруля", patrolWayPref, typeof(GameObject), true) as GameObject;
        objectCounter = EditorGUILayout.IntSlider("Количество объектов", objectCounter, 1, 200);
        radius = EditorGUILayout.Slider("Радиус", radius, 10, 100);

        if(GUILayout.Button("Сгенерировать ботов"))
        {
            if(botPref)
            {
                GameObject main = new GameObject("MainBot");
                for(int i = 0; i < objectCounter; i++)
                {
                    float angle = i * Mathf.PI / objectCounter;

                    Vector3 position = (new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle)) * radius);
                    GameObject temp = Instantiate(botPref, position, Quaternion.identity);
                    temp.transform.parent = main.transform;
                    temp.name += "(" + i + ")";
                }
            }
        }

        if (GUILayout.Button("Очистить сцену"))
        {
            Scene scene = SceneManager.GetActiveScene();
            List<GameObject> rootObject = new List<GameObject>();
            scene.GetRootGameObjects(rootObject);

            foreach (GameObject obj in rootObject)
            {
                Debug.Log(obj.name);
                DestroyImmediate(obj);
            }
        }
    }
}
