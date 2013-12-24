using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Collections.Trees {
   class NodoHash<T> {
      Dictionary<char,NodoHash<T>>  childrens;
      List<char>                    keys;
      int                           count;
      int                           level;
      T                             valuePresent;
      
      public NodoHash( T elementT, int levelAnt ) {
         this.childrens = new Dictionary<char, NodoHash<T>>();
         this.keys = new List<char>();
         this.count = 1;
         this.valuePresent = elementT;
         this.level = levelAnt + 1;
      }
      public T getElement( string stringId ) {
         if( this.valuePresent.ToString() == stringId )
            return valuePresent;
         if( this.childrens.ContainsKey( stringId[this.level] ) )
            return this.childrens[stringId[this.level]].getElement( stringId );
         else
            return default( T );
      }
      public List<string> getString( int valores ) {
         List<string> rtn = new List<string>();
         if( valores <= 0 )
            return rtn;
         rtn.Add( this.valuePresent.ToString() );
            
         int index = 0;
         while( rtn.Count < valores && this.keys.Count > index ) {
            rtn.AddRange( this.childrens[this.keys[index++]].getString( valores - 1 ) );
         }
         return rtn;
      }
      public List<string> getStringWith( string filter, int valores ) {
         return this.filterStrings( filter.ToUpper(), valores );
      }
      public bool Exist( string elementTid ) {
         if( this.valuePresent.ToString() == elementTid )
            return true;
         if( this.childrens.ContainsKey( elementTid[this.level] ) )
            return this.childrens[elementTid[this.level]].Exist( elementTid );
         else
            return false;
      }
      public void AddElement( T elementT ) {
         this.AddNode( elementT );
      }
      private void AddNode( T elementT ) {
         T aux;
         if( this.valuePresent.ToString().Length >= elementT.ToString().Length ) {
            aux = this.valuePresent;
            this.valuePresent = elementT;
            elementT = aux;
         }
         int indice = elementT.ToString().Length > this.level ? this.level : elementT.ToString().Length - 1;
         if( !this.childrens.ContainsKey( elementT.ToString()[indice] ) ) {
            this.childrens.Add( elementT.ToString()[indice], new NodoHash<T>( elementT, this.level ) );
            this.keys.Add( elementT.ToString()[indice] );
         } else {
            this.childrens[elementT.ToString()[indice]].AddNode( elementT );
         }
         this.keys.Sort();
      }
      private List<string> filterStrings( string filter, int valores ) {
         if( filter.Length == this.level ) {
            return this.getString( valores );
         }
         if( this.childrens.ContainsKey( filter[this.level] ) )
            return this.childrens[filter[this.level]].filterStrings( filter, valores );
         else
            return new List<string>();
      }
   }
}
