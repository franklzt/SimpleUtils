using System.Collections.Generic;

public delegate void ItemGenerate<T>(T function);


public class Bag
{
    List<BagItme> TotalItmes { get; } = new List<BagItme>();

    public BagItme GetBagItmeByIndex(int index)
    {
        return TotalItmes[index];
    }

    public void SetupData(List<BagItme> newBagItmeList, ItemGenerate<BagItme> function)
    {
        for (int i = 0; i < TotalItmes.Count; i++)
        {
            function(newBagItmeList[i]);
        }
    }

    public void SetupData(ItemGenerate<BagItme> function)
    {
        for (int i = 0; i < TotalItmes.Count; i++)
        {
            function(TotalItmes[i]);
        }
    }

    public void InitData()
    {
       string[] iconNames = this.ReadData();
       for (int i = 0; i < iconNames.Length; i++)
       {
         BagItme bagItme = new BagItme(i, iconNames[i]);
         TotalItmes.Add(bagItme);
       }
    }
}

public class RootItem<T>
{
    
}
public class BagItme : RootItem<BagItme>
{
    public BagItme(int newBagID,string newIconName)
    {
        BagID = newBagID;
        BagIconName = newIconName;
    }
    public string BagIconName { get; } = "";
    public int BagID { get; }
}