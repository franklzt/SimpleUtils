using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SimpleController : MonoBehaviour
{
    void Start()
    {
        ModelView.SetupView(this);
    }
}

public static class ButtonBinding
{
    public static void BindingEvent(this object obj, Button newBinding, UnityAction unityAction)
    {
        newBinding.onClick.AddListener(unityAction);
    }
}

public class Command
{
    public Command(GameObject bindingGo,UnityAction<GameObject> Action)
    {

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

    public static ModelView SetupView(MonoBehaviour setupRoot)
    {
        string prefabPath = "UIPrefab/ViewRoot";
        GameObject go = Object.Instantiate(Resources.Load<GameObject>(prefabPath), setupRoot.transform);
        return new ModelView(new DataModel("NewData"), go.GetComponent<SimpleView>());      
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