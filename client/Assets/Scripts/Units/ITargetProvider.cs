namespace Units
{
    public interface ITargetProvider
    {
        bool TryGetTarget(out ITarget result);
    }
}