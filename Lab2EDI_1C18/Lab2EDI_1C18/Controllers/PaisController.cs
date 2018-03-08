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
    public class PaisController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;

        // GET: Pais
        public ActionResult Index()
        {
            if (db.arbolPaises.raiz!=null)
            {

                if (db.arbolPaises.ValidacionArbolDegenerado(db.arbolPaises.ObtenerRaiz())) 
                {
                    ViewBag.Tipo = "SI es un arbol degenerado";
                }
                else
                {
                    ViewBag.Tipo = "NO es un arbol degenerado";
                }
            }
           
            return View(db.listaPaises.ToList());
        }
        public ActionResult IndexIn()
        {
            return View(db.listaPaisesEnOrden.ToList());
        }
        public ActionResult IndexPost()
        {
            return View(db.listaPaisesPostOrden.ToList());
        }
        public ActionResult IndexPre()
        {
            return View(db.listaPaisesPreOrden.ToList());
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "nombre,Grupo")] Pais pais)
        {

            try
            {
                // TODO: Add insert logic here
                Nodo<Pais> nuevo = new Nodo<Pais>(pais, CompararPais);
                db.arbolPaises.Insertar(nuevo);
                db.listaPaises.Add(pais);

            

                //Recorridos

                db.listaPaisesEnOrden = new List<Pais>();
                db.listaPaisesPreOrden = new List<Pais>();
                db.listaPaisesPostOrden = new List<Pais>();

                db.arbolPaises.EnOrden(RecorrerPaisIn);
                db.arbolPaises.PreOrden(RecorrerPaisPre);
                db.arbolPaises.PostOrden(RecorrerPaisPost);

                db.arbolPaises.FuncionObtenerLlave = ObtenerClave;
                db.arbolPaises.FuncionCompararLlave = CompararCadenas;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pais/Edit/5
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

        // GET: Pais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pais cg = db.listaPaises.Find(x => x.nombre == id);

            if (cg == null)
            {
                return HttpNotFound();
            }

            return View(cg);
        }

        // POST: Pais/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                Pais cg = db.listaPaises.Find(x => x.nombre == id);
                db.arbolPaises.Eliminar2(cg.nombre);
                db.listaPaises.Remove(cg);
                db.listaPaisesEnOrden.Clear();
                db.listaPaisesPostOrden.Clear();
                db.listaPaisesPreOrden.Clear();

                db.arbolPaises.EnOrden(RecorrerPaisIn);
                db.arbolPaises.PostOrden(RecorrerPaisPost);
                db.arbolPaises.PreOrden(RecorrerPaisPre);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public static int CompararPais(Pais actual, Pais nuevo)
        {
            return actual.nombre.CompareTo(nuevo.nombre);
        }


        public void RecorrerPaisIn(Nodo<Pais> actual)
        {
            db.listaPaisesEnOrden.Add(actual.valor);
        }
        public void RecorrerPaisPost(Nodo<Pais> actual)
        {
            db.listaPaisesPostOrden.Add(actual.valor);
        }
        public void RecorrerPaisPre(Nodo<Pais> actual)
        {
            db.listaPaisesPreOrden.Add(actual.valor);
        }

        public static string ObtenerClave(Pais dato)
        {
            return dato.nombre;
        }
        public static int CompararCadenas(string actual, string nuevo)
        {
            return actual.CompareTo(nuevo);
        }
    }
}
