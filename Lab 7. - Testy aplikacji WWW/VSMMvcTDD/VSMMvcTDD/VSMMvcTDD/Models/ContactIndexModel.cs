using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using Grid.Mvc.Ajax.GridExtensions;
using VSMMvcTDD.GridExtensions;

namespace VSMMvcTDD.Models
{
    public class ContactIndexModel
    {
        public AjaxGrid<ContactViewModel> Contacts { get; set; } 
    }
}