namespace AssistantControls.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Interface para cumplimiento de exportación de componente a diferentes formatos binarios.
    /// </summary>
    interface IExportableComponent
    {      
         /// <summary>
         /// cumplimiento de exportación a formato Xls de un DataSet.
         /// </summary>
         bool CreateExcelFile(DataSet ds, string filename, HttpResponse Response);

         /// <summary>
         /// cumplimiento de exportación a formato Xls de un DataTable.
         /// </summary>
         bool CreateExcelFile(DataTable dt, string filename, HttpResponse Response);
    }
}
