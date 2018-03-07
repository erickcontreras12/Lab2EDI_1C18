﻿using Lab2EDI_1C18.DBContext;
using Lab2EDI_1C18.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDA_NoLineales.Clases;

namespace Lab2EDI_1C18.Controllers
{
    public class EnteroController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Entero
        public ActionResult Index()
        {
            return View(db.listaEnteros.ToList());
        }

        public ActionResult IndexIn()
        {
            return View(db.listaEnterosEnOrden.ToList());
        }

        public ActionResult IndexPre()
        {
            return View(db.listaEnterosPreOrden.ToList());
        }

        public ActionResult IndexPost()
        {
            return View(db.listaEnterosPostOrden.ToList());
        }

        // GET: Entero/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Entero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entero/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "valor")] Entero num)
        {
            try
            {
                // TODO: Add insert logic here
                Nodo<Entero> nuevo = new Nodo<Entero>(num, CompararEntero);
                db.arbolEnteros.Insertar(nuevo);
                db.listaEnteros.Add(num);

                //Recorridos
                db.listaEnterosEnOrden = new List<Entero>();
                db.listaEnterosPreOrden = new List<Entero>();
                db.listaEnterosPostOrden = new List<Entero>();

                db.arbolEnteros.EnOrden(RecorrerEnteroIn);
                db.arbolEnteros.PreOrden(RecorrerEnteroPre);
                db.arbolEnteros.PostOrden(RecorrerEnteroPost);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Arbol()
        {
            try
            {
                if (db.arbolEnteros.ValidacionArbolDegenerado(db.arbolEnteros.ObtenerRaiz()))
                {
                    ViewBag.Message = "SI es un arbol degenerado";
                }
                else
                {
                    ViewBag.Message = "NO es un arbol degenerado";
                }
                
            }
            catch (Exception)
            {

                ViewBag.Message = "Error";
            }

            return View();
        }

        // GET: Entero/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Entero/Edit/5
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

        // GET: Entero/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entero/Delete/5
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

        public static int CompararEntero(Entero actual, Entero nuevo)
        {
            return actual.valor.CompareTo(nuevo.valor);
        }

        public void RecorrerEnteroIn(Nodo<Entero> actual)
        {
            db.listaEnterosEnOrden.Add(actual.valor);
        }

        public void RecorrerEnteroPre(Nodo<Entero> actual)
        {
            db.listaEnterosPreOrden.Add(actual.valor);
        }

        public void RecorrerEnteroPost(Nodo<Entero> actual)
        {
            db.listaEnterosPostOrden.Add(actual.valor);
        }
    }
}
