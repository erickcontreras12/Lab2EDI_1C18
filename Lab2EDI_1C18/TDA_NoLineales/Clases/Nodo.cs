using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA_NoLineales.Interfaces;

namespace TDA_NoLineales.Clases
{
    public class Nodo<T> : IComparable<T>
    {

        public T valor { get; set; }
        public Nodo<T> izquierdo { get; set; }
        public Nodo<T> derecho { get; set; }
        public int factor;
        private CompararNodoDlg<T> comparador;

        public Nodo(T _value, CompararNodoDlg<T> _comparador)
        {
            this.valor = _value;
            this.izquierdo = null;
            this.derecho = null;
            this.factor = 0;
            comparador = _comparador;
        }

        public int CompareTo(T other)
        {
            return comparador(this.valor, other);
        }
    }
}
