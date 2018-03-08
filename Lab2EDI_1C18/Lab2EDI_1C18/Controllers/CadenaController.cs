using Lab2EDI_1C18.DBContext;
using Lab2EDI_1C18.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TDA_NoLineales.Clases;

namespace Lab2EDI_1C18.Controllers
{
    public class CadenaController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;

        // GET: Cadena
        public ActionResult Index()
        {
            if (db.arbolCadenas.raiz != null)
            {

                if (db.arbolCadenas.ValidacionArbolDegenerado(db.arbolCadenas.ObtenerRaiz()))
                {
                    ViewBag.Tipo = "SI es un arbol degenerado";
                }
                else
                {
                    ViewBag.Tipo = "NO es un arbol degenerado";
                }
            }
            return View(db.listaCadenas.ToList());
        }
        public ActionResult IndexIn()
        {
            return View(db.listaCadenasEnOrden.ToList());
        }
        public ActionResult IndexPost()
        {
            return View(db.listaCadenasPostOrden.ToList());
        }
        public ActionResult IndexPre()
        {
            return View(db.listaCadenasPreOrden.ToList());
        }
        // GET: Cadena/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cadena/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cadena/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Texto")] Cadena text)
        {
            try
            {
                // TODO: Add insert logic here
                Nodo<Cadena> nuevo = new Nodo<Cadena>(text , CompararCadena);
                db.arbolCadenas.Insertar(nuevo);
                db.listaCadenas.Add(text);

                //Recorridos

                db.listaCadenasEnOrden = new List<Cadena>();
                db.listaCadenasPreOrden = new List<Cadena>();
                db.listaCadenasPostOrden = new List<Cadena>();

                db.arbolCadenas.EnOrden(RecorrerCadenaIn);
                db.arbolCadenas.PreOrden(RecorrerCadenaPre);
                db.arbolCadenas.PostOrden(RecorrerCadenaPost);

                db.arbolCadenas.FuncionObtenerLlave = ObtenerClave;
                db.arbolCadenas.FuncionCompararLlave = CompString;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public static string ObtenerClave(Cadena dato)
        {
            return dato.Texto;
        }

        // GET: Cadena/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cadena/Edit/5
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

        // GET: Cadena/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cadena cg = db.listaCadenas.Find(x => x.Texto == id);

            if (cg == null)
            {
                return HttpNotFound();
            }

            return View(cg);
        }

        // POST: Cadena/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                Cadena cg = db.listaCadenas.Find(x => x.Texto == id);
                db.arbolCadenas.Eliminar2(cg.Texto);
                db.listaCadenas.Remove(cg);
                db.listaCadenasEnOrden.Clear();
                db.listaCadenasPostOrden.Clear();
                db.listaCadenasPreOrden.Clear();

                db.arbolCadenas.EnOrden(RecorrerCadenaIn);
                db.arbolCadenas.PostOrden(RecorrerCadenaPost);
                db.arbolCadenas.PreOrden(RecorrerCadenaPre);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public static int CompararCadena(Cadena actual, Cadena nuevo)
        {
            return actual.Texto.CompareTo(nuevo.Texto);
        }

        public static int CompString(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }

        public void RecorrerCadenaIn(Nodo<Cadena> actual)
        {
            db.listaCadenasEnOrden.Add(actual.valor);
        }
        public void RecorrerCadenaPost(Nodo<Cadena> actual)
        {
            db.listaCadenasPostOrden.Add(actual.valor);
        }
        public void RecorrerCadenaPre(Nodo<Cadena> actual)
        {
            db.listaCadenasPreOrden.Add(actual.valor);
        }
    }
}
