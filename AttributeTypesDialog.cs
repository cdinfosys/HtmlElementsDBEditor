using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HtmlElementsDB;
using HtmlElementsDBEditor.Properties;

namespace HtmlElementsDBEditor
{
    /// <summary>
    ///     Backing class for the Attribute Types dialog box
    /// </summary>
    public sealed partial class AttributeTypesDialog : Form
    {
        #region Private data members
            /// <summary>
            ///     Stores attribute data types.
            /// </summary>
            private List<DataStorageItem<AttributeTypeDTO>> mAttributeDataTypes = new List<DataStorageItem<AttributeTypeDTO>>();
        #endregion // Private data members

        #region Construction
            /// <summary>
            ///     Default constructor.
            /// </summary>
            public AttributeTypesDialog()
            {
                InitializeComponent();
            }
        #endregion Construction

        #region Helper methods
            /// <summary>
            ///     Validates data from the input box.
            /// </summary>
            /// <param name="inputValue">
            ///     Data to validate
            /// </param>
            /// <param name="restoredObject">
            ///     Reference to a <c>Boolean</c> flag that is set if a deleted item was restored.
            /// </param>
            /// <returns>
            ///     Returns <c>true</c> if the input value is legal or <c>false</c> if the data is not valid.
            /// </returns>
            private Boolean InputBoxValidationProc(String inputValue, Object restoredObject)
            {
                String compareValue = inputValue.Trim();
                restoredObject = null;
                foreach (DataStorageItem<AttributeTypeDTO> rec in mAttributeDataTypes)
                {
                    if (String.Compare(rec.Data.Description, compareValue, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (!rec.Deleted)
                        {
                            MessageBox.Show
                            (
                                this,
                                Resources.DuplicateDescription,
                                Resources.ErrorCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return false;
                        }
                    }
                }

                return true;
            }

            /// <summary>
            ///     Applies data changes to the data source.
            /// </summary>
            private void ApplyChanges()
            {
                this.okButton.Focus();
                this.applyButton.Enabled = false;

                AddUpdateDeleteSorter<AttributeTypeDTO> sorter = new AddUpdateDeleteSorter<AttributeTypeDTO>(this.mAttributeDataTypes);
                foreach (AttributeTypeDTO addedRec in sorter.AddedRecords)
                {
                    DataStore.Instance.AddAttributeDataType(addedRec);
                }

                foreach (AttributeTypeDTO deletedRec in sorter.DeletedRecords)
                {
                    DataStore.Instance.DeleteAttributeDataType(deletedRec.AttributeTypeId);
                }

                foreach (AttributeTypeDTO modifiedRec in sorter.ModifiedRecords)
                {
                    DataStore.Instance.ModifyAttributeDataType(modifiedRec);
                }

                // Remove deleted items from the list and clear flags.
                for (int index = (this.mAttributeDataTypes.Count - 1); index >= 0; --index)
                {
                    if (this.mAttributeDataTypes[index].Deleted)
                    {
                        this.mAttributeDataTypes.RemoveAt(index);
                    }
                    else
                    {
                        this.mAttributeDataTypes[index].ClearTrackingFlags();
                    }
                }

            }
        #endregion Helper methods

        #region Base class method overrides
            /// <summary>
            ///     Called after the form was loaded.
            /// </summary>
            /// <param name="eventArgs"></param>
            protected override void OnLoad(EventArgs eventArgs)
            {
                base.OnLoad(eventArgs);

                this.mAttributeDataTypes = new List<DataStorageItem<AttributeTypeDTO>>();

                // Create a local copy of the stored attribute data types
                this.mAttributeDataTypes.Clear();
                mAttributeDataTypes.AddRange(DataStore.Instance.CreateAttributeDataTypesCopy());

                foreach (DataStorageItem<AttributeTypeDTO> attr in this.mAttributeDataTypes)
                {
                    if (!attr.Deleted)
                    {
                        ListViewItem newListViewItem = new ListViewItem(attr.Data.Description);
                        newListViewItem.Tag = attr;
                        attributeTypesListView.Items.Add(newListViewItem);
                    }
                }
            }
        #endregion // Base class method overrides

        #region Event handlers
            /// <summary>
            ///     Event handler for the apply button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnApplyButtonClicked(Object sender, EventArgs e)
            {
                this.ApplyChanges();
            }

            private void OnOkButtonClicked(Object sender, EventArgs e)
            {
                this.ApplyChanges();
            }

            /// <summary>
            ///     Event handler for the Add button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnAddButtonClicked(Object sender, EventArgs e)
            {
                // The contained value of this object will be set to an instance of a DataStorageItem if a previously deleted item was restored.
                ObjectContainer<DataStorageItem<AttributeTypeDTO>> restoredRec = new ObjectContainer<DataStorageItem<AttributeTypeDTO>>();

                InputBox dlg = new InputBox(InputBoxValidationProc, null, null, restoredRec);

                switch (dlg.ShowDialog(this))
                {
                    case DialogResult.OK:
                        {
                            if (!String.IsNullOrWhiteSpace(dlg.Value))
                            {
                                AttributeTypeDTO newRec = new AttributeTypeDTO(0, dlg.Value.Trim());
                                DataStorageItem<AttributeTypeDTO> addRec = new DataStorageItem<AttributeTypeDTO>(newRec)
                                {
                                    Added = true
                                };
                                this.mAttributeDataTypes.Add(addRec);

                                // Show in the list view
                                ListViewItem newListViewItem = new ListViewItem(addRec.Data.Description);
                                newListViewItem.Tag = addRec;
                                attributeTypesListView.Items.Add(newListViewItem);
                                attributeTypesListView.SelectedItems.Clear();
                                newListViewItem.Selected = true;

                                this.applyButton.Enabled = true;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            ///     Event handler for the Delete button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnDeleteButtonClicked(Object sender, EventArgs e)
            {
                // List for holding items to remove from the list view
                List<ListViewItem> removeItems = new List<ListViewItem>();

                // Mark the records as having been deleted.
                foreach (ListViewItem selectedRec in this.attributeTypesListView.SelectedItems)
                {
                    (selectedRec.Tag as DataStorageItem<AttributeTypeDTO>).Deleted = true;
                    removeItems.Add(selectedRec);
                }

                // Remove items from the list view
                foreach (ListViewItem removeItem in removeItems)
                {
                    this.attributeTypesListView.Items.Remove(removeItem);
                }

                this.applyButton.Enabled = true;
            }

            /// <summary>
            ///     Event handler for the Edit button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnEditButtonClicked(Object sender, EventArgs e)
            {
                if (this.attributeTypesListView.SelectedItems.Count < 1)
                {
                    return;
                }

                // Get a reference to the first selected item.
                DataStorageItem<AttributeTypeDTO> selectedEditItem = this.attributeTypesListView.SelectedItems[0].Tag as DataStorageItem<AttributeTypeDTO>;
                InputBox dlg = new InputBox(InputBoxValidationProc, selectedEditItem.Data.Description);

                switch (dlg.ShowDialog(this))
                {
                    case DialogResult.OK:
                        {
                            if (!String.IsNullOrWhiteSpace(dlg.Value))
                            {
                                // Update the data item
                                selectedEditItem.Data.Description = dlg.Value.Trim();
                                selectedEditItem.Modified = true;

                                // Update the item in the list view
                                this.attributeTypesListView.SelectedItems[0].Text = selectedEditItem.Data.Description;

                                // Enable the apply button.
                                this.applyButton.Enabled = true;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            ///     Event handler for the ItemSelectionChanged event of the livt view.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="eventArgs"></param>
            private void OnListViewItemSelectionChanged(Object sender, ListViewItemSelectionChangedEventArgs eventArgs)
            {
                // Check if anything in the list view is currently selected.
                Boolean stuffSelected = (attributeTypesListView.SelectedItems.Count > 0);

                editButton.Enabled = (attributeTypesListView.SelectedItems.Count == 1);
                deleteButton.Enabled = stuffSelected;
            }
        #endregion Event handlers
    } // class AttributeTypesDialog
} // namespace HtmlElementsDBEditor
