using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HtmlElementsDB;
using HtmlElementsDBEditor.Properties;

namespace HtmlElementsDBEditor
{
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
            /// <returns>
            ///     Returns <c>true</c> if the input value is legal or <c>false</c> if the data is not valid.
            /// </returns>
            private Boolean InputBoxValidationProc(String inputValue)
            {
                String compareValue = inputValue.Trim();
                foreach (DataStorageItem<AttributeTypeDTO> rec in mAttributeDataTypes)
                {
                    if (String.Compare(rec.Data.Description, compareValue, StringComparison.OrdinalIgnoreCase) == 0)
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

                return true;
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
                this.mAttributeDataTypes = new List<DataStorageItem<AttributeTypeDTO>>(DataStore.Instance.GetAttributeDataTypesTracking());
            }
        #endregion // Base class method overrides

        #region Event handlers
            /// <summary>
            ///     Event handler for the Add button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnAddButtonClicked(Object sender, EventArgs e)
            {
                InputBox dlg = new InputBox(InputBoxValidationProc);

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
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        #endregion Event handlers
    }
}
