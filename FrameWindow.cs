using System;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using HtmlElementsDBEditor.Properties;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Main window
    /// </summary>
    public sealed partial class FrameWindow : Form
    {
        #region Construction
            /// <summary>
            ///     Default constructor.
            /// </summary>
            public FrameWindow()
            {
                InitializeComponent();
                DataStore.Instance.DocumentOpen += OnDocumentOpenedClosed;
            }
        #endregion // Construction

        #region Base class method overrides.
            /// <summary>
            ///     Check if it is safe to close the program.
            /// </summary>
            /// <param name="eventArgs">
            ///     The <c>Cancel</c> property may be set to prevent the program from closing.
            /// </param>
            protected override void OnClosing(CancelEventArgs eventArgs)
            {
                base.OnClosing(eventArgs);
                if (!eventArgs.Cancel)
                {
                    if (!DataStore.Instance.Close())
                    {
                        eventArgs.Cancel = true;
                    }
                }
            }
        #endregion Base class method overrides.

        #region Event handlers
            /// <summary>
            ///     Event handler for the Exit item on the File menu.
            /// </summary>
            /// <param name="sender">Not used</param>
            /// <param name="eventArgs">Not used</param>
            private void OnExitClicked(Object sender, EventArgs eventArgs)
            {
                this.Close();
            }

            /// <summary>
            ///     Event handler for the 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnDocumentOpenedClosed(Object sender, Boolean isOpened)
            {
                // Enable or disable menus
                SetMenuItemsEnabledState();
            }

            /// <summary>
            ///     Event handler for the New menu item.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnNewClicked(Object sender, EventArgs e)
            {
                // Create a new document.
                DataStore.Instance.CreateNew();
            }

            /// <summary>
            ///     Event handler for the Close menu item.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnCloseClicked(Object sender, EventArgs e)
            {
                DataStore.Instance.Close();
            }

            /// <summary>
            ///     Event handler for the Save menu item
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnSaveClicked(Object sender, EventArgs e)
            {
                DataStore.Instance.Save();
            }

            /// <summary>
            ///     Event handler for the Save As menu item
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnSaveAsClicked(Object sender, EventArgs e)
            {
                try
                {
                    DataStore.Instance.SaveAs();
                }
                catch (SQLiteException ex)
                {
                    ShowExceptionMessage(ex);
                }
            }

            /// <summary>
            ///     Event handler for the Open menu item
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnOpenClicked(Object sender, EventArgs e)
            {
                try
                {
                    String initialDirectory;
                    if (String.IsNullOrWhiteSpace(DataStore.Instance.FilePath))
                    {
                        initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }
                    else
                    {
                        initialDirectory = Path.GetDirectoryName(Path.GetFullPath(DataStore.Instance.FilePath));
                    }

                    OpenFileDialog dlg = new OpenFileDialog()
                    {
                        AddExtension = true,
                        DefaultExt = "db",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Filter = "SQLite database (*.db)|*.db|All files (*.*)|*.*",
                        FilterIndex = 0,
                        InitialDirectory = initialDirectory,
                        RestoreDirectory = true,
                        ShowHelp = false,
                        SupportMultiDottedExtensions = true,
                        Multiselect = false,
                        ValidateNames = true,
                        ShowReadOnly = false,
                    };

                    switch (dlg.ShowDialog(this))
                    {
                        case DialogResult.OK:
                            DataStore.Instance.Load(dlg.FileName);
                            break;
                    }
                }
                catch (SQLiteException ex)
                {
                    ShowExceptionMessage(ex);
                }
            }

            private void OnAttributeTypesClicked(Object sender, EventArgs eventArgs)
            {
                AttributeTypesDialog dlg = new AttributeTypesDialog();
                dlg.ShowDialog(this);
            }

        #endregion // Event handlers

        #region Helper methods
            private void SetMenuItemsEnabledState()
            {
                bool enableDocumentMenus = DataStore.Instance.IsLoaded;

                // File menu
                this.closeMenuItem.Enabled = enableDocumentMenus;
                this.saveMenuItem.Enabled = enableDocumentMenus;
                this.saveAsMenuItem.Enabled = enableDocumentMenus;

                // Tools menu
                this.attributeTypesMenuItem.Enabled = enableDocumentMenus;
                this.cssPropertyTypesMenuItem.Enabled = enableDocumentMenus;
            }
        #endregion // Helper methods

        #region Public accessor methods.
            /// <summary>
            ///     Checks if unsaved data exists and asks the user if they want to save changes.
            /// </summary>
            /// <returns>
            ///     Returns <c>true</c> if the program can be closed or <c>false</c> if the program cannot be closed.
            /// </returns>
            public bool QueryCanClose()
            {
                switch
                (
                    MessageBox.Show
                    (
                        this,
                        Resources.UnsavedChangesMessage,
                        Resources.UnsavedChangesCaption,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1
                    )
                )
                {
                    case DialogResult.Yes:
                        // Save the document changes
                        DataStore.Instance.Save();
                        return true;

                    case DialogResult.No:
                        // Discard the document changes
                        return true;

                    case DialogResult.Cancel:
                    default:
                        // User wants to cancel closing the document
                        return false;
                }
            }

            /// <summary>
            ///     Shows a message box containing an exception message.
            /// </summary>
            /// <param name="ex">
            ///     Exception object.
            /// </param>
            private void ShowExceptionMessage(Exception ex)
            {
                MessageBox.Show
                (
                    this,
                    ex.Message,
                    ex.GetType().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            /// <summary>
            ///     Show the Save As dialog box to get the file name and path from the user.
            /// </summary>
            /// <param name="filePathOut">
            ///     Reference to a String object that receives the path to the file.
            /// </param>
            /// <param name="saveAs">
            ///     Shows "Save As" instead of "Save" in the caption.
            /// </param>
            /// <returns>
            ///     Returns <c>true</c> if the user selected a path or <c>false</c> if the user clicked Cancel.
            /// </returns>
            public bool GetFilePath(ref String filePathOut)
            {
                String initialDirectory;
                if (String.IsNullOrWhiteSpace(filePathOut))
                {
                    initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                else
                {
                    initialDirectory = Path.GetDirectoryName(Path.GetFullPath(filePathOut));
                }

                SaveFileDialog dlg = new SaveFileDialog()
                {
                    AddExtension = true,
                    DefaultExt = "db",
                    CheckFileExists = false,
                    CheckPathExists = true,
                    CreatePrompt = false,
                    FileName = filePathOut,
                    Filter = "SQLite database (*.db)|*.db|All files (*.*)|*.*",
                    FilterIndex = 0,
                    InitialDirectory = initialDirectory,
                    OverwritePrompt = true,
                    RestoreDirectory = true,
                    ShowHelp = false,
                    SupportMultiDottedExtensions = true,
                };

                switch (dlg.ShowDialog(this))
                {
                    case DialogResult.OK:
                        filePathOut = dlg.FileName;
                        return true;

                    default:
                        return false;
                }
            }
        #endregion Public accessor methods.
    } // class FrameWindow
} // namespace HtmlElementsDBEditor
