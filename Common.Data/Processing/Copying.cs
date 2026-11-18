/* Plan (pseudocode):
1. Accept that returning `default(T)` can be null for reference types -> make the method return nullable `T?`.
2. Allow the input `obj` to be nullable (`T?`) so callers can pass null and we handle it safely.
3. Replace `obj.Equals(default(T))` with a null-safe equality check using `EqualityComparer<T>.Default.Equals(obj, default!)`.
   - This correctly handles both reference types (including null) and value types (including default values).
4. Replace `default(T)` with the simplified `default` expression to satisfy IDE0034.
5. Add `using System.Collections.Generic;` for `EqualityComparer<T>`.
6. Preserve existing cloning behavior: create new instance via `Activator.CreateInstance`, copy properties, skip "Key" for `IKeyable`.
7. Return `T?` (nullable) to avoid CS8603 when `default` (null) is returned.
*/

using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Data.Processing {
    public static class Copying {
        public static T? Clone<T>(this T? obj) {
            if (EqualityComparer<T>.Default.Equals(obj, default!)) {
                return default;
            }

            Type type = obj!.GetType();
            T newObj = (T)Activator.CreateInstance(type)!;

            foreach (var prop in type.GetProperties()) {
                if (obj is IKeyable && prop.Name == "Key") {
                    continue;
                }
                prop.SetValue(newObj, prop.GetValue(obj));
            }
            return newObj;
        }
    }
}
