using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElementsDB
{
    public class AttributeTypeDTO
    {
        #region Private data members
            private uint mAttributeTypeId;
            private String mDescription;
        #endregion Private data members

        #region Construction
            public AttributeTypeDTO
            (
                uint attributeTypeId,
                String description = ""
            )
            {
                this.mAttributeTypeId = attributeTypeId;
                this.mDescription = description;
            }
        #endregion // Construction

        #region Public properties
            public uint AttributeTypeId => this.mAttributeTypeId;

            public String Description
            {
                get
                {
                    return this.mDescription;
                }
                set
                {
                    this.mDescription = value;
                }
            }
        #endregion Public properties
    } // class AttributeTypeDTO
} // namespace HtmlElementsDB
