using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

namespace GameDataTable
{
    public class CreateSystemMapping
    {
        public CreateActorSystem system = new CreateActorSystem();
        public LoginResultData GetLoginResultInfo(UserLoginInfo UserLoginInfo)
        {
            return system.GetLoginResultInfo(UserLoginInfo);
        }
    }

    public class CreateActorSystem : LoginResultInfo<LoginResultData, UserLoginInfo>
    {
        public LoginResultData GetLoginResultInfo(UserLoginInfo UserLoginInfo)
        {
            List<CreateRoleTable> allCreateRoles = CreateRoleTableInstance.GetActorList();
            List<ActorTable> allActors = ActorTableInstance.GetActorList();

            int playerSelectRole = Random.Range(0, CreateRoleTableInstance.Count);

            ActorTable actorTable = ActorTableInstance.GetData(allCreateRoles[playerSelectRole].ActorTable_DataID);
            ActorJobTypeTable actorJobType = ActorJobTypeTableInstance.GetData(actorTable.ActorJobTypeTable_DataID);

            LoginData loginData = new LoginData() { UserName = UserLoginInfo.UserName, Password = UserLoginInfo.Password, ActorTable_DataID = actorTable.DataID };
            LoginTest loginTest = new LoginTest();
            PlayerTable reslutTable = loginTest.OnLogin(loginData);

            CurrentPlayer.SetInstance(reslutTable);
            LoginResultData loginResultData = new LoginResultData();
            loginResultData.Success = true;
            return loginResultData;
        }
    }

    public class LoginTest : LoginGeneric<LoginData, PlayerTable>
    {
        public override PlayerTable OnLogin(LoginData userData)
        {
            List<PlayerTable> playerTables = PlayerTableInstance.GetActorList();

            for (int i = 0; i < playerTables.Count; i++)
            {
                if (playerTables[i].PlayerName == userData.UserName)
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

}
