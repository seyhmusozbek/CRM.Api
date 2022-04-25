using System;
using System.Linq;
using System.Reflection;
using Core.Core.CrossCutting.Abstract;

namespace Core.Core.CrossCutting.Concrete
{
    public class TransparentProxy<T> : DispatchProxy
    {
        private T _decorate;
        protected object response=null;
        
        public static T GenerateProxy(T decorate,IServiceProvider service)
        {
            object instance = Create<T, TransparentProxy<T>>();
            ((TransparentProxy<T>)instance).SetParameters(((T)decorate));
            AspectContext.Instance.Services = service;
            return (T)instance;
        }

        private void SetParameters(T decorated)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorate = decorated;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                var param = targetMethod.GetParameters();
                response = null;
                //Tanımlı Attribute alınıyor.
                //MethodInfo da tanımlı getcustomattribute fonksiyonu çalışmıyor.
                //bu yüzden decorate den alıyoruz attribute listesini
                
                var methodAspects = _decorate.GetType().GetMethod(targetMethod.Name)?.GetCustomAttributes().ToArray();
                var typeAttributes=_decorate.GetType().GetCustomAttributes()?.Where(x => x is ITypeAspectForMethods).ToArray();
                Attribute[] aspects = new Attribute[methodAspects.Length + (typeAttributes?.Length ?? 0)];
                methodAspects.CopyTo(aspects,0);
                typeAttributes.CopyTo(aspects,methodAspects.Length);
                
                //Method bilgileri Aspecte aktarılıyor.
                FillAspectContext(targetMethod, args);

                //object response = null;
                //Method Öncesinde Tanımlı aspect kontrolu
                CheckBeforeAspect( aspects, param);

                // Response boş değilse, buradaki veri cache üzerinden de geliyor olabilir ve tekrardan invoke etmeye
                // gerek yok, direkt olarak geriye response dönebiliriz bu durumda.
                if (response != null)
                {
                    return response;
                }
                //Method çalıştırılıyor.
                var runAttr = aspects.FirstOrDefault(x=>x is IInRunAspect);
                if(runAttr==null)
                {
                    response = targetMethod.Invoke(_decorate, args);
                }
                else
                {
                   response= ((IInRunAspect) runAttr).OnRun(targetMethod,_decorate,args);
                }


                //Method Bittikten sonra çalıştırılacak Aspectler
                CheckAfterAspect(response, aspects);

                // After aspectlerimizi'de çalıştırdıktan sonra artık geriye çıktıyı dönebiliriz.
                return response;
            }
            catch (Exception e)
            {
                return new Exception("");
            }
        }

        protected void FillAspectContext(MethodInfo method,object[] args)
        {
            AspectContext.Instance.Arguments = args;
            AspectContext.Instance.MethodInfo = method;
        }

        protected void CheckBeforeAspect( object[] aspects,ParameterInfo[] prms)
        {
            foreach (IAspect loopAttribute in aspects)
            {
                if (loopAttribute is IBeforeVoidAspect)
                {
                    ((IBeforeVoidAspect)loopAttribute).OnBefore(prms);
                }
                else if (loopAttribute is IBeforeAspect)
                {
                    response = ((IBeforeAspect)loopAttribute).OnBefore();
                }
            }
        }

        protected void CheckAfterAspect(object result, object[] aspects)
        {
            foreach (IAspect loopAttribute in aspects)
            {
                if (loopAttribute is IAfterVoidAspect)
                {
                    ((IAfterVoidAspect)loopAttribute).OnAfter(result);
                }
            }
        }
    }
}