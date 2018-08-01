using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameRoot : MonoBehaviour
    {
        public NetUIDemo userInput;
        SigalManager sigalManager;
        CommNet CommNet;

        private void Start()
        {
            CommNet = new CommNet();
            CommNet.OnReceiveMessage += OnReceiveMessage;
            sigalManager = new SigalManager();
            userInput.onButtonClick += SendButtonMessage;
        }

        public void OnReceiveMessage<T>(T Value)
        {
            if (Value is Sigal)
            {
                Sigal sigal = Value as Sigal;
                sigalManager.AddSinal(sigal, userInput);
            }
        }

        private void Update()
        {
            sigalManager.UpdateSendSinalLoop();
        }

        void SendButtonMessage()
        {
            CommNet.SendNetMessage();
        }

        private void OnDestroy()
        {
            if(CommNet != null)
            {
                CommNet.OnDestroy();
                CommNet = null;
            }
        }
    }
}