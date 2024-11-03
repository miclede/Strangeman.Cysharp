namespace Strangeman.Message
{
    public interface IMessage<T>
    {
        T Contents { get; }
        void SetMessage(T content);
        void ResetMessage();
    }
}
