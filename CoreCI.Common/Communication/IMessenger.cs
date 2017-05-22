using System.Threading.Tasks;

namespace CoreCI.Common.Communication
{
    public interface IMessenger
    {
        Task<bool> SendMessage<TType>(TType obj);
        Task<TType> RecieveMessage<TType>();
        void StopMessenger();
    }
}