using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Lab2EDI_1C18.DBContext;
using Newtonsoft.Json;
using TDA_NoLineales.Clases;
using Lab2EDI_1C18.Models;
using Lab2EDI_1C18.DBContext;
using Lab2EDI_1C18.Models;
using System.Net;

namespace Lab2EDI_1C18.Controllers
{
    public class CargaEnteroController : Controller
    {
        ConnectionJson db = ConnectionJson.getInstance;
        // GET: CargaEntero
        public ActionResult IndexIn()
        {
            return View(db.EnterosEnOrden.ToList());
        }
        public ActionResult IndexPost()
        {
            return View(db.EnterosPostOrden.ToList());
        }
        public ActionResult IndexPre()
        {
            return View(db.EnterosPreOrden.ToList());
        }

        // GET: CargaEntero/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CargaEntero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargaEntero/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {

                string filepath = string.Empty;
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepath);

                string csvData = System.IO.File.ReadAllText(filepath);

                try
                {
                    db.EnterosCargados.raiz = JsonConvert.DeserializeObject<Nodo<int>>(csvData);
                    ViewBag.Message = "Archivo Cargado de manera exitosa";
                    db.EnterosCargados.PreOrden(RecorrerEnteroAux);
                                       
                    foreach (var item in db.aux1)
                    {
                        Entero a = new Entero();
                        a.Numero = item;
                        Nodo<Entero> x = new Nodo<Entero>(a, CompararEntero);
                        db.EnterosCargados1.Insertar(x);                        
                    }
                    db.EnterosCargados1.FuncionObtenerLlave = ObtenerClave;
                    db.EnterosCargados1.FuncionCompararLlave = CompInt;
                    db.EnterosCargados1.PreOrden(RecorrerEnteroIn);
                    db.EnterosCargados1.PostOrden(RecorrerEnteroPos);
                    db.EnterosCargados1.PreOrden(RecorrerEnteroPre);

                    bool tipoArbol = db.EnterosCargados1.ValidacionArbolDegenerado(db.EnterosCargados1.raiz);
                    if (tipoArbol == true)
                    {
                        ViewBag.Tipo = "Arbol equilibrado";
                    }
                    else
                    {
                        ViewBag.Tipo = "Arbol Degenerado";
                    }

                }
                catch (Exception e)
                {
                    ViewBag.Message = "Dato erroneo.";
                }

            }

            return View();
        }

        private int ObtenerClave(Entero dato)
        {
            return dato.Numero;
        }

        // GET: CargaEntero/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CargaEntero/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CargaEntero/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Entero cg = db.EnterosEnOrden.Find(x => x.Numero == id);

            if (cg == null)
            {
                return HttpNotFound();
            }

            return View(cg);
        }

        // POST: CargaEntero/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                Entero cg = db.EnterosEnOrden.Find(x => x.Numero == id);
                db.EnterosCargados1.Eliminar2(cg.Numero);
                db.EnterosEnOrden.Clear();
                db.EnterosPostOrden.Clear();
                db.EnterosPreOrden.Clear();

                db.EnterosCargados1.EnOrden(RecorrerEnteroIn);
                db.EnterosCargados1.PostOrden(RecorrerEnteroPos);
                db.EnterosCargados1.PreOrden(RecorrerEnteroPre);

                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }
        public void RecorrerEnteroAux(Nodo<int> actual)
        {
            db.aux1.Add(actual.valor);
        }
        public void RecorrerEnteroIn(Nodo<Entero> actual)
        {
            db.EnterosEnOrden.Add(actual.valor);
        }
        public void RecorrerEnteroPos(Nodo<Entero> actual)
        {
            db.EnterosPostOrden.Add(actual.valor);
        }
        public void RecorrerEnteroPre(Nodo<Entero> actual)
        {
            db.EnterosPreOrden.Add(actual.valor);
        }
             
        public static int CompararEntero(Entero actual, Entero nuevo)
        {
            return actual.Numero.CompareTo(nuevo.Numero);
        }

        public static int CompInt(int actual, int nuevo)
        {
            return actual.CompareTo(nuevo);
        }

    }
}
