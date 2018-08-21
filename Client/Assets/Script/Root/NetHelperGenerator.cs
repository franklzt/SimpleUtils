using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;


public class ConnectionConfig
{
    public  static string IP = "127.0.0.1";
    public  static int PORT = 12345;

    public  ConnectionConfig(string ip,int port)
    {
        IP = ip;
        PORT = port;
    }
}


public class RecieveMessageLoop
{
    public void AddMessage<T>(T value)
    {

    }
}


public class GeneratorTemplemate
{
    public string RegisterTemplemate = @"
";
}




/////////NetMessageRegisterManager ///////

//public class NetMessageRegisterManager
//{
//    public NetMessageRegisterManager()
//    {
//        /////{$RegisterList}
//        new LoginRegister();
//    }
//}


///////////LoginHelper /////////////

//public static class LoginExtension
//{
//    public static void SendLogin(this object obj, Login Login)
//    {
//        new SendLoginMessage().SendLogin(Login);
//    }
//}

//public class SendLoginMessage
//{
//    public void SendLogin(Login Login)
//    {
//        NetworkComms.SendObject("Login", ConnectionConfig.IP, ConnectionConfig.PORT, Login);
//    }    
//}

//public class LoginRegister
//{
//    public LoginRegister()
//    {
//        LoginMessageHandler handler = new LoginMessageHandler();
//        NetworkComms.AppendGlobalIncomingPacketHandler<Login>("Login", handler.OnHandleMessage);
//    }
//}

/////////LoginHandler ///////////

//public class LoginMessageHandler
//{
//    public  void OnHandleMessage(PacketHeader header, Connection connection, Login Value)
//    {
        
//    }
//}

///////////////////


public class SendCodeGenerator
{
    public string templemate = @"
using Assets.protoSource.Login;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

"
;

}
