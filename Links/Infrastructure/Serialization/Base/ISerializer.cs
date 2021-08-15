namespace Links.Infrastructure.Serialization.Base
{
    internal interface ISerializer<T>
    {
        T Deserialize(string data);
        string Serialize(T item);
    }
}
