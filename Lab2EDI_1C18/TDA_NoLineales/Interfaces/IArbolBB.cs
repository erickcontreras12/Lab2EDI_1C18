using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA_NoLineales.Clases;
namespace TDA_NoLineales.Interfaces
{
    interface IArbolBB<T, K>
    {
        void Insertar(Nodo<T> _nuevo);

        void Eliminar(K _key);

        Nodo<T> ObtenerRaiz();

        void EnOrden(RecorridoDlg<T> _recorrido);

        void PreOrden(RecorridoDlg<T> _recorrido);

        void PostOrden(RecorridoDlg<T> _recorrido);
    }
}
