using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Template class for wrapping items that can be edited, stored in lists
    /// </summary>
    /// <typeparam name="T">
    ///     Contained item type
    /// </typeparam>
    internal class DataStorageItem<T>
    {
        /// <summary>
        ///     Flags to track how a record was added, modified, or deleted.
        /// </summary>
        [Flags]
        public enum ChangeTrackingFlags
        {
            None        = 0x00,
            Added       = 0x01,
            Modified    = 0x02,
            Deleted     = 0x04
        } // enum ChangeTrackingFlags

        #region Privata properties
            /// <summary>
            ///     Wrapped object.
            /// </summary>
            private T mData;

            /// <summary>
            ///     Flags to track changes to the record.
            /// </summary>
            private ChangeTrackingFlags mTrackingFlags = ChangeTrackingFlags.None;
        #endregion Privata properties

        #region Construction
            /// <summary>
            ///     Construct an object that wraps a data object.
            /// </summary>
            /// <param name="dataObject">
            ///     Reference to the data object to store.
            /// </param>
            public DataStorageItem(T dataObject)
            {
                this.mData = dataObject;
            }
        #endregion Construction

        #region Public properties
            /// <summary>
            ///     Returns 
            /// </summary>
            public T Data => this.mData;

            /// <summary>
            ///     Gets or sets the state of the tracking flags.
            /// </summary>
            public ChangeTrackingFlags TrackingFlags
            {
                get
                {
                    return mTrackingFlags;
                }

                set
                {
                    this.mTrackingFlags = value & (ChangeTrackingFlags.Added | ChangeTrackingFlags.Modified | ChangeTrackingFlags.Deleted);
                }
            }

            /// <summary>
            ///     Gets or sets the flag that indicates that the record was added.
            /// </summary>
            public Boolean Added
            {
                get { return (ChangeTrackingFlags.Added == (this.mTrackingFlags & ChangeTrackingFlags.Added)); }
                set
                {
                    if (value == true)
                    {
                        this.mTrackingFlags |= ChangeTrackingFlags.Added;
                    }
                    else
                    {
                        this.mTrackingFlags &= ~ChangeTrackingFlags.Added;
                    }
                }
            }

            /// <summary>
            ///     Gets or sets the flag that indicates that the record was added.
            /// </summary>
            public Boolean Modified
            {
                get { return (ChangeTrackingFlags.Modified == (this.mTrackingFlags & ChangeTrackingFlags.Modified)); }
                set
                {
                    if (value == true)
                    {
                        this.mTrackingFlags |= ChangeTrackingFlags.Modified;
                    }
                    else
                    {
                        this.mTrackingFlags &= ~ChangeTrackingFlags.Modified;
                    }
                }
            }

            /// <summary>
            ///     Gets or sets the flag that indicates that the record was deleted.
            /// </summary>
            public Boolean Deleted
            {
                get { return (ChangeTrackingFlags.Deleted == (this.mTrackingFlags & ChangeTrackingFlags.Deleted)); }
                set
                {
                    if (value == true)
                    {
                        this.mTrackingFlags |= ChangeTrackingFlags.Deleted;
                    }
                    else
                    {
                        this.mTrackingFlags &= ~ChangeTrackingFlags.Deleted;
                    }
                }
            }
        #endregion Public properties.

        #region Public methods
            /// <summary>
            ///     Clear all the change tracking flags.
            /// </summary>
            public void ClearTrackingFlags()
            {
                this.mTrackingFlags = ChangeTrackingFlags.None;
            }
        #endregion Public methods

        #region Static methods
            /// <summary>
            ///     Implicit cast of the object to the contained data object.
            /// </summary>
            /// <param name="val">Reference to an instance of the class from which to retrieve the contained data.</param>
            public static implicit operator T(DataStorageItem<T> val)
            {
                return val.mData;
            }
        #endregion // Static methods
    } // class DataStorageItem
} // namespace HtmlElementsDBEditor
