namespace AssistantControls.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Interface para cumplimiento de exportación de controles a diferentes formatos binarios.
    /// </summary>
    interface IExportableControl
    {
        /// <summary>
        /// cumplimiento de exportación a formato Xls de un CompositeDataBoundControl.
        /// </summary>
        bool CreateExcelFile(CompositeDataBoundControl dataBound, string filename, HttpResponse Response);
    }
}
