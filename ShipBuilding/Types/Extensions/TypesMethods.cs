using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ShipBuilding.Types.Extensions
{
    public static class TypesMethods
    {
        /// <summary>Назва статусу з атрибута [Display]; якщо його немає — ім'я значення.</summary>
        public static string GetDisplayName(ShipStatus status)
        {
            FieldInfo? field = status.GetType().GetField(status.ToString());
            if (field == null) return status.ToString();
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? status.ToString();
        }
    }
}
