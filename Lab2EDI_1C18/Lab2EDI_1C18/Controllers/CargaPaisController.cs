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
namespace Lab2EDI_1C18.Controllers
{
    public class CargaPaisController : Controller
    {

        ConnectionJson db = ConnectionJson.getInstance;
        // GET: CargaPais
        public ActionResult IndexIn()
        {
            return View(db.PaisesEnOrden.ToList());
        }
        public ActionResult IndexPost()
        {
            return View(db.PaisesPostOrden.ToList());
        }
        public ActionResult IndexPre()
        {
            return View(db.PaisesPreOrden.ToList());
        }

        // GET: CargaPais/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CargaPais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargaPais/Create
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
                    db.PaisesCargados.raiz = JsonConvert.DeserializeObject<Nodo<CargaPais>>(csvData);
                    ViewBag.Message = "Archivo Cargado de manera exitosa";
                    db.PaisesCargados.EnOrden(RecorrerPaisIn);
                    db.PaisesCargados.PostOrden(RecorrerPaisPos);
                    db.PaisesCargados.PreOrden(RecorrerPaisPre);

                }
               catch (Exception e){
                            ViewBag.Message = "Dato erroneo.";
                        }
                    
                }
               
            return View();
        }

        // GET: CargaPais/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CargaPais/Edit/5
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

        // GET: CargaPais/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CargaPais/Delete/5
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

            public void RecorrerPaisIn(Nodo<CargaPais> actual)
            {
                db.PaisesEnOrden.Add(actual.valor);
            }
            public void RecorrerPaisPos(Nodo<CargaPais> actual)
            {
                db.PaisesPostOrden.Add(actual.valor);
            }
            public void RecorrerPaisPre(Nodo<CargaPais> actual)
            {
                db.PaisesPreOrden.Add(actual.valor);
            }
    }
}
