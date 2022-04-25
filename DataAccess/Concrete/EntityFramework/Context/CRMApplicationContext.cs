using System.Reflection;
using Castle.DynamicProxy.Internal;
using Entities.Abstract;
using Entities.Concrete.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class CrmApplicationContext:BaseContext
    {
        public CrmApplicationContext():base()
        {
            SetDb();
        }
        
        public CrmApplicationContext(DbContextOptions options):base(options)
        {
            SetDb();
        }
        
        private  const string EntitiesNameSpace = "Entities.Concrete.Tables";
        private const string EntityMapsNameSpace = "Entities.Concrete.Builders";
        private IEnumerable<Type> _entities;
        private void SetDb()
        {
            _entities = from t in (Assembly.GetAssembly(typeof(BaseEntity))?.GetTypes())
                where t.IsClass && t.Namespace == EntitiesNameSpace
                                && t.GetAllInterfaces().Select(x => x.ToString()).Contains(typeof(IEntity).ToString())
                select t;
            Console.WriteLine("SetDb Çalıştı");
            foreach (var Entity in _entities)
            {
                Console.WriteLine(Entity.Name);
                MethodInfo method = this.GetType().GetMethod("Set",new Type[]{});
                MethodInfo generic = method.MakeGenericMethod(Entity);
                generic.Invoke(this, null);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var map = Assembly.GetAssembly(typeof(BaseEntityMapping<>))?.GetTypes().FirstOrDefault(x => x.Namespace==EntityMapsNameSpace);
            if(map!= null)
             modelBuilder.ApplyConfigurationsFromAssembly(map.Assembly);
        }
    }
}