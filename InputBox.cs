using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HtmlElementsDBEditor
{
    public partial class InputBox : Form
    {
        /// <summary>
        ///     Delegate to validate the input data before the dialog is closed.
        /// </summary>
        /// <param name="validationData">
        ///     Data to validate.
        /// </param>
        /// <paaram name="additionalData">
        ///     Data that was passed to the constructor.
        /// </paaram>
        /// <returns>
        ///     Must return <c>true</c> if the data is valid or <c>false</c> if the value is invalid.
        /// </returns>
        public delegate Boolean ValidateInputValueProc(String validationData, Object additionalData);

        #region Private data members
            /// <summary>
            ///     Delegate to validate user input.
            /// </summary>
            private ValidateInputValueProc mValidationCallback;

            /// <summary>
            ///     Additional data passed by the user.
            /// </summary>
            private Object mAdditionalData;
        #endregion Private data members

        #region Construction
            /// <summary>
            ///     Constructor.
            /// </summary>
            /// <param name="initialValue">
            ///     Initial value to appear in the text box.
            /// </param>
            /// <param name="dialogCaption">
            ///     Dialog caption.
            /// </param>
            public InputBox(String initialValue = "", String dialogCaption = null, Object additionalData = null)
            {
                InitializeComponent();

                if (!String.IsNullOrWhiteSpace(dialogCaption))
                {
                    this.Text = dialogCaption.Trim();
                }

                this.valueTextBox.Text = initialValue;

                this.mAdditionalData = additionalData;
            }

            /// <summary>
            ///     Construct an object with a validation delegate.
            /// </summary>
            /// 
            /// <param name="initialValue">
            ///     Initial value to appear in the text box.
            /// </param>
            /// <param name="dialogCaption">
            ///     Dialog caption.
            /// </param>
            public InputBox(ValidateInputValueProc validationCallbackProc, String initialValue = "", String dialogCaption = null, Object additionalData = null)
            {
                InitializeComponent();

                this.mValidationCallback = validationCallbackProc;

                if (!String.IsNullOrWhiteSpace(dialogCaption))
                {
                    this.Text = dialogCaption.Trim();
                }

                this.valueTextBox.Text = initialValue;

                this.mAdditionalData = additionalData;
            }
        #endregion // Construction

        #region Public properties
            /// <summary>
            ///     Gets the value entered by the user.
            /// </summary>
            public String Value => this.valueTextBox.Text;
        #endregion Public properties

        #region Event handlers
            /// <summary>
            ///     Event handler for the OK button.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnOkButtonClicked(Object sender, EventArgs e)
            {
                if (this.mValidationCallback != null)
                {
                    if (this.mValidationCallback.Invoke(this.valueTextBox.Text, mAdditionalData))
                    {
                        this.Close();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                    }
                }
            }
        #endregion Event handlers
    } // class InputBox
} // namespace HtmlElementsDBEditor
