using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public delegate void OnButtonClick();
    public delegate void ShowText(NetWorkData text);

    public class NetUIDemo : MonoBehaviour
    {

        public Button sendMessageButton;
        public Text showMessageText;

        public OnButtonClick onButtonClick;
        ShowText showText;

        public void UpdateData<T>(T Value)
        {
            Sigal sigal = Value as Sigal;
            if(sigal != null)
            {
                showMessageText.text = sigal.Data;
            }
        }

        private void Awake()
        {
            sendMessageButton.onClick.AddListener(SendButtonMessage);
            showText += UpdateData;
        }

       

        private void ShowNewText(string newText)
        {
            showMessageText.text = newText;
        }

        private void SendButtonMessage()
        {
            onButtonClick?.Invoke();
        }

    }
}


