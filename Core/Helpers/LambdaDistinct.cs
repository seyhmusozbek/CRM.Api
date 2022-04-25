using System.Linq.Expressions;

namespace Core.Helpers
{
    public class LambdaDistinct<T> : IEqualityComparer<T>
    {
        private Expression<Func<T,T, bool>> _expression;

        public Expression<Func<T, int>> HashCode { get; }

        public LambdaDistinct(Expression<Func<T,T, bool>> lambda, Expression<Func<T,int>> hashCode)
        {
            _expression = lambda;
            HashCode = hashCode;
        }

        public bool Equals(T x, T y)
        {
            return _expression.Compile()(x, y);
        }

        public int GetHashCode(T obj)
        {  
            return HashCode.Compile()(obj);
        }
    }
}