namespace Links.Infrastructure.Serialization.Base
{
    internal abstract class Serializer<T> : SerializerBase, ISerializer<T>
    {
        public abstract T Deserialize(string data);
        public abstract string Serialize(T item);

        public override string ToString()
        {
            return GetInfo().SerializerName;
        }
    }
}
