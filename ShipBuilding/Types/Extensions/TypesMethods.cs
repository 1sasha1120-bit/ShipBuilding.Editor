using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ShipBuilding.Types.Extensions
{
    public static class TypesMethods
    {
        public static string GetDisplayName(this ShipStatus status)
        {
            FieldInfo? field = status.GetType().GetField(status.ToString());
            if (field == null) return status.ToString();
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? status.ToString();
        }
    }
}
