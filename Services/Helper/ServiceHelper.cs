using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class ServiceHelper
    {
        public static IComparer<T> NullsLast<T>(this IComparer<T> inner)
        => Comparer<T>.Create((a, b) =>
        {
            if (a is null && b is null) return 0; // iguais
            if (a is null) return 1;              // a vem depois de b
            if (b is null) return -1;             // b vem depois de a
            return inner.Compare(a, b);           // compara normalmente
        });

    }
}
