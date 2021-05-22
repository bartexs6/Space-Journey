#if (UNITY_EDITOR) 
using UnityEngine;
using UnityEditor;

public class DevModeSwitch : EditorWindow
{
    // Do zmiany devmoda
    static GameObject[] objects = new GameObject[5];
    [MenuItem("Window/Switch devmode")]
    public static void ShowWindow()
    {
        var window = GetWindow<DevModeSwitch>();
        window.Show();
    }
    private void OnGUI()
    {
        objects = GameObject.Find("GameManager").GetComponent<Manager>().devModeObjects;
        if (GUILayout.Button("Switch devmode"))
        {
            foreach (var i in objects)
            {
                if(i.name == "Main Camera")
                {
                    i.GetComponent<CameraScript>().enabled = !i.GetComponent<CameraScript>().enabled;
                }else if(i.name == "GameManager")
                {
                    i.GetComponent<Load>().enabled = !i.GetComponent<Load>().enabled;
                }
                else
                {
                    i.SetActive(!i.activeSelf);
                }
            }
        }
    }
}
#endif