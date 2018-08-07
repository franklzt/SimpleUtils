using UnityEngine;

public class GameImplement : MonoBehaviour
{
    public SimpleMonoView monoView;
    //List<RootManager<LocalModel,View,NetWorkModel>> RootManagerList  ;



    private void Start()
    {
        // RootManagerList = new List<RootManager<LocalModel, View, NetWorkModel>>();
        SimpleRootManager simpleRootManager = new SimpleRootManager(new SimpleLocalModel(), monoView, new SimpleNetModel());
    }
}


public class SimpleRootManager : RootManager<SimpleLocalModel, SimpleMonoView, SimpleNetModel>
{
    public SimpleRootManager(SimpleLocalModel L, SimpleMonoView V, SimpleNetModel N)
    {

    }

    public ViewSendLocalData<SimpleLocalModel, SimpleMonoView> ViewToLocalData
    {
        get;set;
    }

    public UpdateLocalModelByNetModel<SimpleLocalModel, SimpleNetModel> NetDataToLocal
    {
        get;set;
    }

    public UpdateViewByLocalData<SimpleMonoView, SimpleLocalModel> LocalDataToView
    {
        get;set;
    }
}

public class SimpleLocalModel : LocalModel
{
    public string LocalData = "LocalData";
}

public class SimpleNetModel : NetWorkModel
{
    public string NetData = "NetData";
}

public class SimpleViewSendLocalData : ViewSendLocalData<SimpleLocalModel, SimpleMonoView>
{
    private readonly SimpleRootManager manager;

    public SimpleViewSendLocalData(SimpleRootManager manager, SimpleLocalModel L, SimpleMonoView V)
    {
        this.manager = manager;
    }

    public SimpleLocalModel LocalData
    {
        get;
    }

    public SimpleMonoView ViewData { get; }

    public void UpdateData(SimpleLocalModel localModel, SimpleMonoView view)
    {

    }
}

public class SimpleUpdateLocalModelByNetModel : UpdateLocalModelByNetModel<SimpleLocalModel, SimpleNetModel>
{
    private readonly SimpleRootManager manager;

    public SimpleLocalModel LocalData
    {
        get;      
    }

    public SimpleNetModel NetworkData { get; }

    public void UpdateData(SimpleLocalModel localModel, SimpleNetModel netModel)
    {
    }

    public SimpleUpdateLocalModelByNetModel(SimpleRootManager manager, SimpleLocalModel L, SimpleNetModel N)
    {
        this.manager = manager;
    }
}

public class SimpleUpdateViewByLocalData : UpdateViewByLocalData<SimpleMonoView, SimpleLocalModel>
{
    private SimpleRootManager manager;

    public SimpleMonoView ViewData
    {
        get;
    }

    public SimpleLocalModel LocalData { get; }

    public SimpleUpdateViewByLocalData(SimpleRootManager manager, SimpleMonoView V, SimpleLocalModel L)
    {
        this.manager = manager;
        V.BindingEvent(V.sendButton, SendLocalData);
        Debug.Log("binding");
    }

    private void SendLocalData()
    {
        //string sendData = string.Format("SendLocalData {0}", manager.LocalData.LocalData);
        //string netDataupdate = string.Format("{0}{1}", manager.NetWorkData, manager.LocalData.LocalData);
        //string netDataUpdateLocalData = manager.LocalData.LocalData;
        //string updatedlocalData = manager.NetWorkData.NetData;
        //string updateViewData = updatedlocalData;

        //var debugData = new[] { sendData, netDataupdate, netDataUpdateLocalData, updatedlocalData, updatedlocalData };
        //for (int i = 0; i < debugData.Length; i++)
        //{
        //    Debug.Log(debugData[i] + "n");
        //}

        //manager.View.outText.text = updateViewData;

        string senddata = string.Format("{0}", manager.ViewToLocalData.LocalData);
       //manager.NetDataToLocal.UpdateData()
    }
    public void UpdateData(SimpleMonoView V, SimpleLocalModel L)
    {

    }
}


