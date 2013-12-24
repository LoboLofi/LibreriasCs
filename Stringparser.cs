using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace StringParser {
   /// <summary>
   /// 
   /// </summary>
   static class Stringparser {

      static public string DecimalToMoney( decimal cadena ) {
         string aux = cadena.ToString().Replace( ',', '.' );
         char[] separadores = { ',', '.' };
         int separate = aux.LastIndexOfAny( separadores ) - 3;

         while( separate > 0 ) {
            aux = aux.Insert( separate, "," );
            separate -= 3;
         }
         if( aux.Length > 1 && aux[1] == ',' && aux[0] == '-' ) {
            return "-" + aux.Substring( 2 );
         }
         return aux;
      }
      static public decimal stringToDecimal( string cadena ) {
         if( String.IsNullOrEmpty( cadena ) )
            return 0.0M;
         cadena          = cadena.Replace(',', '.');
         decimal nvo     = 0.0M;
         decimal divisor = 10.0M;
         int ptnDecimal = cadena.LastIndexOf( '.' );
         ptnDecimal = ptnDecimal == -1 ? cadena.Length : ptnDecimal;
         foreach( char a in cadena.Substring( 0, ptnDecimal ) ) {
            if( ( 48 <= a && a < 58 ) ) {
               nvo = 10 * nvo + ( (short)a ) - 48;
            }
         }
         foreach( char a in cadena.Substring( ptnDecimal ) ) {
            if( ( 48 <= a && a < 58 ) ) {
               nvo += ( ( (short)a ) - 48 ) / divisor;
               divisor = divisor * 10;
            }
         }
         if( cadena[0] == '-' )
            nvo *= -1;
         return nvo;
      }
      static public string StringToMoney( string cadena ) {
         if( String.IsNullOrEmpty( cadena )  )
            return "0.0";
         cadena = cadena.Replace( ',', '.' );
         char[] separadores = { '.' };

         int i = cadena.LastIndexOfAny( separadores );
         int separate = i == -1 ? cadena.Length - 3 : i - 3;

         while( separate > 0 ) {
            cadena = cadena.Insert( separate, "," );
            separate -= 3;
         }
         if( cadena[1] == ',' && cadena[0] == '-' ) {
            return "-" + cadena.Substring( 2 );
         }
         return cadena;
      }
      static public string SubstractStringToString( string numero1, string numero2 ) {
         return ( stringToDecimal( numero1 ) - stringToDecimal( numero2 ) ).ToString().Replace( ',', '.' );
      }
      static public string SubstractStringToDecimal( string numero1, decimal numero2 ) {
         return ( stringToDecimal( numero1 ) - numero2 ).ToString().Replace( ',', '.' );
      }
      static public string DecimalMaxValue = Decimal.MaxValue.ToString();
      static public string QuitarCaracteres( string cadena ) {
         List<char> nvo = new List<char>();
         foreach( char a in cadena ) {
            if( ( 48 <= a && a < 58 ) || a == '.' ) {
               nvo.Add( a );
            }
         }
         return new string( nvo.ToArray() );
      }

      static public string ValidaAlfaNumerico( string cadena ) {
         List<char> nvo = new List<char>();
         foreach( char a in cadena ) {
            if( Char.IsLetterOrDigit( a ) || a == '.' ) {
               nvo.Add( a );
            }
         }
         return new string( nvo.ToArray() );
      }


     
   }
}