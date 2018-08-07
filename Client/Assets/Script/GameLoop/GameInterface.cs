public interface GameRoot<T, U>
{
    void UpdateData(T t, U u);
}

public interface View
{

}

public interface LocalModel
{

}

public interface NetWorkModel
{

}

public interface ViewSendLocalData<L, V> : GameRoot<L, V> where L : LocalModel where V : View
{
    L LocalData { get; }
    V ViewData { get; }
}

public interface UpdateLocalModelByNetModel<L, N> : GameRoot<L, N> where L : LocalModel where N : NetWorkModel
{
    L LocalData { get; }
    N NetworkData { get; }
}

public interface UpdateViewByLocalData<V, L> : GameRoot<V, L> where V : View where L : LocalModel
{
    V ViewData { get; }
    L LocalData { get; }
}

public interface RootManager<L,V,N> where L : LocalModel where V : View where N : NetWorkModel
{
    ViewSendLocalData<L,V> ViewToLocalData { get; set; }
    UpdateLocalModelByNetModel<L,N> NetDataToLocal { get; set; }
    UpdateViewByLocalData<V,L> LocalDataToView { get; set; }
}