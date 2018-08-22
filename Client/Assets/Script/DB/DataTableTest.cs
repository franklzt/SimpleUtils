using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

namespace GameDataTable
{
    
    public class DataTableTest
    {
        public static void Test()
        {
           

            //List<ActorTable> actorLists = new ActorTableManager().GetActorList();
            //foreach (var item in actorLists)
            //{
            //    Debug.Log(item.ToString());
            //}
        }

        public static void CreateRoleTest()
        {
            List<CreateRoleTable> allCreateRoles = CreateRoleTableInstance.GetActorList();
            List<ActorTable> allActors = ActorTableInstance.GetActorList();
                        
            int playerSelectRole = Random.Range(0,CreateRoleTableInstance.Count);

            ActorTable actorTable = ActorTableInstance.GetData(allCreateRoles[playerSelectRole].ActorTable_DataID);
            ActorJobTypeTable actorJobType = ActorJobTypeTableInstance.GetData(actorTable.ActorJobTypeTable_DataID);

            LoginData loginData = new LoginData() { UserName = "UserName", Password = "password",ActorTable_DataID = actorTable.DataID };
            LoginTest loginTest = new LoginTest();
            PlayerTable reslutTable = loginTest.OnLogin(loginData);
            CurrentPlayer.SetInstance(reslutTable);

            Debug.Assert(CurrentPlayer.AccessInstance.PlayerName == loginData.UserName);
        }
    }


    public class CurrentPlayer :SinglgInstance<CurrentPlayer>
    {        
        public PlayerTable Instance { get; private set; }
        public static void SetInstance(PlayerTable playerTable)
        {
            SingleInstance.Instance = playerTable;
        }
        public static PlayerTable AccessInstance{ get { return SingleInstance.Instance; } }
    }

    public class LoginGeneric<T,U> where U:new()
    {
        public virtual U OnLogin(T userData)
        {
            return new U();
        }
    }

    public class LoginTest:LoginGeneric<LoginData,PlayerTable>
    {
        public override PlayerTable OnLogin(LoginData userData)
		{
            List<PlayerTable> playerTables = PlayerTableInstance.GetActorList();

            for (int i = 0; i < playerTables.Count; i++)
            {
                if(playerTables[i].PlayerName == userData.UserName)
                {
                    return playerTables[i];                   
                }
            }

            PlayerTable playerTable = new PlayerTable() { };
            playerTable.DataID = PlayerTableInstance.Count <= 0 ? 10000 : PlayerTableInstance.GetLastItme().DataID + 1;      
            playerTable.PlayerName = userData.UserName;
            playerTable.Password = userData.Password;
            playerTable.ActorTable_DataID = userData.ActorTable_DataID;

            SQLiteConnection connection = DataBaseHelperExtend.GetDefaultDataBaseConnnect();
            connection.Insert(playerTable);
            return playerTable;
		}
	}

    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ActorTable_DataID { get; set; }
    }

   
}
