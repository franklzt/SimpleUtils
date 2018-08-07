using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RandomElement;

namespace BagImplement
{
    public class BagImplement : MonoBehaviour
    {
        BagController BagController;
        public Transform viewParent;
        private void Start()
        {
            BagController = new BagController(viewParent);
            BagController.LoadInitData();
        }
    }


    public class BagViewManager
    {
        public List<BagViewItem> BagViewItems { get; }
        Transform parent;
        public BagViewManager(Transform parent)
        {
            this.parent = parent;
            BagViewItems = new List<BagViewItem>();
        }
    }


    public class BagViewItem
    {
        public Image BagIccon { get; }
        public Text BagText { get; }
        public BagViewItem(Transform parent)
        {
            GameObject bagViewRoot = new GameObject("GameRoot");
            bagViewRoot.transform.SetParent(parent);
            bagViewRoot.transform.position = Vector3.zero;
            bagViewRoot.AddComponent<RectTransform>();
            BagIccon = new GameObject("BagIccon").AddComponent<Image>();
            BagIccon.transform.SetParent(bagViewRoot.transform);
            BagText = new GameObject("BagText").AddComponent<Text>();
            BagText.transform.SetParent(bagViewRoot.transform);

            RectTransform bagIconRect = BagIccon.GetComponent<RectTransform>();
            bagIconRect.anchoredPosition = Vector2.zero;

            RectTransform textIconRect = BagText.GetComponent<RectTransform>();
            textIconRect.anchoredPosition = Vector2.zero;
        }
    }

    public class BagController
    {
        public BagManger bagManger { get; }
        public PlayerBagItmeManager playerBagItmeManager { get; }
        public BagViewManager bagViewManager;
        Transform parent;
        public BagController(Transform parent)
        {
            this.parent = parent;
            bagManger = new BagManger();
            bagManger.LoadData();
            playerBagItmeManager = new PlayerBagItmeManager();
            playerBagItmeManager.LoadData();
            bagViewManager = new BagViewManager(parent);
        }

        public void LoadInitData()
        {
            for (int i = 0; i < playerBagItmeManager.PlayerBagItems.Count; i++)
            {
                int playItmeIndex = playerBagItmeManager.PlayerBagItems[i].PlayerBagID;

                BagItme bagItme = bagManger.GetBagItme(playItmeIndex);

                BagViewItem bagViewItem = new BagViewItem(parent);
                bagViewItem.BagText.text = bagItme.BagId.ToString();
                bagViewItem.BagIccon.sprite = bagItme.GetSprite;
                bagViewManager.BagViewItems.Add(bagViewItem);
            }
        }
    }


    public class BagManger
    {
        public List<BagItme> BagItmes { get; }
        public BagManger()
        {
            BagItmes = new List<BagItme>();
        }
        public void LoadData()
        {
            string[] tempIconAssets = this.ReadData();
            for (int i = 0; i < tempIconAssets.Length; i++)
            {
                BagItme bagItme = new BagItme(tempIconAssets[i], i);
                BagItmes.Add(bagItme);
            }
        }

        public BagItme GetBagItme(int BagId)
        {
            for (int i = 0; i < BagItmes.Count; i++)
            {
                if (BagId == BagItmes[i].BagId)
                {
                    return BagItmes[i];
                }
            }
            return null;
        }
    }


    public class PlayerBagItmeManager
    {
        public List<PlayerBagItem> PlayerBagItems { get; }

        public PlayerBagItmeManager()
        {
            PlayerBagItems = new List<PlayerBagItem>();
        }

        public void LoadData()
        {
            int[] tempIndex = new int[15];
            for (int i = 0; i < 15; i++)
            {
                tempIndex[i] = i;
            }
            tempIndex.Shuffle();
            for (int i = 0; i < tempIndex.Length; i++)
            {
                Debug.Log(tempIndex[i]);
                PlayerBagItem playerBagItem = new PlayerBagItem(tempIndex[i]);
                PlayerBagItems.Add(playerBagItem);
            }
        }

        public PlayerBagItem GetPlayerItem(int index)
        {
            return PlayerBagItems[index];
        }
    }

    public class PlayerBagItem
    {
        public int PlayerBagID { get; }
        public PlayerBagItem(int playerId)
        {
            PlayerBagID = playerId;
        }
    }

    public class BagItme
    {
        public string BagIcon { get; }
        public int BagId { get; }
        public BagItme(string bagIcon, int id)
        {
            BagIcon = bagIcon;
            BagId = id;
        }

        public Sprite GetSprite
        {
            get
            {
                string path = string.Format("inventory_icons/{0}", BagIcon);
                return Resources.Load<Sprite>(path);
            }
        }
    }
}



