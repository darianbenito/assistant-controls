namespace AssistantControls
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Windows.Forms;
    using System.Web;

    /// <summary>
    /// Asistente para la interacción con elementos de la clase System.Web.UI.WebControls.GridView.
    /// </summary>
    public class GridViewControlAssistant : CompositeDataBoundControlAssistant
    {

        public GridViewControlAssistant() { }

        public override bool CreateExcelFile(CompositeDataBoundControl dataBound, string filename, HttpResponse Response)
        {
            try
            {
                GridView gv = (GridView)dataBound;
                this.RemoveHeaderFormat(gv);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page page = new Page();
                HtmlForm form = new HtmlForm();
                gv.EnableViewState = false;
                page.EnableEventValidation = false;
                page.DesignerInitialize();
                page.Controls.Add(form);
                form.Controls.Add(gv);
                page.RenderControl(htw);

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                Response.Charset = "UTF-8";
                sb = sb.Replace("<caption>", string.Empty).Replace("Resultado", string.Empty).Replace("</caption>", string.Empty);
                Response.Write(sb.ToString());
                Response.End();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }                    
        }

        /*
         * Elimina las imágenes del HEADER para una exportación "limpia" a excel.
         * */
        private GridView RemoveHeaderFormat(GridView gridView)
        {
            try
            {
                // Iteramos las columnas del HEADER
                foreach (TableCell cell in gridView.HeaderRow.Cells)
                {
                    LinkButton lbSort = (LinkButton)cell.Controls[0];

                    // Se eliminan elementos HTML.
                    if (lbSort.Text.Split('<').Count() > 0)
                    {
                        lbSort.Text = lbSort.Text.Split('<')[0];
                    }

                    lbSort.Attributes.Remove("href");
                    lbSort.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                    lbSort.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";

                    if (lbSort.Enabled != false)
                    {
                        lbSort.Enabled = false;
                    }

                    if (lbSort.OnClientClick != null)
                    {
                        lbSort.OnClientClick = null;
                    }
                }
            }
            catch
            {
               return gridView;
            }

            return gridView;
        }
    }
}
