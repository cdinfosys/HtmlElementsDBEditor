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

            private const String SQLite_Add_AttributeType = @"INSERT INTO AttributeType(AttributeTypeID, Description) VALUES ($pAttributeTypeId, $pDescription)";
            private const String SQLite_Update_AttributeType = @"UPDATE AttributeType SET Description = $pDescription WHERE AttributeTypeID = $pAttributeTypeId";
            private const String SQLite_Delete_AttributeType = @"DELETE FROM AttributeType WHERE AttributeTypeID = $pAttributeTypeId";
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
                SQLiteParameter paramDescription = new SQLiteParameter("$pDescription", DbType.String)
                {
                    Direction = ParameterDirection.Input
                };

                SQLiteParameter paramAttributeId = new SQLiteParameter("$pAttributeTypeId", DbType.Int32)
                {
                    Direction = ParameterDirection.Input
                };

                if (addedRecords != null)
                {

                    foreach (AttributeTypeDTO addedRecord in addedRecords)
                    {
                        paramAttributeId.Value = addedRecord.AttributeTypeId;
                        paramDescription.Value = addedRecord.Description;

                        using (SQLiteCommand dbCommand = this.mDbConnection.CreateCommand())
                        {
                            dbCommand.CommandText = SQLite_Add_AttributeType;
                            dbCommand.CommandType = CommandType.Text;
                            dbCommand.Parameters.Add(paramAttributeId);
                            dbCommand.Parameters.Add(paramDescription);
                            dbCommand.ExecuteNonQuery();
                        }
                    }
                }

                if (updatedRecords != null)
                {
                    foreach (AttributeTypeDTO updatedRecord in updatedRecords)
                    {
                        paramAttributeId.Value = updatedRecord.AttributeTypeId;
                        paramDescription.Value = updatedRecord.Description;

                        using (SQLiteCommand dbCommand = this.mDbConnection.CreateCommand())
                        {
                            dbCommand.CommandText = SQLite_Update_AttributeType;
                            dbCommand.CommandType = CommandType.Text;
                            dbCommand.Parameters.Add(paramAttributeId);
                            dbCommand.Parameters.Add(paramDescription);
                            dbCommand.ExecuteNonQuery();
                        }
                    }
                }

                if (deletedRecords != null)
                {
                    foreach (AttributeTypeDTO deletedRecord in deletedRecords)
                    {
                        paramAttributeId.Value = deletedRecord.AttributeTypeId;
                        using (SQLiteCommand dbCommand = this.mDbConnection.CreateCommand())
                        {
                            
                            dbCommand.CommandText = SQLite_Delete_AttributeType;
                            dbCommand.CommandType = CommandType.Text;
                            dbCommand.Parameters.Add(paramAttributeId);
                            dbCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        #endregion // Public accessor methods
    } // class DataLoader_v1
} // namespace HtmlElementsDBEditor
