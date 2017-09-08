using System;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Exception class for exceptions that occur in the data store.
    /// </summary>
    internal class DataStoreException : ApplicationException
    {
        /// <summary>
        ///     Initializes a new instance of the System.DataStoreException class with a specified error message.
        /// </summary>
        /// <param name="message">
        ///     A message that describes the error.
        /// </param>
        public DataStoreException(String message)
            :   base (message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the System.DataStoreException class with a specified
        ///     error message and a reference to the inner exception that is the cause of this
        ///     exception.
        /// </summary>
        /// <param name="message">
        ///     The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception. If the innerException
        ///     parameter is not a null reference, the current exception is raised in a catch
        ///     block that handles the inner exception.
        /// </param>
        public DataStoreException(String message, Exception innerException)
            :   base(message, innerException)
        {
        }
    }
}
