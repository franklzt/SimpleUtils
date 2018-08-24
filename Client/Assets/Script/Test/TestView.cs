using Game;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TestView : MonoBehaviour
{
    private void Start()
    {
        var testData = new[] 
        {
            new  { playerName = "Player1", playerId = 1 },
            new  { playerName = "Player3", playerId = 3 }
        };
        for (int i = 0; i < testData.Length; i++)
        {

        }
    }
}

public class TestData
{
    public int ID { get; set; }
}

public class TestDataCollection
{
    public List<TestData> Datas = new List<TestData>();

    void Test()
    {
        Datas.Where(testdata => testdata.ID == 3);
    }
}