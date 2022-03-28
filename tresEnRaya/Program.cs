namespace Program
{
  class TresEnRayaMain
  {
    static void Main(string[] args)
    {
      string[,] tabla = new string[3, 3] {
                                          { " ", " ", " " },
                                          { " ", " ", " " },
                                          { " ", " ", " " }
                                        };

      bool jugando = true;

      string? lecturaUsuario = "";

      do
      {
        System.Console.WriteLine("Posiciones: \n 1 2 3 \n 4 5 6 \n 7 8 9");
        System.Console.Write("Introduce tu eleccion: ");
        lecturaUsuario = Console.ReadLine();

        if (string.IsNullOrEmpty(lecturaUsuario)) continue;
        if (lecturaUsuario == "q")
        {
          jugando = false;
          continue;
        }

        int usuarioEleccion = Convert.ToInt32(lecturaUsuario);

        int linea = DeterminaLinea(usuarioEleccion);

        bool valorIntroducido = IntroducirValor(tabla, linea, usuarioEleccion);

        ImprimirTabla(tabla);


      } while (jugando);
    }

    static bool IntroducirValor(string[,] tabla, int linea, int eleccion)
    {
      bool puedeIntroducir = ComprobacionCelda(tabla, eleccion, linea);

      int columna = DeterminarColumna(eleccion);

      if (puedeIntroducir)
      {
        tabla[linea, columna] = "X";
        return true;
      }
      return false;
    }

    static int DeterminarColumna(int eleccion)
    {
      int columna = (eleccion % 3) == 0 ? 2 : (eleccion % 3) - 1;
      return columna;
    }

    static int DeterminaLinea(int eleccion)
    {
      int linea;
      if (eleccion == 1 || eleccion == 2 || eleccion == 3)
      {
        linea = 0;
      }
      else if (eleccion == 4 || eleccion == 5 || eleccion == 6)
      {
        linea = 1;
      }
      else
      {
        linea = 2;
      }
      return linea;
    }

    static bool ComprobacionCelda(string[,] tabla, int eleccion, int linea)
    {

      return true;
    }

    static void ImprimirTabla(string[,] tabla)
    {
      //                                                                  1ª DIMENSION  === 3 elementos / lineas
      //                                                                 ______________________________________
      //
      //                                                                  2ª DIMESION   ===  3 elemntos / columnas en cada linea
      //                                                                  __________   __________   __________
      // string[,] tabla = new string[1ª dimension, 2ª dimension] ===> { { e, e, e }, { e, e, e }, { e, e, e } }
      // 
      // .GetLength(X) es 0-based donde X es el numero de la dimension
      // .GetLength(0) => (0 == 1ª dimension) => numero de lineas en la dimension 1  (1ª dimension) { (1 linea){...}, (2 linea){...}, (3 linea){...} } => 3 lineas en la matriz
      // .GetLength(1) => (1 == 2ª dimension) => numero de columnas en cada linea    (2ª dimension) { (1 linea){e,e,e}, (2 linea){e,e,e}, (3 linea){e,e,e} } => 3 elementos en cada linea

      int totalLineas = tabla.GetLength(0);
      int totalElementos = tabla.GetLength(1);
      for (int linea = 0; linea < totalLineas; linea++)
      {
        for (int columna = 0; columna < totalElementos; columna++)
        {
          System.Console.Write($" {tabla[linea, columna]} {(columna < totalElementos - 1 ? "|" : "")}{(columna == totalElementos - 1 ? "\n" : "")}");
        }
        if (!(linea == totalLineas - 1))
        {
          System.Console.WriteLine("-----------");
        }

      }

    }
  }
}