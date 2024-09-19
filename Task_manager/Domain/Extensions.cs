using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq.Expressions;
using Task_manager.Enum;
using Task_manager.Models;

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
        public static Priority GetPriorityFromDisplayName(this TaskVievModel model)
        {
            switch (model.Priority)
            {
                case "0":
                    return Priority.Easy;
                case "1":
                    return Priority.Medium;
                case "2":
                    return Priority.High;
                default:
                    return Priority.Easy; 
            }
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
