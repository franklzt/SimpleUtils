using System.Collections.Generic;


public abstract class RootInterface
{
    public abstract void InitData();
}

public class ViewRoot 
{   


    readonly static ViewRoot Instance = new ViewRoot();
    public static ViewRoot GetViewRoot()
    {
        return Instance;
    }

}
