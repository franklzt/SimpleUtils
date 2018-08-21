using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

namespace GameDataTable
{
    
    public class DataTableTest
    {
        public static void Test()
        {
            SQLTableCodeGenerator.GenerateCode();

            //List<ActorTable> actorLists = new ActorTableManager().GetActorList();
            //foreach (var item in actorLists)
            //{
            //    Debug.Log(item.ToString());
            //}
        }
    }


    
}
