using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SerializeMonoTest : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

        string jsonText = Resources.Load<TextAsset>("json/jsondemo").text;
        Debug.Log(jsonText);
        AnimalsJsonRoot jsonRoot = JsonConvert.DeserializeObject<AnimalsJsonRoot>(jsonText);
        Debug.Log(jsonRoot);

    }


}
