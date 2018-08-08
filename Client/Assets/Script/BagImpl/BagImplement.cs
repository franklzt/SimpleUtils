using RandomElement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BagImplement
{
    public class BagImplement : MonoBehaviour
    {
        private BagController BagController;
        public Transform viewParent;
        private void Start()
        {
            BagController = new BagController(viewParent);
            BagController.LoadInitData();
        }
    }

    public class BagController
    {
        public BagManger bagManger { get; }
        public PlayerBagItmeManager playerBagItmeManager { get; }
        public BagView BagView;
        private readonly Transform parent;
        public BagController(Transform parent)
        {
            this.parent = parent;
            bagManger = new BagManger();
            bagManger.LoadData();
            playerBagItmeManager = new PlayerBagItmeManager();
            playerBagItmeManager.LoadData();
            BagView = new BagView();
        }

        public void LoadInitData()
        {
            BagItme bagFrameItem = bagManger.GetFrameItem();
            Font font = Resources.Load<Font>("arial");

            for (int i = 0; i < playerBagItmeManager.PlayerBagItems.Count; i++)
            {
                int playItmeIndex = playerBagItmeManager.PlayerBagItems[i].PlayerBagID;

                BagItme bagItme = bagManger.GetBagItme(playItmeIndex);

                BagViewItem bagViewItem = new BagViewItem(parent);
                bagViewItem.BagText.text = bagItme.BagId.ToString();
                bagViewItem.BagIccon.sprite = bagItme.GetSprite;
                bagViewItem.UpdateFrameIcon(bagFrameItem.GetSprite);
                bagViewItem.UpdateFront(font);
                BagView.Add(bagViewItem);
            }


        }
    }
    public class BagView
    {
        public List<BagViewItem> BagViewItems { get; }
        public BagView()
        {
            BagViewItems = new List<BagViewItem>();
        }

        public void Add(BagViewItem bagViewItem)
        {
            BagViewItems.Add(bagViewItem);
        }
    }
    public class BagViewItem
    {
        public Image BagIccon { get; }
        public Text BagText { get; }
        public Image BagBG { get; }

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
            BagText.color = Color.black;




            RectTransform bagIconRect = BagIccon.GetComponent<RectTransform>();
            bagIconRect.anchoredPosition = Vector2.zero;
            RectTransform textIconRect = BagText.GetComponent<RectTransform>();
            textIconRect.anchoredPosition = new Vector2(0, -38);
            textIconRect.sizeDelta = new Vector2(90, 18);

            BagBG = new GameObject("Frame").AddComponent<Image>();
            BagBG.transform.SetParent(bagViewRoot.transform);
            RectTransform bagBGIconRect = BagBG.GetComponent<RectTransform>();
            bagBGIconRect.anchoredPosition = Vector2.zero;
        }

        public void UpdateFrameIcon(Sprite frameSprite)
        {
            BagBG.sprite = frameSprite;
        }

        public void UpdateFront(Font font)
        {
            BagText.font = font;
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
    public class BagManger
    {
        public List<BagItme> BagItmes { get; }
        public int FrameItemIndex { get; }

        public BagManger()
        {
            BagItmes = new List<BagItme>();
            FrameItemIndex = 11;
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

        public BagItme GetFrameItem()
        {
            return BagItmes[FrameItemIndex];
        }


        public BagItme GetBagItme(int BagId)
        {
            if (BagId == FrameItemIndex)
            {
                return BagItmes[20];
            }

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

namespace SimpleFramework
{
    public class SimpleBase
    {
        public int CompareID = 0;
        public bool TheSameAs(SimpleBase other)
        {
            return CompareID == other.CompareID;
        }
    }

    public class SimpleData<T, U> where T : SimpleBase
    {
        public U ReferenceU { get; set; }
        public T ReferenceT { get; }
        public SimpleData(U referenceU)
        {
            ReferenceT = default(T);
            ReferenceU = referenceU;
        }
    }

    public class SimpleDataManager<T, U> where T : SimpleBase
    {
        List<SimpleData<T, U>> listData = new List<SimpleData<T, U>>();
        public void AddOrUpdate(SimpleData<T, U> other)
        {
            for (int i = 0; i < listData.Count; i++)
            {
                if (listData[i].ReferenceT.TheSameAs(other.ReferenceT))
                {
                    listData[i] = other;
                    return;
                }
            }
            listData.Add(other);
        }
    }

    public class SimpleModel
    {
        public string modelData = "modelData";
    }

    public class SimpleModelManager : SimpleDataManager<SimpleBase, SimpleModel>
    {

    }

}