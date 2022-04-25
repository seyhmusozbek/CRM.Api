namespace Core.Core.CrossCutting.Abstract
{
    public interface IBeforeAspect : IAspect
    {
        object OnBefore();
    }
}