namespace AssistantControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AssistantControls.Utils;
    using System.Web;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Asistente para la interacción con elementos de la clase System.Data.DataSet.
    /// </summary>
    public class DataSetComponentAssistant : MarshalByValueComponentAssistant
    {

        /// <summary>
        /// Crea DataSet con la información recibida de la ejecución de la query solicitada.
        /// </summary>
        /// <param name="connectionString">Cadena que especifica la información para conectarse a una base de datos determinada.</param>
        /// <param name="queryString">Query SQL SERVER.</param>
        /// <example> El siguiente ejemplo carga un GridView con la salida de la ejecución de la query solicitada.
        ///         <code>
        ///            string connectionString = "Data Source=localhost;Initial Catalog=MyDB;user id=user;password=pass;";
        ///            
        ///            String queryString = "SELECT p.id, gp.team_name FROM teams ";
        ///                                   
        ///            DataSet content = AssistantControls.DataSetComponentAssistant.GetData(connectionString, queryString);
        ///            
        ///            GridView listTeams = new GridView();
        ///            listTeams.DataSource = content;
        ///            listTeams.DataBind();
        ///         </code>
        /// </example>
        /// <returns>Retorna un DataSet cargado con la query recibida por parametro.
        ///          Retornará un DataSet vacío en caso de que ocurra algún tipo de excepción.
        /// </returns>
        public static DataSet GetData(string connectionString, String queryString)
        {
            DataSet ds = new DataSet();       

            SqlConnection connection = new SqlConnection();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand();

            try
            {
                // Conexion a la base de datos.
                connection.ConnectionString = connectionString;
                connection.Open();

                // Carga de la query recibida por parametro.
                command.CommandText = queryString;
                command.Connection = connection;
                adapter.SelectCommand = command;

                // Ejecución de la query recibida por parametro y carga DataSet.
                adapter.Fill(ds);

                return ds;
            }
            catch (SqlException ex)
            {
                PrintDbException(ex);

                // En caso de que ocurra un error, se retorna un DataSet vacio.
                return new DataSet();
            }
            finally
                    {
                      // Finalizamos las instancias, con sus métodos correspondientes, de los elementos utilizados.   
                        connection.Close();
                        ds.Dispose();             
                    }
        }
      
        /// <summary>
        /// Crea DataSet con la información recibida de la ejecución del Stored Procedure solicitado.
        /// </summary>
        /// <param name="connectionString">Cadena que especifica la información para conectarse a una base de datos determinada.</param>
        /// <param name="storedProcedureName">Nombre de un storedProcedure presente en la base de datos indicada en el parámetro connectionString.</param>
        /// <param name="parameters">Parámetros de Stored Procedure, HashTable con key como nombre del parámetro en el Stored Procedure y valor del mismo.</param>
        /// <example> El siguiente ejemplo carga un GridView con la salida del Stored Procedure solicitado.
        ///         <code>
        ///            string connectionString = "Data Source=localhost;Initial Catalog=MyDB;user id=user;password=pass;";
        ///            
        ///            string storedProcedureName = "dbo.mySP";
        ///            
        ///            Hashtable parameters = new Hashtable();
        ///
        ///            parameters.Add("from", "2014-01-01 00:00:00");
        ///            parameters.Add("to", "2015-01-01 00:00:00");
        ///            parameters.Add("Team", "Team1");
        ///                         
        ///            DataSet content = AssistantControls.DataSetComponentAssistant.GetData(connectionString, storedProcedureName, parameters);
        ///            
        ///            GridView listTeams = new GridView();
        ///            listTeams.DataSource = content;
        ///            listTeams.DataBind();
        ///         </code>
        /// </example>
        /// <returns>Retorna un DataSet cargado con la información recibida de la ejecución del Stored Procedure solicitado, 
        ///          con los parámetros recibidos.
        ///          Retornará un DataSet vacío en caso de que ocurra algún tipo de excepción.
        /// </returns>
        public static DataSet GetData(string connectionString, string storedProcedureName, Hashtable parameters)
        { 
            DataSet ds = new DataSet();

            SqlConnection connection = new SqlConnection();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand();
          
            try
            {
                // Conexion a la base de datos.
                connection.ConnectionString = connectionString;
                connection.Open();


                // Se instancia un comando asociado al nombre del stored procedure.
                command.CommandText = storedProcedureName;
                command.Connection = connection;             

                // Se cargan los parámetros al comando.
                foreach (DictionaryEntry parameter in parameters)
                {
                    command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                }

                // Se indica como stored procedure al comando.
                command.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand = command;              

                // Se carga ds con la ejecución del stored procedure.
                adapter.Fill(ds);

                return ds;
            }
            catch (SqlException ex)
            {
                PrintDbException(ex);
                
                // En caso de que ocurra un error, se retorna un DataSet vacio.
                return new DataSet();
            }
            finally
            {
                // Finalizamos las instancias, con sus métodos correspondientes, de los elementos utilizados.   
                connection.Close();
                ds.Dispose();
            }          
        }

        private static void PrintDbException(SqlException ex)
        {
            StringBuilder errorMessages = new StringBuilder();

            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages.Append("Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "Error Number: " + ex.Errors[i].Number + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n");
            }

            Console.WriteLine(errorMessages.ToString());
        }

        /// <summary>
        /// Crea un archivo Excel en base a todos los DataTable del DataSet enviado, en la ruta solicitada.
        /// </summary>
        /// <param name="ds">DataSet desde donde se van a obtener los datos a guardar en el archivo Excel.</param>
        /// <param name="excelFileName">Ruta destino del Excel generado.</param>
        /// <example> El siguiente ejemplo guarda en C:\\Sample.xlsx la información del DataSet ds(Todas sus tablas).
        ///         <code>
        ///            DataSet ds = CreateSampleData();
        ///            CreateExcelFile.CreateExcelFile(ds, "C:\\Sample.xlsx");
        ///         </code>
        /// </example>
        public override bool CreateExcelFile(DataSet ds, string filename, HttpResponse Response)
        {
            GridView gv = new GridView();
            gv.DataSource = ds;
            gv.DataBind();

            GridViewControlAssistant gVCA = new GridViewControlAssistant();

            return gVCA.CreateExcelFile(gv, filename, Response);
        }

        /// <summary>
        /// Crea un archivo Excel en base a todos del DataTable enviado, en la ruta solicitada.
        /// </summary>
        /// <param name="dt">DataTable desde donde se van a obtener los datos a guardar en el archivo Excel.</param>
        /// <param name="excelFileName">Ruta destino del Excel generado.</param>
        /// <example> El siguiente ejemplo guarda en C:\\Sample.xlsx la información de la primer tabla de un DataSet.
        ///         <code>
        ///            DataTable dt = ds.Tables[0].Copy(); // Importante no hacer DataTable dt = ds.Tables[0]
        ///            DataSetComponentAssistant assistant = new DataSetComponentAssistant();
        ///            assistant.CreateExcelFile(dt, "C:\\SVN\\XMLXDHOYTS\\Sample.xlsx");
        ///         </code>
        /// </example>
        public override bool CreateExcelFile(DataTable dt, string filename, HttpResponse Response)
        {
            return false;
        }
    }
}
