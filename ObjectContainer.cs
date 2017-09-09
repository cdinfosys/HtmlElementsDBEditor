using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Wrapper around an object. This is used to pass references to objects around without the need for the ref keyword.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of object to store
    /// </typeparam>
    public struct ObjectContainer<T> where T : class
    {
        public T ContainedObject;

        public ObjectContainer(T value)
        {
            ContainedObject = value;
        }
    }
}
