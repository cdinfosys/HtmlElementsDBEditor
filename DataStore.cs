using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using HtmlElementsDB;
using System.Data.SQLite;
using System.Data;
using HtmlElementsDBEditor.Properties;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Data storage manager for the program.
    /// </summary>
    internal sealed class DataStore
    {
        #region MetaDataTable record identifiers
            /// <summary>
            ///     ID of the record in MetaDataTable that stores the database schema version
            /// </summary>
            const Int32 METADATA_ID_DATABASE_SCHEMA_VERSION = 1;

            /// <summary>
            ///     Identifier for schema version 1
            /// </summary>
            const Int32 METADATA_ID_DATABASE_SCHEMA_VERSION_1 = 1;
        #endregion MetaDataTable record identifiers

        #region Constant declarations
            /// <summary>
            ///     Current database schema version ID
            /// </summary>
            const Int32 DATABASE_SCHEMA_VERSION = 1;
        #endregion // Constant declarations

        #region SQL statements
            /// <summary>
            ///     SQL command used to create the MetaDataTable table
            /// </summary>
            private const String SQLite_CreateMetaDataTable = @"
                CREATE TABLE MetaDataTable
                (
                    MetaDataId INTEGER NOT NULL PRIMARY KEY,
                    Code VARCHAR(50),
                    IntegralValue INTEGER NULL,
                    StringValue TEXT NULL
                )
            ";

            /// <summary>
            ///     SQL command used to create the AttributeTypes table
            /// </summary>
            private const String SQLite_CreateAttributeTypes = @"
                CREATE TABLE AttributeType
                (
                    AttributeTypeId INTEGER NOT NULL PRIMARY KEY,
                    Description VARCHAR(80) NOT NULL
                )
            ";

            /// <summary>
            ///     SQL command used to insert the database schema version number into the MetaDataTable table
            /// </summary>
            private const String SQLite_Insert_MetaData_DatabaseSchemaVersion = @"INSERT INTO MetaDataTable(MetaDataId, Code, IntegralValue) VALUES (1, 'DBSchemaVersion', 1)";

            /// <summary>
            ///     Retrieve all values from the MetaDataTable table
            /// </summary>
            private const String SQLite_Select_MetaDataTable = @"SELECT MetaDataId, Code, IntegralValue, StringValue FROM MetaDataTable";
        #endregion SQL statements

        #region Class members
        /// <summary>
        ///     The only instance of the class.
        /// </summary>
        private static DataStore mInstance;
        #endregion Class members

        #region Private data members
            /// <summary>
            ///     Flag to indicate that the contents of the document were modified.
            /// </summary>
            private Boolean mIsModified = false;

            /// <summary>
            ///     Flag to indicate if the document is loaded.
            /// </summary>
            private Boolean mIsLoaded = false;

            /// <summary>
            ///     Database schema version
            /// </summary>
            private Int32 mDatabaseSchemaVersion = -1;

            /// <summary>
            ///     Stores HTML element objects.
            /// </summary>
            private List<DataStorageItem<HtmlElementDTO>> mHtmlElements = new List<DataStorageItem<HtmlElementDTO>>();

            /// <summary>
            ///     Stores attribute data types.
            /// </summary>
            private List<DataStorageItem<AttributeTypeDTO>> mAttributeDataTypes = new List<DataStorageItem<AttributeTypeDTO>>();

            /// <summary>
            ///     Path to the document file.
            /// </summary>
            private String mDocumentPath = String.Empty;
        #endregion Private data members

        #region Construction
            /// <summary>
            ///     Default constructor is hidden.
            /// </summary>
            private DataStore()
            {
            }
        #endregion Construction

        #region Events
            /// <summary>
            ///     This event is raised when a document is opened or closed. The parameter indicates if the document is opened or closed.
            /// </summary>
            public event  EventHandler<Boolean> DocumentOpen;
        #endregion Events

        #region Public class properties
            /// <summary>
            ///     Gets a reference to the only instance of the class.
            /// </summary>
            public static DataStore Instance => DataStore.mInstance ?? (DataStore.mInstance = new DataStore());
        #endregion // Public class properties

        #region Public properties
            /// <summary>
            ///     Gets or sets the flag that indicates that the data was modified.
            /// </summary>
            public Boolean IsModified
            {
                get { return this.mIsModified; }
                set { this.mIsModified = value; }
            }

            /// <summary>
            ///     Returns a flag that indicates if the document is loaded.
            /// </summary>
            public Boolean IsLoaded => this.mIsLoaded;

            /// <summary>
            ///     Gets the path to the document.
            /// </summary>
            public String FilePath => this.mDocumentPath;
        #endregion // Public properties

        #region Public methods
            /// <summary>
            ///     Load data.
            /// </summary>
            /// <param name="dbFilePath">
            ///     Path to the file where the SQLite database is located.
            /// </param>
            public void Load(String dbFilePath)
            {
                // First check if there are changes to save
                bool canLoad = false;
                if (this.IsModified)
                {
                    if (this.Close())
                    {
                        canLoad = true;
                    }
                }
                else
                {
                    canLoad = true;
                }

                // Check if we can proceed with loading the new document.
                if (canLoad)
                {
                    LoadDocument(dbFilePath);
                }
            }

            /// <summary>
            ///     Save data.
            /// </summary>
            public void Save()
            {
                if (String.IsNullOrWhiteSpace(this.mDocumentPath))
                {
                    SaveAs();
                }
                else
                {
                    UpdateDocumentChanges();
                }
            }

            /// <summary>
            ///     Save data.
            /// </summary>
            /// <param name="filePath">
            ///     Path to the output file
            /// </param>
            public void SaveAs()
            {
                String newFilePath = this.mDocumentPath;
                FrameWindow frameWindow = Application.OpenForms[0] as FrameWindow;
                if (frameWindow.GetFilePath(ref newFilePath))
                {
                    SaveNewDocument(newFilePath);
                    this.mDocumentPath = newFilePath;
                }
            }

            /// <summary>
            ///     Create a new document.
            /// </summary>
            public void CreateNew()
            {
                // First check if there are changes to save
                bool canCreate = false;
                if (this.IsModified)
                {
                    if (this.Close())
                    {
                        canCreate = true;
                    }
                }
                else
                {
                    canCreate = true;
                }

                // Check if we can proceed with loading the new document.
                if (canCreate)
                {
                    CreateDocument();
                }
            }

            /// <summary>
            ///     Close the current document.
            /// </summary>
            /// <returns>
            ///     Returns <c>true</c> if the document was closed or <c>false</c> if the user cancelled the close operation.
            /// </returns>
            public Boolean Close()
            {
                if (this.IsModified)
                {
                    FrameWindow frameWindow = Application.OpenForms[0] as FrameWindow;
                    if (frameWindow != null)
                    {
                        Boolean result = frameWindow.QueryCanClose();
                        if (result == true)
                        {
                            CloseDocument();
                        }
                        return result;
                    }
                }

                return true;
            }

            /// <summary>
            ///     Adds an attribute type to the collection.
            /// </summary>
            /// <param name="attributeType">
            ///     New value.
            /// </param>
            public void AddAttributeDataType(AttributeTypeDTO attributeType)
            {
                DataStorageItem<AttributeTypeDTO> newRec = new DataStorageItem<AttributeTypeDTO>(new AttributeTypeDTO(GetNextAttributeDataTypeId(), attributeType.Description));
                newRec.Added = true;
                this.mAttributeDataTypes.Add(newRec);
                this.IsModified = true;
            }

            /// <summary>
            ///     Remove a record from the attribute data types collection.
            /// </summary>
            /// <param name="attributeTypeId">
            ///     ID of the record to delete.
            /// </param>
            public void DeleteAttributeDataType(uint attributeTypeId)
            {
                for (int index = 0; index < this.mAttributeDataTypes.Count; ++index)
                {
                    if (this.mAttributeDataTypes[index].Data.AttributeTypeId == attributeTypeId)
                    {
                        mAttributeDataTypes[index].Deleted = true;
                        this.IsModified = true;
                        return;
                    }
                }
            }

            /// <summary>
            ///     Modify an attribute data type record.
            /// </summary>
            /// <param name="attributeType">
            ///     New values for the record.
            /// </param>
            public void ModifyAttributeDataType(AttributeTypeDTO attributeType)
            {
                for (int index = 0; index < this.mAttributeDataTypes.Count; ++index)
                {
                    if (this.mAttributeDataTypes[index].Data.AttributeTypeId == attributeType.AttributeTypeId)
                    {
                        mAttributeDataTypes[index].Data.Description = attributeType.Description;
                        mAttributeDataTypes[index].Modified = true;
                        this.IsModified = true;
                        return;
                    }
                }
            }

            /// <summary>
            ///     Gets the list of attribute data types
            /// </summary>
            /// <returns>
            ///     Returns a reference to the list of attribute data types
            /// </returns>
            public IEnumerable<DataStorageItem<AttributeTypeDTO>> GetAttributeDataTypesTracking()
            {
                return this.mAttributeDataTypes;
            }
        #endregion // Public methods

        #region Private helper methods
            /// <summary>
            ///     Helper method for the <see cref="Close"/> method to close the document.
            /// </summary>
            private void CloseDocument()
            {
                mHtmlElements.Clear();
                mDocumentPath = String.Empty;

                this.IsModified = false;
                this.mIsLoaded = false;
                if (this.DocumentOpen != null)
                {
                    this.DocumentOpen.Invoke(this, false);
                }
            }

            /// <summary>
            ///     Helper for the <see cref="Load"/> method.
            /// </summary>
            /// <param name="dbFilePath"></param>
            private void LoadDocument(String dbFilePath)
            {
                String connectionString = $"Data Source={dbFilePath}; Version=3;";
                using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                {
                    dbConnection.Open();
                    LoadMetaData(dbConnection);

                    switch (this.mDatabaseSchemaVersion)
                    {
                        case METADATA_ID_DATABASE_SCHEMA_VERSION_1:
                            ReadDatabaseSchemeV1(dbConnection);
                            break;

                        default:
                            throw new DataStoreException(String.Format(Resources.DataStoreException_UnknownDBSchemaVersion, this.mDatabaseSchemaVersion));
                    }
                }
            }

            /// <summary>
            ///     Reads data from a schema version 1 type database.
            /// </summary>
            /// <param name="dbConnection">
            ///     Open database connection.
            /// </param>
            private void ReadDatabaseSchemeV1(SQLiteConnection dbConnection)
            {
                SQLiteDataAccess_v1 dataAccess = new SQLiteDataAccess_v1(dbConnection);

                // Read attribute types from the database.
                this.mAttributeDataTypes.Clear();
                foreach (AttributeTypeDTO attributeType in dataAccess.FetchAttributeTypes())
                {
                    this.mAttributeDataTypes.Add(new DataStorageItem<AttributeTypeDTO>(attributeType));
                }
            }

            /// <summary>
            ///     Writes data toa a schema version 1 type database.
            /// </summary>
            /// <param name="dbConnection">
            ///     Open database connection.
            /// </param>
            private void StoreDatabaseSchemaV1(SQLiteConnection dbConnection)
            {
                SQLiteDataAccess_v1 dataAccess = new SQLiteDataAccess_v1(dbConnection);

                AddUpdateDeleteSorter<AttributeTypeDTO> sorter = new AddUpdateDeleteSorter<AttributeTypeDTO>(this.mAttributeDataTypes);
                dataAccess.AddUpdateDelete_AttributeTypes(sorter.AddedRecords, sorter.ModifiedRecords, sorter.DeletedRecords);

                foreach (var attributeTypeRec in this.mAttributeDataTypes)
                {
                }
            }

            /// <summary>
            ///     Helper for <see cref="LoadDocument"/> to fetch the meta data for the database.
            /// </summary>
            /// <param name="dbConnection">
            ///     Database connection object.
            /// </param>
            private void LoadMetaData(SQLiteConnection dbConnection)
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = SQLite_Select_MetaDataTable;
                    dbCommand.CommandType = CommandType.Text;
                    using (SQLiteDataReader reader = dbCommand.ExecuteReader())
                    {
                        int colIndexMetaDataId = reader.GetOrdinal("MetaDataId");
                        int colIndexCode = reader.GetOrdinal("Code");
                        int colIndexIntegralValue = reader.GetOrdinal("IntegralValue");
                        int colIndexStringValue = reader.GetOrdinal("StringValue");

                        while (reader.Read())
                        {
                            switch (reader.GetInt32(colIndexMetaDataId))
                            {
                                case METADATA_ID_DATABASE_SCHEMA_VERSION:
                                    this.mDatabaseSchemaVersion = reader.GetInt32(colIndexIntegralValue);
                                    break;
                            }
                        }
                    }
                }
            }

            /// <summary>
            ///     Helper for the <see cref="CreateNew"/> method.
            /// </summary>
            private void CreateDocument()
            {
                this.mIsLoaded = true;
                this.mDatabaseSchemaVersion = METADATA_ID_DATABASE_SCHEMA_VERSION;
                if (this.DocumentOpen != null)
                {
                    this.DocumentOpen.Invoke(this, true);
                }
            }

            /// <summary>
            ///     Update changes to the existing document.
            /// </summary>
            private void UpdateDocumentChanges()
            {
            }

            /// <summary>
            ///     Create a new database and store the data.
            /// </summary>
            /// <param name="documentFilePath">
            ///     Path to the output file.
            /// </param>
            private void SaveNewDocument(String documentFilePath)
            {
                SQLiteConnection.CreateFile(documentFilePath);
                String connectionString = $"Data Source={documentFilePath}; Version=3;";
                using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                {
                    dbConnection.Open();

                    // Create the MetaDataTable table
                    using (SQLiteCommand createCommand = dbConnection.CreateCommand())
                    {
                        createCommand.CommandText = SQLite_CreateMetaDataTable;
                        createCommand.CommandType = CommandType.Text;
                        createCommand.ExecuteNonQuery();
                    }

                    // Create the AttributeTypes table
                    using (SQLiteCommand createCommand = dbConnection.CreateCommand())
                    {
                        createCommand.CommandText = SQLite_CreateAttributeTypes;
                        createCommand.CommandType = CommandType.Text;
                        createCommand.ExecuteNonQuery();
                    }

                    // Store the database schema version number
                    using (SQLiteCommand createCommand = dbConnection.CreateCommand())
                    {
                        createCommand.CommandText = SQLite_Insert_MetaData_DatabaseSchemaVersion;
                        createCommand.CommandType = CommandType.Text;
                        createCommand.ExecuteNonQuery();
                    }
                }
            }

            /// <summary>
            ///     Get the next attribute data type ID.
            /// </summary>
            /// <returns>
            ///     Returns an ID to use for a new record.
            /// </returns>
            private uint GetNextAttributeDataTypeId()
            {
                uint nextID = 0;

                foreach (var attributeDataType in mAttributeDataTypes)
                {
                    if (attributeDataType.Data.AttributeTypeId > nextID)
                    {
                        nextID = attributeDataType.Data.AttributeTypeId;
                    }
                }

                return (nextID + 1);
            }
        #endregion // Private helper methods
    } // class DataStore
} // namespace HtmlElementsDBEditor
