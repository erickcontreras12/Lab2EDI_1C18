using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2EDI_1C18.Models;
using TDA_NoLineales.Clases;
namespace Lab2EDI_1C18.DBContext
{
    public class ConnectionJson
    {
        private static volatile ConnectionJson Instance;
        private static object syncRoot = new Object();

        public ArbolBB<CargaPais,string> PaisesCargados = new ArbolBB<CargaPais, string>();
        public ArbolBB<int,int> EnterosCargados = new ArbolBB<int,int>();
        public List<CargaPais> PaisesEnOrden = new List<CargaPais>();
        public List<CargaPais> PaisesPostOrden = new List<CargaPais>();
        public List<CargaPais> PaisesPreOrden = new List<CargaPais>();
        public List<int> aux1 = new List<int>();

        public ArbolBB<Entero,int> EnterosCargados1 = new ArbolBB<Entero,int>();
        public List<Entero> EnterosEnOrden = new List<Entero>();
        public List<Entero> EnterosPostOrden = new List<Entero>();
        public List<Entero> EnterosPreOrden = new List<Entero>();

        public int IDActual { get; set; }

        

        private ConnectionJson()
        {
            IDActual = 0;
        }

        public static ConnectionJson getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new ConnectionJson();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}