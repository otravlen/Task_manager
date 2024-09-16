using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq.Expressions;

namespace Task_manager.Domain
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            return enumValue.GetType().
                GetMember(enumValue.ToString()).
                First().
                GetCustomAttribute<DisplayAttribute>()?.GetName()??"Неопределённый";
        }
    }
    public static class QueryExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> sourse, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                return sourse.Where(predicate);
            return sourse;
        }
    }
}
