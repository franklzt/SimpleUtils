using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using System.Threading;
using System.Collections.Generic;

namespace Game
{
    public class CommNet
    {
        Thread connectThread;
        Thread messageThread;
        Connection connection;
        CommNetWraper connectWrap;
        const string IpAddress = "127.0.0.1";
        const int IpPort = 12345;

        public ShowText OnReceiveMessage;
        int count = 0;

        public  CommNet()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);

            //NetworkComms.AppendGlobalIncomingPacketHandler<LoginResult>("LoginResult", LoginResult);


            connectThread = new Thread(ThreadConnect);
            connectThread.Start();
        }

        void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            string newMessage = string.Format("{1} Count:{0} ", count,message);
            if(OnReceiveMessage != null)
            {
                Sigal sigal = new Sigal(newMessage);
                OnReceiveMessage.Invoke(sigal);
            } 
            count++;
        }


        //void LoginResult(PacketHeader header, Connection connection, LoginResult LoginResult)
        //{
        //    UnityEngine.Debug.Log(string.Format("Login Result {0}", LoginResult.Result));
        //}


        void ThreadConnect()
        {
            connectWrap = new CommNetWraper(IpAddress, IpPort);
            connection = connectWrap.ConnectToServer();
        }

        public void SendNetMessage()
        {
            if(connection == null)
            {
                return;
            }

            if(connection.ConnectionAlive())
            {
                string messageToSend = "This is message";
                NetworkComms.SendObject("Message", IpAddress, IpPort, messageToSend);
                SendLogin();
            }        
        }

        public void SendLogin()
        {
            //Login login = new Login() { UserName = "Player", PassWord = "Password" };
            //NetworkComms.SendObject("Login", IpAddress, IpPort, login);
        }

        public void OnDestroy()
        {
            if(connection == null)
            {
                NetworkComms.Shutdown();
                connectThread.Abort();
                return;
            }
            if (connection.ConnectionAlive())
            {
                connection.CloseConnection(false);
                NetworkComms.Shutdown();
                connectThread.Abort();
            }
        }
    }

    class CommNetWraper
    {
        private readonly string IpAddress = "";
        private readonly int IpPort = 12345;

        public CommNetWraper(string ipAddress, int port)
        {
           
            IpAddress = ipAddress;
            IpPort = port;
        }

        public Connection ConnectToServer()
        {            
            ConnectionInfo connInfo = new ConnectionInfo(IpAddress, IpPort);
            Connection newTCPConn = TCPConnection.GetConnection(connInfo);           
            return newTCPConn;
        }
    }



    public class NetWorkData
    {

    }

    public class Sigal : NetWorkData
    {
        string data;
        public string Data { get { return data; } }

        public Sigal(string newData)
        {
            data = newData;
        }
    }



   public struct BindData
    {
        public NetWorkData sigal;
        public NetUIDemo netUIDemo;
        public BindData(NetWorkData newSinal,NetUIDemo ui)
        {
            sigal = newSinal;
            netUIDemo = ui;
        }

        public void UpdateData()
        {
            netUIDemo.UpdateData<NetWorkData>(sigal);
        }
    }


    public class SigalManager
    {
        List<BindData> bindDatas = new List<BindData>();
        public void AddSinal(Sigal sigal,NetUIDemo netUIDemo)
        {
            BindData bindData = new BindData(sigal,netUIDemo);
            bindDatas.Add(bindData);
        }

        public void UpdateSendSinalLoop()
        {
            while(bindDatas.Count > 0)
            {
                bindDatas[0].UpdateData();
                bindDatas.RemoveAt(0);
            }
        }
    }
}