using System;
using System.Reflection;

namespace Core.Core.CrossCutting.Concrete
{
    public class AspectContext
    {
        private static readonly  Lazy<AspectContext> _instance=new Lazy<AspectContext>(()=>new AspectContext());

        private AspectContext()
        {

        }

        public static AspectContext Instance => _instance.Value;

        public object[] Arguments { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public IServiceProvider Services { get; set; }
    }
}