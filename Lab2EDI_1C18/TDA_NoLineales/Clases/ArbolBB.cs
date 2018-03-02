using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA_NoLineales.Interfaces;

namespace TDA_NoLineales.Clases
{
    public class ArbolBB<T> : IArbolBB<T>
    {

        private Nodo<T> raiz;

        public ArbolBB()
        {
            raiz = null;
        }

        public void EnOrden(RecorridoDlg<T> recorrido)
        {
            RecorridoEnOrdenInterno(recorrido, raiz);
        }

        public void Insertar(Nodo<T> nuevo)
        {
            if (raiz == null)
            {
                raiz = nuevo;
            }
            else
            {
                InsercionInterna(raiz, nuevo);
            }
        }

        public Nodo<T> ObtenerRaiz()
        {
            return raiz;
        }

        public void PostOrden(RecorridoDlg<T> recorrido)
        {
            RecorridoPostOrdenInterno(recorrido, raiz);
        }

        public void PreOrden(RecorridoDlg<T> recorrido)
        {
            RecorridoPreOrdenInterno(recorrido, raiz);
        }


        private void InsercionInterna(Nodo<T> actual, Nodo<T> nuevo)
        {
            if (actual.CompareTo(nuevo.valor) < 0)
            {
                if (actual.derecho == null)
                {
                    actual.derecho = nuevo;
                }
                else
                {
                    InsercionInterna(actual.derecho, nuevo);
                }
            }
            else if (actual.CompareTo(nuevo.valor) > 0)
            {
                if (actual.izquierdo == null)
                {
                    actual.izquierdo = nuevo;
                }
                else
                {
                    InsercionInterna(actual.izquierdo, nuevo);
                }
            }
        } //Fin de inserción interna.

        private void RecorridoEnOrdenInterno(RecorridoDlg<T> recorrido, Nodo<T> actual)
        {
            if (actual != null)
            {
                RecorridoEnOrdenInterno(recorrido, actual.izquierdo);

                recorrido(actual);

                RecorridoEnOrdenInterno(recorrido, actual.derecho);
            }
        }

        private void RecorridoPostOrdenInterno(RecorridoDlg<T> recorrido, Nodo<T> actual)
        {
            if (actual != null)
            {
                RecorridoEnOrdenInterno(recorrido, actual.izquierdo);

                RecorridoEnOrdenInterno(recorrido, actual.derecho);

                recorrido(actual);
            }
        }

        private void RecorridoPreOrdenInterno(RecorridoDlg<T> recorrido, Nodo<T> actual)
        {
            if (actual != null)
            {
                recorrido(actual);

                RecorridoEnOrdenInterno(recorrido, actual.izquierdo);

                RecorridoEnOrdenInterno(recorrido, actual.derecho);
            }
        }

        public void Eliminar(T _key)
        {
            throw new NotImplementedException();
        }

       

    }
}
