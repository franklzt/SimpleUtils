namespace SimpleFramework
{
    public class SimpleStruct <T, U>
    {
        public T TData { get; }
        public U UData { get; }

        public SimpleStruct (T t, U u)
        {
            TData = t;
            UData = u;
        }
    }

    public class SimpleStruct <T, U,V>
    {
        public T TData { get; }
        public U UData { get; }
        public V VData { get; }

        public SimpleStruct (T t, U u,V v)
        {
            TData = t;
            UData = u;
        }
    }

    public class SimpleStruct <T, U,V,W>
    {
        public T TData { get; }
        public U UData { get; }
        public V VData { get; }
        public W WData { get; }


        public SimpleStruct (T t, U u, V v,W w)
        {
            TData = t;
            UData = u;
            VData = v;
            WData = w;
        }
    }

    public class SimpleStruct <T, U,V,W,X>
    {
        public T TData { get; }
        public U UData { get; }
        public V VData { get; }
        public W WData { get; }
        public X XData { get; }

        public SimpleStruct (T t, U u, V v, W w,X x)
        {
            TData = t;
            UData = u;
            VData = v;
            WData = w;
            XData = x;
        }
    }
}

