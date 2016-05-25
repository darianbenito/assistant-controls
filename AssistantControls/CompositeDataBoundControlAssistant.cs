namespace AssistantControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.UI.WebControls;
    using AssistantControls.Utils;
    using System.Web;
    using System.Web.UI;

    /// <summary>
    /// Asistente para la interacción con elementos de la clase System.Web.UI.WebControls.CompositeDataBoundControl.
    /// System.Object 
    ///     System.Web.UI.WebControls.CompositeDataBoundControl
    ///         System.Web.UI.WebControls.DetailsView
    ///         System.Web.UI.WebControls.FormView
    ///         System.Web.UI.WebControls.GridView(*)
    ///         
    /// (*) Implementado.
    /// </summary>
    public abstract class CompositeDataBoundControlAssistant : IExportableControl
    {
        public abstract bool CreateExcelFile(CompositeDataBoundControl dataBound, string filename, HttpResponse Response);
       
    }
}
