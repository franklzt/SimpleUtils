using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SimpleController : MonoBehaviour
{
    DataModel dataModel;
    SimpleView simpleView;
    ModelView modelView;

    void Start()
    {
        dataModel = new DataModel("NewData");
        string prefabPath = "UIPrefab/ViewRoot";
        GameObject go = Instantiate(Resources.Load<GameObject>(prefabPath), transform);
        simpleView = go.GetComponent<SimpleView>();
        modelView = new ModelView(dataModel, simpleView);
    }
}

public static class ButtonBinding
{
    public static void BindingEvent(this object obj, Button newBinding, UnityAction unityAction)
    {
        newBinding.onClick.AddListener(unityAction);
    }
}

public class ModelView : UpdateUI<DataModel, SimpleView>
{
    DataModel model;
    SimpleView view;
    int inputClick = 0;
    string defaultData;

    public ModelView(DataModel model, SimpleView view)
    {
        defaultData = model.model;
        this.model = model;
        this.view = view;
        view.BindingEvent(view.inputButton, OnClickEvent);
    }

    void OnClickEvent()
    {
        model = new DataModel(updateData());
        UpdateData(model, view);
    }

    string updateData()
    {
        string tempData = string.Format("{0} {1}", defaultData, inputClick);
        int newInput = inputClick + 1;
        inputClick = newInput;
        return tempData;
    }

    public void UpdateData(DataModel Data, SimpleView view)
    {
        view.outputText.text = Data.model;
    }
}

public class DataModel
{
    public string model { get; }
    public DataModel(string newData)
    {
        model = newData;
    }
}

public interface UpdateUI<Model, View>
{
    void UpdateData(Model model, View view);
}

public class Tuple<Model, View> where View : UpdateUI<Model, View>
{
    public Model TValue { get; }
    public View UValue { get; }

    public Tuple(Model newT, View newU)
    {
        TValue = newT;
        UValue = newU;
    }

    public void UpdateData(Model Model, View view)
    {
        UValue.UpdateData(Model, view);
    }
}