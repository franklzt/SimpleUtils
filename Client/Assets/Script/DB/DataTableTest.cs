using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

namespace GameDataTable
{
    
    public class DataTableTest
    {
        public static void Test()
        {
           
        }

        public static void CreateRoleTest()
        {
            CreateSystemMapping createSystemMapping = new CreateSystemMapping();
            createSystemMapping.GetLoginResultInfo(new UserLoginInfo() { UserName = "userName", Password = "password" });           
        }
    }


  
   
}
