using UnityEngine;
using UnityEngine.EventSystems;


public static class SimpleHelper  {


    public static GameObject[] GetMonoGameObject(this Object obj, MonoBehaviour[] monos)
    {
        GameObject[] tempGameObject = new GameObject[monos.Length];
        for (int i = 0; i < tempGameObject.Length; i++)
        {
            tempGameObject[i] = monos[i].gameObject;
        }
        return tempGameObject;
    }


    public static int GetClickGameObjectIndex(this Object obj, GameObject[] eventGameObject)
    {
        int index = -1;
        for (int i = 0; i < eventGameObject.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == eventGameObject[i])
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
