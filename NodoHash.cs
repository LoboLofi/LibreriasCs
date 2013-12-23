using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Collections.Trees {
   class NodoHash<T> {
         Dictionary<char,NodoHash<T>>  hijos;
         List<char>                    keys;
         int                           count;
         int                           nivel;
         T                             presente;

         public       NodoHash( T cadenaId, int nivelAnt ) {
            this.hijos = new Dictionary<char, NodoHash<T>>();
            this.keys = new List<char>();
            this.count = 1;
            this.presente = cadenaId;
            this.nivel = nivelAnt + 1;
         }
         public   T              getValor( string Cadena ){
            if( this.presente.ToString() == Cadena )
               return presente;
            if( this.hijos.ContainsKey( Cadena[this.nivel] ) )
               return this.hijos[Cadena[this.nivel]].getValor( Cadena );
            else
               return default(T);
         }
         public   List<string>   getCadena( int valores ) {
            List<string> rtn = new List<string>();
            if( valores <= 0 )
               return rtn;
            rtn.Add( "-1|" + this.presente + "||-1" );
            int indice = 0;
            while( rtn.Count < valores && this.keys.Count > indice ) {
               rtn.AddRange( this.hijos[this.keys[indice++]].getCadena( valores - 1 ) );
            }
            return rtn;
         }
         public   List<string>   getCadenasCon( string subCadena, int valores ) {
            return this.filtroCadenas( subCadena.ToUpper(), valores );
         }
         public   void           AgregarCadena( T cadenaId ) {
            this.AgregarNodo( cadenaId );
         }
         private  void           AgregarNodo( T cadenaId ) {
            T aux;
            if( this.presente.ToString().Length >= cadenaId.ToString().Length ) {
               aux = this.presente;
               this.presente = cadenaId;
               cadenaId = aux;
            }
            int indice = cadenaId.ToString().Length > this.nivel ? this.nivel : cadenaId.ToString().Length - 1;
            if( !this.hijos.ContainsKey( cadenaId.ToString()[indice] ) ) {
               this.hijos.Add( cadenaId.ToString()[indice], new NodoHash<T>( cadenaId, this.nivel ) );
               this.keys.Add( cadenaId.ToString()[indice] );
            } else {
               this.hijos[cadenaId.ToString()[indice]].AgregarNodo( cadenaId );
            }
            this.keys.Sort();
         }
         private  List<string>   filtroCadenas( string subCadena, int valores ) {
            if( subCadena.Length == this.nivel ) {
               return this.getCadena( valores );
            }
            if( this.hijos.ContainsKey( subCadena[this.nivel] ) )
               return this.hijos[subCadena[this.nivel]].filtroCadenas( subCadena, valores );
            else
               return new List<string>();
         } 
   }
   class ParLlave {
      public   string   nombre{get;set;}
      public   int      arrayValor{get;set;}
      
      public ParLlave( string _nombre, int _val ) {
         this.nombre          = _nombre;
         this.arrayValor      = _val;
      }
      public override string ToString() {
         return nombre;
      }

   }
   class Sctruct {
      string  idProducto{get;set;}
      string  nombre{get;set;}
      decimal precioUnitario{get;set;}
      
      public Sctruct( string _nombre, decimal _val) {
         this.nombre          = _nombre;
         this.precioUnitario  = _val;
      }
      public override string ToString() {
         return nombre;
      }
   }
}
