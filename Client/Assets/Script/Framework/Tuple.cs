namespace Tuple
{
    public class BindingTuple<T, U>
    {
        public T TValue { get; }
        public U UValue { get; }

        public BindingTuple(T t, U u)
        {
            TValue = t;
            UValue = u;
        }
    }

    public class Tuple<Model, View> where View : UpdateUI<Model, View>
    {
        public Model TValue { get; }
        public View UValue { get; }

        public Tuple(Model newT, View newU)
        {
            TValue = newT;
            UValue = newU;
        }

        public void UpdateData(Model Model, View view)
        {
            UValue.UpdateData(Model, view);
        }
    }
}

