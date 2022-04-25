namespace Core.Core.CrossCutting.Abstract
{
    public interface IAfterVoidAspect : IAspect
    {
        void OnAfter(object result);
    }
}