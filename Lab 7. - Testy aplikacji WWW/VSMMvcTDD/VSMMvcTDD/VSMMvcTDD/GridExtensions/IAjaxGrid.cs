using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grid.Mvc.Ajax.GridExtensions;

namespace VSMMvcTDD.GridExtensions
{
    public interface IAjaxGrid<TView> where TView : class
    {
        AjaxGrid<TView> GridData { get; set; }
        IAjaxGrid AjaxGrid { get; set; }
    }
}
