namespace Links.Infrastructure.Serialization
{
    internal interface ISerializer<T>
    {
        T Deserialize(string data);
        string Serialize(T item);
    }
}
