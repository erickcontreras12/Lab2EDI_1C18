﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Lab2EDI_1C18.DBContext;
using TDA_NoLineales.Interfaces;
using Lab2EDI_1C18.Models;
using TDA_NoLineales.Clases;

namespace Lab2EDI_1C18.Controllers
{
    public class PaisController : Controller
    {
        
        DefaultConnection db = DefaultConnection.getInstance;

        // GET: Pais
        public ActionResult Index()
        {
            return View(db.listaPaises.ToList());
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
        public ActionResult Create([Bind(Include = "nombrePais,grupo")] Pais pais)
        {
            try
            {
                // TODO: Add insert logic here
                Nodo<Pais> nuevo = new Nodo<Pais>(pais, CompararPais);
                db.arbolPaises.Insertar(nuevo);
                db.listaPaises.Add(pais);
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pais/Delete/5
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

        public static int CompararPais(Pais actual, Pais nuevo)
        {
            return actual.nombrePais.CompareTo(nuevo.nombrePais);
        }
    }
}