using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2EDI_1C18.Models;
using TDA_NoLineales.Clases;

namespace Lab2EDI_1C18.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public ArbolBB<Pais> arbolPaises = new ArbolBB<Pais>();
        public List<Pais> listaPaises = new List<Pais>();
        public ArbolBB<Cadena> arbolCadenas = new ArbolBB<Cadena>();
        public List<Cadena> listaCadenas = new List<Cadena>();
        public ArbolBB<Entero> arbolEnteros = new ArbolBB<Entero>();
        public List<Entero> listaEnteros = new List<Entero>();

        ////Listas para recorridos
        public List<Pais> listaPaisesEnOrden = new List<Pais>();
        public List<Pais> listaPaisesPreOrden = new List<Pais>();
        public List<Pais> listaPaisesPostOrden = new List<Pais>();

        public List<Cadena> listaCadenasEnOrden = new List<Cadena>();
        public List<Cadena> listaCadenasPreOrden = new List<Cadena>();
        public List<Cadena> listaCadenasPostOrden = new List<Cadena>();

        public List<Entero> listaEnterosEnOrden = new List<Entero>();
        public List<Entero> listaEnterosPreOrden = new List<Entero>();
        public List<Entero> listaEnterosPostOrden = new List<Entero>();

        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }

}
