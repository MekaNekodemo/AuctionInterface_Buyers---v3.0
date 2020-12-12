using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionInterface_Sellers.CustomHtmlHelpers
{
        public static class DatePickerHelper
        {
            public static MvcHtmlString DatePicker(this HtmlHelper helper,
                                                   string name)
            {
                TagBuilder tag = new TagBuilder("input");
                tag.Attributes.Add("name", name);
                tag.Attributes.Add("id", name);
                tag.Attributes.Add("value", "");
                tag.Attributes.Add("type", "date");

                MvcHtmlString html = new MvcHtmlString(
                                tag.ToString(TagRenderMode.SelfClosing));
                return html;
            }




        }
    
}