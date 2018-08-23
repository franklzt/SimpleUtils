using System.Collections.Generic;
using UnityEngine;

namespace GameDataTable
{
    public class LoginResultInfo<T,U> where T:new() where U:new()
    {
        public virtual U GetLoginResultInfo(T loginInfo)
        {            
            return new U();
        }
    }

    public class CurrentPlayer : SinglgInstance<CurrentPlayer>
    {
        public PlayerTable Instance { get; private set; }
        public static void SetInstance(PlayerTable playerTable)
        {
            SingleInstance.Instance = playerTable;
        }
        public static PlayerTable AccessInstance { get { return SingleInstance.Instance; } }
    }

    public class LoginGeneric<T, U> where U : new()
    {
        public virtual U OnLogin(T userData)
        {
            return new U();
        }
    }

    public class UserLoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ActorTable_DataID { get; set; }
    }

    public class LoginResultData
    {
        public bool Success { get; set;}
    }
}