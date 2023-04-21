namespace Sequence.Serializer;

public interface ISerializer<T>
{
    void Write(T obj);
}
