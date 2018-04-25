using System;
using ED.Common.IoC;

namespace ED.Common.Extensions
{
    public static class IoCContainerExtensions
    {
        public static void AddAspect(this IoCContainer container)
        {
            if(container==null)
                throw new ArgumentNullException(nameof(container));
        }
    }
}
