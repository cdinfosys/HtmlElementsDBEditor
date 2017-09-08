using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Object to split DataStorageItem records into updated, modified, deleted, and untouched collections
    /// </summary>
    internal class AddUpdateDeleteSorter<T>
    {
        #region Private data members
            /// <summary>
            ///     All new records. This collection contains all input records that had their Added flags set but not their Deleted flags.
            /// </summary>
            private List<T> mAddedRecords = new List<T>();

            /// <summary>
            ///     All modified records. This collection contains all records that had their Modified flags set but neither Added nor Deleted set
            /// </summary>
            private List<T> mModifiedRecords = new List<T>();

            /// <summary>
            ///     All deleted records. This collection contains all records that had their Deleted flags set but not their Added flags
            /// </summary>
            private List<T> mDeletedRecords = new List<T>();

            /// <summary>
            ///     All untouched records. This collection contains all records that had neither their Added, Modified, nor Deleted flags set
            /// </summary>
            private List<T> mUntouchedRecords = new List<T>();
        #endregion Private data members

        #region Construction
            public AddUpdateDeleteSorter(IEnumerable<DataStorageItem<T>> inputRecordCollection)
            {
                foreach (DataStorageItem<T> rec in inputRecordCollection)
                {
                    if (rec.Added && !rec.Deleted)
                    {
                        // New records that were not deleted
                        this.mAddedRecords.Add(rec);
                    }
                    else if (rec.Deleted && !rec.Added)
                    {
                        // Deleted records that were not added now
                        this.mDeletedRecords.Add(rec);
                    }
                    else if (rec.Modified && !rec.Deleted)
                    {
                        // Modified records that were not deleted
                        this.mModifiedRecords.Add(rec);
                    }
                    else if (rec.TrackingFlags == DataStorageItem<T>.ChangeTrackingFlags.None)
                    {
                        // All records that were not added, modified, or deleted
                        this.mUntouchedRecords.Add(rec);
                    }
                }
            }
        #endregion Construction

        #region Public accessor methods
            /// <summary>
            ///     Gets a list of records that were added.
            /// </summary>
            public IEnumerable<T> AddedRecords => this.mAddedRecords;

            /// <summary>
            ///     Gets a list of records that were added.
            /// </summary>
            public IEnumerable<T> ModifiedRecords => this.mModifiedRecords;

            /// <summary>
            ///     Gets a list of records that were deleted.
            /// </summary>
            public IEnumerable<T> DeletedRecords => this.mDeletedRecords;

            /// <summary>
            ///     Gets a list of records that were not changed.
            /// </summary>
            public IEnumerable<T> UntouchedRecords => this.mUntouchedRecords;

        #endregion Public accessor methods
    }
}
