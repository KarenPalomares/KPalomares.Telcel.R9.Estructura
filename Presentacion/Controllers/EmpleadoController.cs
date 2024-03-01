using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
       [HttpGet]
        public ActionResult GetAll()
        {
            Dictionary<string, object> result = Negocio.Empleado.GetAll();
            bool resultado = (bool)result["Resultado"];
            if (resultado == true)
            {
                Negocio.Empleado empleado = (Negocio.Empleado)result["Empleado"];

           
                return View(empleado);
            }
            else
            {
                string exepcion = (string)result["Excepcion"];


                return View();

            }
            
        }
        [HttpGet]
        public ActionResult Delete(int EmpleadoID)
        {
            Dictionary<string, object> result = Negocio.Empleado.Delete(EmpleadoID);
            return View();
        }
    }
}