using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseUI : MonoBehaviour
{
    public Button button;
    public Text infoText;
}

public class DataBaseUIBinding : UIBinding<DataBaseUI, DataBaseUIMedia>
{

}


public class UIBinding<View, Meida> where View : MonoBehaviour
{
    View view;
    public void BindView(View view, Meida meida)
    {
        this.view = view;
    }
}

public class DataBaseUIMedia
{
    public DataBaseUI View { get; set; }

}

public class DataBaseUIModel
{
    public string Info { get; set; }
}