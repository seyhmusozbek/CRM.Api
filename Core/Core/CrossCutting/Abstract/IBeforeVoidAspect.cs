using System.Reflection;

namespace Core.Core.CrossCutting.Abstract
{
    public interface IBeforeVoidAspect : IAspect
    {
        void OnBefore(ParameterInfo[] args);
    }
}