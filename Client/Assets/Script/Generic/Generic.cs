using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class FactoryTest
    {
        public void Test()
        {
            BagFactory bagFactory = new BagFactory();
            bagFactory.InitData();
            PlayerFactory playerFactory = new PlayerFactory();
            playerFactory.InitData();

            bool sameID = bagFactory.GetItmeID(5).ItemID == 5;
            Debug.Assert(sameID, string.Format("Get the same in bagFactory: ",sameID));

            sameID = playerFactory.GetItmeID(5).ItemID == 5;
            Debug.Assert(sameID,string.Format("Get the same in playerFactory: ", sameID));
        }       
    }

    public abstract class BaseFactory<T> : Factory<T> where T : AbstractItmeBase
    {
        public abstract void InitData();
    }

    public class BagFactory : BaseFactory<BagItmeTest>
    {
        public override void InitData()
        {
            for (int i = 0; i < 15; i++)
            {
                AddOrUpdate(new BagItmeTest() { ItemID = i, BagItmeName = string.Format("BagItmeName: {0}", i) });
            }
        }
    }

    public class PlayerFactory : BaseFactory<PlayerItmeTest>
    {
        public override void InitData()
        {
            for (int i = 0; i < 10; i++)
            {
                AddOrUpdate(new PlayerItmeTest() { ItemID = i, PlayerItmeName = string.Format("PlayerName: {0}", i) });
            }
        }
    }


    public class Factory<T> where T : AbstractItmeBase
    {
        protected List<T> ItmeList = new List<T>();
        public void AddOrUpdate(T newValue)
        {
            for (int i = 0; i < ItmeList.Count; i++)
            {
                if (ItmeList[i].TheSameAs(newValue))
                {
                    ItmeList[i] = newValue;
                    return;
                }
            }
            ItmeList.Add(newValue);
        }

        public T GetItmeIndex(int index)
        {
            return ItmeList[index];
        }

        public T GetItmeID(int Id)
        {
            for (int i = 0; i < ItmeList.Count; i++)
            {
                if (ItmeList[i].ItemID == Id)
                {
                    return ItmeList[i];
                }
            }
            return null;
        }
    }


    public abstract class AbstractItmeBase
    {
        public int ItemID = 0;
        public abstract bool TheSameAs(AbstractItmeBase value);
    }

    public class ItmeBase : AbstractItmeBase
    {
        public override bool TheSameAs(AbstractItmeBase value)
        {
            return this.ItemID == value.ItemID;
        }
    }

    public class BagItmeTest : ItmeBase
    {
        public string BagItmeName = "BagItmeName";
    }

    public class PlayerItmeTest : ItmeBase
    {
        public string PlayerItmeName = "PlayerItmeName";
    }
}


