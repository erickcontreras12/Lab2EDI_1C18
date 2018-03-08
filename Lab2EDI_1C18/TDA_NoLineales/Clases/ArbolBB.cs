using System;
using TDA_NoLineales.Interfaces;

namespace TDA_NoLineales.Clases
{
    public class ArbolBB<T, K> : IArbolBB<T, K>
    {


        public Nodo<T> raiz { get; set; }
        CompararNodoDlg<K> _fnCompararLave;
        ObtenerKey<T, K> _fnObtenerLlave;
        private int size;
        private int cont;
        public ArbolBB()
        {
            raiz = null;           
            size = 0;
            cont = 0;
        }

        public void EnOrden(RecorridoDlg<T> recorrido)
        {
            RecorridoEnOrdenInterno(recorrido, raiz);
        }

        public bool ValidacionArbolDegenerado(Nodo<T> actual)
        {
            cont = 0;
            ValidarDerecha(actual);
            if (actual.derecho == null)
            {
                ValidarIzquierda(actual);
            }
            else
            {
                ValidarIzquierda(actual.izquierdo);
            }

            if (cont == size)
            {
                return true;
            }
            return false;
        }

        public void ValidarDerecha(Nodo<T> actual)
        {
            if (actual != null)
            {
                while (actual.derecho != null)
                {
                    if (actual.factor == 1)
                    {
                        cont++;
                    }
                    actual = actual.derecho;
                    if (actual.izquierdo != null)
                    {
                        ValidarIzquierda(actual);
                    }
                }
            }
        }

        public void ValidarIzquierda(Nodo<T> actual)
        {
            if (actual != null)
            {
                while (actual.izquierdo != null)
                {
                    if (actual.factor == 1)
                    {
                        cont++;
                    }
                    actual = actual.izquierdo;
                    if (actual.derecho != null)
                    {
                        ValidarDerecha(actual);
                    }
                }
            }
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

        public void Eliminar(K _key)
        {
          
        }

        public CompararNodoDlg<K> FuncionCompararLlave
        {
            get
            {
                return _fnCompararLave;
            }
            set
            {
                _fnCompararLave = value;
            }
        }

        public ObtenerKey<T, K> FuncionObtenerLlave
        {
            get
            {
                return _fnObtenerLlave;
            }
            set
            {
                _fnObtenerLlave = value;
            }
        }

        public void Eliminar2(K llave)
        {
            if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
            
                throw new Exception("No se han inicializado las funciones para operar la estructura");
              
            if (Equals(llave, default(K)))
                throw new ArgumentNullException("La llave enviada no es valida");

            if (raiz == null)
                throw new Exception("El arbol se encuentra vacio");
            else //Si el árbol no está vacio
            {
                Nodo<T> siguiente = raiz;
                Nodo<T> padre = null;
                bool EsHijoIzquierdo = false;
                bool encontrado = false;

                while (!encontrado)
                {
                    K llaveSiguiente = this.FuncionObtenerLlave(siguiente.valor);

                    // > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
                    int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

                    if (comparacion == 0)
                    {

                        if ((siguiente.derecho == null) && (siguiente.izquierdo == null)) //Si es una hoja
                        {
                            T miDato = siguiente.valor;
                            if ((padre != null))
                            {
                                if (EsHijoIzquierdo)
                                    padre.izquierdo = null;
                                else
                                    padre.derecho = null;
                            }
                            else //Si padre es null entonces es la raiz
                            {
                                raiz = null;
                            }
                            return;
                          //  return miDato;
                        }
                        else
                        {
                            if (siguiente.derecho == null) //Si solo tiene rama izquierda
                            {
                                T miDato = siguiente.valor;
                                if ((padre != null))
                                {
                                    if (EsHijoIzquierdo)
                                        padre.izquierdo = siguiente.izquierdo;
                                    else
                                        padre.derecho = siguiente.derecho;
                                }
                                else
                                {
                                    raiz = siguiente.izquierdo as Nodo<T>;
                                }
                                return;
                             //   return miDato;
                            }
                            else if (siguiente.izquierdo == null)  //Si solo tiene rama derecha
                            {
                                T miDato = siguiente.valor;
                                if ((padre != null))
                                {
                                    if (EsHijoIzquierdo)
                                        padre.izquierdo = siguiente.derecho;
                                    else
                                        padre.derecho = siguiente.derecho;
                                }
                                else
                                {
                                    raiz = siguiente.derecho as Nodo<T>;
                                }
                                return;
                          //      return miDato;
                            }
                            else  //Tiene ambas ramas el que lo sustituirá será el mas izquierdo de los derechos
                            {
                                Nodo<T> aEliminar = siguiente;
                                siguiente = siguiente.derecho as Nodo<T>;
                                int cont = 0;
                                while (siguiente.izquierdo != null)
                                {
                                    padre = siguiente;
                                    siguiente = siguiente.izquierdo as Nodo<T>;
                                    cont++;
                                }

                                if (cont > 0)
                                {
                                    if (padre != null)
                                    {
                                        T miDato = aEliminar.valor;
                                        aEliminar.valor = siguiente.valor;
                                        padre.izquierdo = null;
                                        return;
                                //        return miDato;
                                    }

                                }
                                else
                                {
                                    siguiente.izquierdo = aEliminar.izquierdo;

                                    if (padre != null)
                                    {
                                        if (EsHijoIzquierdo)
                                            padre.izquierdo = aEliminar.derecho;
                                        else
                                            padre.derecho = aEliminar.derecho;
                                    }
                                    else //Es la raiz
                                    {
                                        if (EsHijoIzquierdo)
                                            raiz = aEliminar.derecho as Nodo<T>;
                                        else
                                            raiz = aEliminar.derecho as Nodo<T>;
                                    }
                                    return;

                             //       return aEliminar.valor;
                                }

                            }
                        }
                    }
                    else
                    {
                        if (comparacion > 0)
                        {
                            if (siguiente.derecho == null)
                            {
                                return;
                                //   return default(T);
                            }
                            else
                            {
                                padre = siguiente;
                                EsHijoIzquierdo = false;
                                siguiente = siguiente.derecho as Nodo<T>;
                            }

                        }
                        else //menor que 0
                        {
                            if (siguiente.izquierdo == null)
                            {
                                return;
                               // return default(T);
                            }
                            else
                            {
                                padre = siguiente;
                                EsHijoIzquierdo = true;
                                siguiente = siguiente.izquierdo as Nodo<T>;
                            }
                        }
                    }//Fin del if comparaci{on

                } //Fin del ciclo

            }//Fin del if que verifica que no exista ningún dato.
            return;
       //   return default(T);
        }
    }


   }

