using System.Reflection;

namespace Core.Core.CrossCutting.Abstract
{
    public interface IInRunAspect
    {
        object OnRun<T>(MethodInfo method,T decorate,object[] args);
        int Order { get; set; }
    }
}