using System;

namespace HtmlElementsDB
{
    /// <summary>
    ///     DTO for entries from the HtmlElement table.
    /// </summary>
    public class HtmlElementDTO : ICloneable
    {
        #region Private data members
            private uint mHtmlElementId;
            private String mElementTag;
            private uint mHtmlElementTypeId;
        #endregion // Private data members

        #region Construction
            private HtmlElementDTO()
            {
            }

            public HtmlElementDTO
            (
                uint htmlElementId,
                String elementTag = "",
                uint htmlElementTypeId = 0
            )
            {
                this.mHtmlElementId = htmlElementId;
            }
        #endregion // Construction

        #region Public properties
            public uint HtmlElementId => this.mHtmlElementId;

            public String ElementTag
            {
                get { return this.mElementTag; }
                set { this.mElementTag = value; }
            }

            public UInt32 HtmlElementTypeId
            {
                get { return mHtmlElementTypeId; }
                set { this.mHtmlElementTypeId = value; }
            }
        #endregion Public properties

        #region ICloneable
            public Object Clone()
            {
                return new HtmlElementDTO()
                {
                    mHtmlElementId = this.mHtmlElementId,
                    mElementTag = this.mElementTag,
                    mHtmlElementTypeId = this.mHtmlElementTypeId
                };
            }
        #endregion ICloneable

    } // class HtmlElementDTO
} // namespace HtmlElementsDBEditor
