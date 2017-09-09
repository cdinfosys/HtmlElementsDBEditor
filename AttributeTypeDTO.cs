using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElementsDB
{
    public class AttributeTypeDTO : ICloneable
    {
        #region Private data members
            private uint mAttributeTypeId;
            private String mDescription;
        #endregion Private data members

        #region Construction
            private AttributeTypeDTO()
            {
            }

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

        #region IClonable interface
            /// <summary>
            ///     Creates a clone of the object by copying the values of all the data members to the new object.
            /// </summary>
            /// <returns>
            ///     Returns a copy of the object.
            /// </returns>
            public Object Clone()
            {
                return new AttributeTypeDTO()
                {
                    mAttributeTypeId = this.mAttributeTypeId,
                    mDescription = this.mDescription
                };
            }
        #endregion IClonable interface
    } // class AttributeTypeDTO
} // namespace HtmlElementsDB
