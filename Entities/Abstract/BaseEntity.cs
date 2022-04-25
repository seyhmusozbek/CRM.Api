using System.Text.Json.Serialization;

namespace Entities.Abstract
{
    public abstract class BaseEntity:IEntity
    {
        public BaseEntity()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                var PropType = prop.PropertyType;
                var den= PropType.IsGenericType? PropType.GenericTypeArguments:null;
                var den1 = PropType.IsGenericType && PropType.GetGenericTypeDefinition().GetInterfaces().Select(x => x.ToString()).Contains(typeof(IEnumerable<>).ToString());
                if (den!=null&&PropType.IsGenericType && den1)
                {
                    var genericType = typeof(List<>).MakeGenericType(den[0]);
                    var active = Activator.CreateInstance(genericType);
                    prop.SetValue(this, active);
                }
            }
        }

        public int Id { get; set; }
        
        [JsonIgnore]
        public byte[] TimeStamp { get; set; }
    }
}