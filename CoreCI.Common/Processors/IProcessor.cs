namespace CoreCI.Common.Processors
{
    public interface IProcessor
    {
        void Run();
        void Terminate();
    }
}