using Lab2EDI_1C18.DBContext;
using Lab2EDI_1C18.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(db.listaCadenas.ToList());
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cadena/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

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
    }
}
