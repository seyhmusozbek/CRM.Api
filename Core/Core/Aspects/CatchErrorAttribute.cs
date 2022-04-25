using System.Reflection;
using Core.Core.CrossCutting.Abstract;
using Core.Core.CrossCutting.Concrete;
using Core.Models.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Core.Aspects
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class CatchErrorAttribute:AspectBaseAttribute,IInRunAspect,ITypeAspectForMethods
    {
        public object OnRun<T>(MethodInfo method, T decorate, object[] args)
        {
            try
            {
                return method.Invoke(decorate, args);
            }
            catch (Exception e)
            {
                var logger = AspectContext.Instance.Services.GetRequiredService<ILogger<T>>();
                logger.Log(
                    LogLevel.Error, 
                    $"Error: {decorate.GetType().FullName}->{method.Name}",
                    e.InnerException?.Message ?? e.Message);
                var rtType = method.ReturnType;
                if (!(typeof(IServiceResponse).IsAssignableFrom(rtType)))
                {
                    return null;
                };
                var rt =Activator.CreateInstance(rtType) as IServiceResponse;
                
                
                rt!.ExceptionMessage = e.InnerException?.Message ?? e.Message;
                return rt;
            }
        }

        public int Order { get; set; }
    }
}