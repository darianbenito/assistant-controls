namespace AssistantControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AssistantControls.Utils;
    using System.Web;

    /// <summary>
    /// Asistente para la interacción con elementos de la clase System.ComponentModel.MarshalByValueComponent.
    /// System.Object 
    ///     System.ComponentModel.MarshalByValueComponent
    ///         System.Data.DataColumn
    ///         System.Data.DataSet(*)
    ///         System.Data.DataTable
    ///         System.Data.DataView
    ///         System.Data.DataViewManager
    ///         System.Web.Services.WebService
    /// 
    /// (*) Implementado.
    /// </summary>
    public abstract class MarshalByValueComponentAssistant : IExportableComponent
    {
        public abstract bool CreateExcelFile(DataSet ds, string filename, HttpResponse Response);

        public abstract bool CreateExcelFile(DataTable dt, string filename, HttpResponse Response);
    }
}
