using Entities;
using ServiceContracts.DTO.Person;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class FilterHelper
    {
        public static Func<T, bool> MakePredicate<T>(string propName, string? search)
        {
            if (string.IsNullOrWhiteSpace(propName) || string.IsNullOrWhiteSpace(search))
                return _ => true;

            var pi = typeof(T).GetProperty(
                propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi is null) return _ => true;

            var t = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType;

            // string Contains (case-insensitive)
            if (t == typeof(string))
                return p => ((string?)pi.GetValue(p))?
                    .Contains(search, StringComparison.OrdinalIgnoreCase) == true;

            // bool (aceita: true/false, 1/0, yes/no, y/n, sim/não)
            if (t == typeof(bool))
            {
                if (!TryParseBool(search, out var b)) return _ => false;
                return p => pi.GetValue(p) is bool v && v == b;
            }

            // DateTime compara apenas a data (ajuste se quiser horário)
            if (t == typeof(DateTime))
            {
                return p =>
                {
                    var date = pi.GetValue(p) as DateTime?;
                    if (date is null) return false;

                    // formata no padrão "dd MMM yyyy"
                    var formatted = date.Value.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);

                    // compara com Contains (case-insensitive)
                    return formatted.Contains(search, StringComparison.OrdinalIgnoreCase);
                };
            }

            // Guid igualdade (se não parsear, faz Contains no ToString)
            if (t == typeof(Guid))
            {
                if (Guid.TryParse(search, out var g))
                    return p => (pi.GetValue(p) as Guid?) == g;

                var s = search.ToLowerInvariant();
                return p => pi.GetValue(p)?.ToString()!
                             .ToLowerInvariant().Contains(s) == true;
            }

            // Números
            if (TryChangeType(search, t, out var typed))
                return p =>
                {
                    var v = pi.GetValue(p);
                    if (v is null) return false;
                    // normaliza Nullable<T>
                    if (pi.PropertyType != t)
                        v = Convert.ChangeType(v, t, CultureInfo.InvariantCulture);
                    return Equals(v, typed);
                };

            // Fallback: ToString().Contains
            return p => pi.GetValue(p)?.ToString()?
                            .Contains(search, StringComparison.OrdinalIgnoreCase) == true;
        }

        private static bool TryParseBool(string s, out bool value)
        {
            var txt = s.Trim().ToLowerInvariant();
            if (txt is "true" or "1" or "yes" or "y" or "sim" or "s") { value = true; return true; }
            if (txt is "false" or "0" or "no" or "n" or "nao" or "não") { value = false; return true; }
            return bool.TryParse(s, out value);
        }

        private static bool TryParseDate(string s, out DateTime dt) =>
            DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt) ||
            DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

        private static bool TryChangeType(string s, Type t, out object? value)
        {
            // tenta Invariant e Current 
            try { value = Convert.ChangeType(s, t, CultureInfo.InvariantCulture); return true; }
            catch
            {
                try { value = Convert.ChangeType(s, t, CultureInfo.CurrentCulture); return true; }
                catch { value = null; return false; }
            }
        }
    }
}
