namespace AssistantControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Asistente para la interacción con elementos de la clase System.Web.UI.WebControls.ListControl.
    /// </summary>
    public class ListControlAssistant
    {      
        /// <summary>
        /// Crea arreglo de elementos para la posterior carga de un ListControl.
        /// </summary>
        /// <param name="items">Cadena de caracteres contenedora de los elementos, separados por un delimitador, a cargar.</param>
        /// <param name="delimiter">Delimitador por el cual se encuentran separados los elementos a cargar.</param>
        /// <example> El siguiente ejemplo muestra la carga de los elementos de un DropDownList.
        ///         <code>
        ///            DropDownList teamsList = new DropDownList();      
        ///            string teams = "Team1;Team2;Team3";
        ///            char delimiter = ';';
        ///            teamsList.DataSource = AssistantControls.ListControlAssistant.LoadListItems(teams, delimiter);
        ///            teamsList.DataTextField = "Text";
        ///            teamsList.DataValueField = "Value";
        ///            teamsList.DataBind();
        ///         </code>
        /// </example>
        /// <returns>Retorna los elementos para la carga de un ListControl, en base a los valores que 
        ///          se encuentren, separados por el deliminador(deliminer), en la cadena de caracteres items.
        ///          Retornará un arreglo vacío en caso de que items no contenga elementos o ocurra algún tipo de excepción.
        /// </returns>
        public static List<ListItem> LoadListItems(string items, char delimiter)
        {
            List<ListItem> lItems = new List<ListItem>();
            try
            {              
                string[] itemsArr = items.Split(delimiter);
                foreach (string item in itemsArr)
                {
                    ListItem newListItem = new ListItem();
                    newListItem.Text = newListItem.Value = item;
                    lItems.Add(newListItem);
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se ha podido realizar la operación: " + ex.Message);             
            }

            return lItems;
        }
       
        /// <summary>
        /// Crea arreglo de elementos para la posterior carga de un ListControl.
        /// </summary>
        /// <param name="items">Cadena de caracteres contenedora de los elementos, separados por un delimitador y a su vez, a cargar.</param>
        /// <param name="delimiterOne">Delimitador por el cual se encuentran separados los elementos a cargar.</param>
        /// <param name="delimiterTwo">Delimitador por el cual se encuentra dividido cada elemento(text_delimitador_value).</param>
        /// <example> El siguiente ejemplo muestra la carga de los elementos de un DropDownList.
        ///         <code>
        ///            DropDownList teamsList = new DropDownList();
        ///            string teams = "All Teams|-1;Team1|001;Team2|002;Team3|003";
        ///            char delimiterOne = ';';
        ///            char delimiterTwo = '|';
        ///            teamsList.DataSource = AssistantControls.ListControlAssistant.LoadListItems(teams, delimiterOne, delimiterTwo);
        ///            teamsList.DataTextField = "Text";
        ///            teamsList.DataValueField = "Value";
        ///            teamsList.DataBind();
        ///         </code>
        /// </example>
        /// <returns>Retorna los elementos para la carga de un ListControl, en base a los valores que 
        ///          se encuentren, separados por el deliminador uno(deliminerOne), en la cadena de caracteres items. 
        ///          A su vez, cada uno de estos valores deberán estar divididos en dos partes por el delimitador dos(delimiterTwo),
        ///          la primera parte será asignada al campo text del LisItem correspondiente, miesntras que la segunda será 
        ///          asignada al campo value.
        ///          Retornará un arreglo vacío en caso de que items no contenga elementos o ocurra algún tipo de excepción.
        /// </returns>
        public static List<ListItem> LoadListItems(string items, char delimiterOne, char delimiterTwo)
        {
            List<ListItem> lItems = new List<ListItem>();
            try
            {
                string[] itemsArr = items.Split(delimiterOne);
                foreach (string item in itemsArr)
                {
                    string[] subItemsArr = item.Split(delimiterTwo);
                    ListItem newListItem = new ListItem();
                    newListItem.Text = subItemsArr[0];
                    newListItem.Value = subItemsArr[1];
                    lItems.Add(newListItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se ha podido realizar la operación: " + ex.Message);              
            }

            return lItems;
        }

        /// <summary>
        /// Crea un archivo Excel en base a todos los elementos del ListControl enviado, en la ruta solicitada.
        /// </summary>
        /// <param name="listControl">ListControl desde donde se van a obtener los datos a guardar en el archivo Excel.</param>
        /// <param name="excelFileName">Ruta destino del Excel generado.</param>
        /// <example> El siguiente ejemplo guarda en C:\\Sample.xlsx la información del DataSet ds(Todas sus tablas).
        ///         <code>
        ///            ListControl listControl = CreateSampleData();
        ///            CreateExcelFile.CreateExcelFile(listControl, "C:\\Sample.xlsx");
        ///         </code>
        /// </example>
        public bool CreateExcelFile(ListControl listControl, HttpResponse Response)
        {          
           return false;     
        }
    }
}
