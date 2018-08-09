using System.Collections.Generic;

namespace SimpleFramework
{
    public  class SimpleBase
    {
        public int CompareID { get; }
        public bool TheSameAs(SimpleBase other)
        {
            return CompareID == other.CompareID;
        }

        public SimpleBase(int CompareID)
        {
            this.CompareID = CompareID;
        }
    }

    public class SimpleGeneric<T, U> where T : SimpleStruct<SimpleBase, U>
    {
        List<SimpleStruct<SimpleBase, U>> listData = new List<SimpleStruct<SimpleBase, U>>();

        public void AddOrUpdate(SimpleStruct<SimpleBase, U> other)
        {
            for (int i = 0; i < listData.Count; i++)
            {
                if(listData[i].TData.TheSameAs(other.TData))
                {
                    listData[i] = other;
                    break;
                }              
            }
            listData.Add(other);
        }

        public SimpleStruct<SimpleBase, U> GetItemData(int CompareID)
        {
            for (int i = 0; i < listData.Count; i++)
            {
                if (CompareID == listData[i].TData.CompareID)
                {
                    return listData[i];
                }
            }
            return null;
        }

        public virtual void LoadData()
        {

        }
    }

    public class SimpleModel
    {
        public string modelData { get; }
        public SimpleModel(string newModelData)
        {
            modelData = newModelData;
        }
    }

 
    public class SimpleModelManager : SimpleGeneric<SimpleStruct<SimpleBase, SimpleModel>, SimpleModel> 
    {
        public SimpleModelManager()
        {
            
        }

    }

    public  static class StructHelp
    {
        public static SimpleStruct<SimpleBase, U> Construct<U>(this object obj, int CompareId,U u)
        {
            return new SimpleStruct<SimpleBase, U>(new SimpleBase(CompareId), u);
        }
    }

    public class Test
    {
        public  void AssertTest()
        {
            SimpleModelManager simpleModelManager = new SimpleModelManager();
            simpleModelManager.AddOrUpdate(this.Construct(1, new SimpleModel("SimpleModelData")));
        }
    }
        
}
