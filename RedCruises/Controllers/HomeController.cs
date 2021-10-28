using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using RedCruises.Models;

namespace RedCruises.Controllers
{
    public class HomeController : Controller
    {
        static string baseRed = " Server=LAPTOP-09OOM2I1\\MSSQLSERVER2;Database=RedCruises;Uid=sa;Pwd=Marce15599;";

        #region Iniciar vistas

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReservaCamarote()
        {
            return View();
        }

        public ActionResult CancelarViaje()
        {
            return View();
        }

        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult DWH()
        {
            return View();
        }

        public ActionResult InformacionNautica()
        {
            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("spInfoNautica", conn))
                {
                    InfoNautica objetos = null;
                    List<InfoNautica> listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new List<InfoNautica>();
                        while (reader.Read())
                        {

                            objetos = new InfoNautica();
                            objetos.ID = Int32.Parse(reader["ID del Tripulante"].ToString());
                            objetos.Horas = Int32.Parse(reader["Horas nauticas"].ToString());
                            objetos.Millas = Int32.Parse(reader["Millas nauticas"].ToString());

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();

                    return View("InformacionNautica", listaObjetos);
                }
            }
        }

        public ActionResult ReservasCola()
        {
            return View();
        }

        public ActionResult ReservasConfirmadas()
        {
            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("spReservasConfirmadas", conn))
                {
                    Confirmadas objetos = null;
                    List<Confirmadas> listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new List<Confirmadas>();
                        while (reader.Read())
                        {

                            objetos = new Confirmadas();
                            objetos.Cantidad = reader["Cantidad de reservas"].ToString();
                            objetos.Dia = reader["Dia"].ToString();

                            if (reader["Canal"].ToString() == "1")
                            {
                                objetos.Canal = "Telefono";
                            }
                            else if(reader["Canal"].ToString() == "2")
                            {
                                objetos.Canal = "Pagina";
                            }
                            else
                            {
                                objetos.Canal = "Oficinas";
                            }

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();

                    return View("ReservasConfirmadas", listaObjetos);
                }
            }
        }

        public ActionResult RutasLargas()
        {
            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("spViajesLargos", conn))
                {
                    ViajesLargos objetos = null;
                    List<ViajesLargos> listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new List<ViajesLargos>();
                        while (reader.Read())
                        {

                            objetos = new ViajesLargos();
                            objetos.IDViaje = reader["ID_Viaje"].ToString();
                            objetos.Origen = reader["PuertoOrigen"].ToString();
                            objetos.Destino = reader["PuertoDestino"].ToString();
                            objetos.Salida = reader["FechaSalida"].ToString();
                            objetos.Llegada = reader["FechaLlegada"].ToString();

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();

                    return View("RutasLargas", listaObjetos);
                }
            }
        }

        public ActionResult ViajesLlenos()
        {
            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("spViajesLlenos", conn))
                {
                    infoViajesLlenos objetos = null;
                    List<infoViajesLlenos> listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new List<infoViajesLlenos>();
                        while (reader.Read())
                        {

                            objetos = new infoViajesLlenos();
                            objetos.Origen = reader["Origen"].ToString();
                            objetos.Dia = reader["Dia"].ToString();
                            objetos.PasajerosConfirmados = reader["Pasajeros confirmados"].ToString();
                            objetos.CapacidadTotal = reader["Capacidad de pasajeros"].ToString();
                            objetos.Porcentaje = reader["% Ocupado"].ToString() + "%";

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();

                    return View("ViajesLlenos", listaObjetos);
                }
            }
        }

        #endregion

        #region Reservaciones
        [HttpPost]
        public ActionResult ReservaCamarote(FormCollection collection)
        {
            int idCanal = 0;
            string dia = collection["Salida"].Substring(0, 2);
            string mes = collection["Salida"].Substring(3, 2);
            string anio = collection["Salida"].Substring(6, 4);

            InfoReserva informacion = new InfoReserva();
            informacion.Nombre = collection["Nombre"];
            informacion.Apellido = collection["Apellido"];
            informacion.DPI = collection["DPI"];
            informacion.Camarote = Convert.ToInt32(collection["Camarote"]);
            informacion.Origen = collection["Origen"];
            DateTime dt = DateTime.ParseExact(collection["Salida"], "dd/MM/yyyy", null);
            System.Data.SqlTypes.SqlDateTime dtSql = System.Data.SqlTypes.SqlDateTime.Parse(dt.ToString("yyyy/MM/dd"));
            informacion.Confirmacion = Convert.ToDouble(collection["Confirmacion"]);
            informacion.Canal = collection["Canal"];

            if (informacion.Canal == "Teléfono")
            {
                idCanal = 1;
            }
            else if (informacion.Canal == "Página")
            {
                idCanal = 2;
            }
            else
            {
                idCanal = 3;
            }

            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("Proc_Reservar", conn))
                {
                    dynamic objetos = null;
                    JArray listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@NombreCliente", informacion.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoCliente", informacion.Apellido);
                    cmd.Parameters.AddWithValue("@dpi", informacion.DPI);
                    cmd.Parameters.AddWithValue("@NumCamarote", informacion.Camarote);
                    cmd.Parameters.AddWithValue("@Origen", informacion.Origen);
                    cmd.Parameters.AddWithValue("@FechaSalida", dtSql);
                    cmd.Parameters.AddWithValue("@tiempo", informacion.Confirmacion);
                    cmd.Parameters.AddWithValue("@CanalReserva", idCanal);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new JArray();
                        while (reader.Read())
                        {

                            objetos = new JObject();
                            objetos.cantidad = Int32.Parse(reader["Cantidad de reservas"].ToString());

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();

                    return View();
                }
            }
        }
        #endregion

        #region IngresoClientes

        [HttpPost]
        public ActionResult Clientes(FormCollection collection)
        {
            Cliente infoCliente = new Cliente();
            infoCliente.Nombre = collection["Nombre"];
            infoCliente.Apellido = collection["Apellido"];
            infoCliente.DPI = collection["DPI"];
            infoCliente.Edad = Convert.ToInt32(collection["Edad"]);
            infoCliente.Peso = Convert.ToDouble(collection["Peso"]);

            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("spIngresoCliente", conn))
                {
                    dynamic objetos = null;
                    JArray listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@DPI", infoCliente.DPI);
                    cmd.Parameters.AddWithValue("@Nombre", infoCliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", infoCliente.Apellido);
                    cmd.Parameters.AddWithValue("@Edad", infoCliente.Edad);
                    cmd.Parameters.AddWithValue("@Peso", infoCliente.Peso);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new JArray();
                        while (reader.Read())
                        {

                            objetos = new JObject();
                            objetos.cantidad = Int32.Parse(reader["Cantidad de reservas"].ToString());

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();
                    return View();
                }
            }
        }
        #endregion

        #region Cancelar Reserva
        [HttpPost]
        public ActionResult CancelarViaje(FormCollection collection)
        {
            CancelarViaje cancelar = new CancelarViaje();
            cancelar.ID = Convert.ToInt32(collection["ID"]);

            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CancelarViaje", conn))
                {
                    dynamic objetos = null;
                    JArray listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@idReserva", cancelar.ID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new JArray();
                        while (reader.Read())
                        {

                            objetos = new JObject();
                            objetos.cantidad = Int32.Parse(reader["Cantidad de reservas"].ToString());

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();
                    return View();

                    return View();
                }
            }
        }
        #endregion

        #region DWH
        [HttpPost]
        public ActionResult ReservasCola(FormCollection collection)
        {
            ReservasEnCola reservas = new ReservasEnCola();
            reservas.Mes = Convert.ToInt32(collection["Mes"]);
            reservas.Año = Convert.ToInt32(collection["Año"]);

            using (SqlConnection conn = new SqlConnection(baseRed))
            {
                using (SqlCommand cmd = new SqlCommand("spColaReservas", conn))
                {
                    Cantidad objetos = null;
                    List<Cantidad> listaObjetos = null;

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@mes", reservas.Mes);
                    cmd.Parameters.AddWithValue("@anio", reservas.Año);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        listaObjetos = new List<Cantidad>();
                        while (reader.Read())
                        {

                            objetos = new Cantidad();
                            objetos.CantidadCola = Int32.Parse(reader["Cantidad de reservas"].ToString());

                            listaObjetos.Add(objetos);
                        }
                    }
                    JArray vacio = new JArray();
                    return View("ReservasColaRes",listaObjetos);
                }
            }
        }
        #endregion
    }
}