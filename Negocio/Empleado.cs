using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public List<object> Empleados { get; set; }
        public Negocio.Puesto puesto { get; set; }
        public Negocio.Departamento departamento { get; set; }

        public static Dictionary<string, object> GetAll()
        {
            Empleado empleado1 = new Empleado();

            Dictionary<string, object> resultado = new Dictionary<string, object> { { "Excepcion", "" }, { "Resultado", false }, { "Empleado", null } };
            try
            {
                using (AccesoDatos.KPalomaresEstructuraEntities context = new AccesoDatos.KPalomaresEstructuraEntities())
                {
                    var registros = context.EmpleadoGetAll().ToList();
                    if (registros != null)
                    {
                        empleado1.Empleados = new List<object>();
                        foreach (var registro in registros)
                        {
                            Empleado empleado = new Empleado();
                            empleado.EmpleadoID = registro.EmpleadoID;
                            empleado.Nombre = registro.Nombre;
                            empleado.puesto = new Puesto();
                            empleado.puesto.PuestoDescripcion = registro.PuestoDescripcion;
                            empleado.departamento = new Departamento();
                            empleado.departamento.DepartamentoDescripcion = registro.DepartamentoDescripcion;

                            empleado1.Empleados.Add(empleado);

                        }
                        resultado["Resultado"] = true;
                        resultado["Empleado"] = empleado1;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado["Resultado"] = false;
                resultado["Excepcion"] = ex.Message;

            }
            return resultado;
        }

        public static Dictionary<string, object> Delete(int EmpleadoID)
        {
            Dictionary<string, object> result = new Dictionary<string, object> { { "Empleado", EmpleadoID }, { "Resultado", false }, { "Exepcion", "" } };
            {
                try
                {
                    using (SqlConnection context = new SqlConnection(AccesoDatos.Conexion.Get()))
                    {
                        var sentencia = "EmpleadoDelete";

                        SqlCommand command = new SqlCommand(sentencia, context);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] parametros = new SqlParameter[1];

                        parametros[0] = new SqlParameter("@EmpleadoID", SqlDbType.Int);
                        parametros[0].Value = EmpleadoID;

                        command.Parameters.AddRange(parametros);

                        command.Connection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        int filasAfectadas = command.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            result["Resultado"] = true;
                        }
                        else
                        {
                            result["Resultado"] = false;

                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            return result;

        }
    }
}
