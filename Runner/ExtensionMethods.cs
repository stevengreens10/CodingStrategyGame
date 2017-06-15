using Newtonsoft.Json;
using System;

namespace CSharpRunner
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            return (T)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(source));
        }
    }
}
