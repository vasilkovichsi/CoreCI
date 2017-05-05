namespace CoreCI.Common.IoC
{
    public enum LifeTimeManager
    {
        PerResolve = 0,
        ContainerControlled = 1,
        PerThread = 2,
        Hierarchical = 3,
        ExternallyControlled = 4,
        Transient = 5
    }
}