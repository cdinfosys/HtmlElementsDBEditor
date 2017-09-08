using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using HtmlElementsDB;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Loads data from a SQLite database
    /// </summary>
    /// <remarks>
    ///     This version supports database schema version 1.
    /// </remarks>
    internal class SQLiteDataAccess_v1
    {
        #region SQL statements
            private const String SQLite_Select_AttributeTypes = @"SELECT AttributeTypeID, Description FROM AttributeType";

            private const String SQLite_Add_AttributeType = @"INSERT INTO AttributeType(AttributeTypeID, Description) VALUES ({0}, {1})";
            private const String SQLite_Update_AttributeType = @"UPDATE AttributeType SET Description = '{1}' WHERE AttributeTypeID = {0}";
            private const String SQLite_Delete_AttributeType = @"DELETE FROM AttributeType WHERE AttributeTypeID = {0}";
        #endregion SQL statements

        #region Private data members
        /// <summary>
        ///     Reference to the database connection object.
        /// </summary>
        private SQLiteConnection mDbConnection;
        #endregion Private data members

        #region Construction
            /// <summary>
            ///     Construct an object to read the data from the database.
            /// </summary>
            /// <param name="dbConnection">
            ///     Reference to a connection object to access the database.
            /// </param>
            public SQLiteDataAccess_v1(SQLiteConnection dbConnection)
            {
                this.mDbConnection = dbConnection;
            }
        #endregion  // Construction

        #region Public accessor methods
            /// <summary>
            ///     Reads the data from the AttributeType table.
            /// </summary>
            /// <returns>
            ///     Returns a list of <see cref="AttributeTypeDTO"/> objects.
            /// </returns>
            public IEnumerable<AttributeTypeDTO> FetchAttributeTypes()
            {
                List<AttributeTypeDTO> result = new List<AttributeTypeDTO>();

                using (SQLiteCommand dbCommand = this.mDbConnection.CreateCommand())
                {
                    dbCommand.CommandText = SQLite_Select_AttributeTypes;
                    dbCommand.CommandType = CommandType.Text;
                    using (SQLiteDataReader reader = dbCommand.ExecuteReader())
                    {
                        int colIndexAttributeTypeId = reader.GetOrdinal("AttributeTypeId");
                        int colIndexDescription = reader.GetOrdinal("Description");

                        while (reader.Read())
                        {
                            result.Add
                            (
                                new AttributeTypeDTO
                                (
                                    Convert.ToUInt32(reader.GetInt32(colIndexAttributeTypeId)),
                                    reader.GetString(colIndexDescription)
                                )
                            );
                        }
                    }
                }

                return result;
            }

            /// <summary>
            ///     Add, update, and delete records in the AttributeTypes table
            /// </summary>
            /// <param name="addedRecords"></param>
            /// <param name="updatedRecords"></param>
            /// <param name="deletedRecords"></param>
            public void AddUpdateDelete_AttributeTypes
            (
                IEnumerable<AttributeTypeDTO> addedRecords, 
                IEnumerable<AttributeTypeDTO> updatedRecords,
                IEnumerable<AttributeTypeDTO> deletedRecords
            )
            {
                foreach (AttributeTypeDTO addedRecord in addedRecords)
                {
                    using (SQLiteCommand dbCommand = new SQLiteCommand())
                    {
                        dbCommand.CommandText = String.Format(SQLite_Add_AttributeType, addedRecord.AttributeTypeId, addedRecord.Description);
                        dbCommand.CommandType = CommandType.Text;
                        dbCommand.ExecuteNonQuery();
                    }
                }

                foreach (AttributeTypeDTO updatedRecord in addedRecords)
                {
                    using (SQLiteCommand dbCommand = new SQLiteCommand())
                    {
                        dbCommand.CommandText = String.Format(SQLite_Update_AttributeType, updatedRecord.AttributeTypeId, updatedRecord.Description);
                        dbCommand.CommandType = CommandType.Text;
                        dbCommand.ExecuteNonQuery();
                    }
                }

                foreach (AttributeTypeDTO deletedRecord in deletedRecords)
                {
                    using (SQLiteCommand dbCommand = new SQLiteCommand())
                    {
                        dbCommand.CommandText = String.Format(SQLite_Delete_AttributeType, deletedRecord.AttributeTypeId);
                        dbCommand.CommandType = CommandType.Text;
                        dbCommand.ExecuteNonQuery();
                    }
                }
            }
        #endregion // Public accessor methods
    } // class DataLoader_v1
} // namespace HtmlElementsDBEditor
