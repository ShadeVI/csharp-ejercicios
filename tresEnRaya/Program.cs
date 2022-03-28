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

      string? usuarioEleccion = "";

      do
      {
        System.Console.WriteLine("Posiciones: \n 1 2 3 \n 4 5 6 \n 7 8 9");
        System.Console.Write("Introduce tu eleccion: ");
        usuarioEleccion = Console.ReadLine();

        if (string.IsNullOrEmpty(usuarioEleccion)) continue;
        if (usuarioEleccion == "q")
        {
          jugando = false;
          continue;
        }

        IntroducirValor(tabla, Convert.ToInt32(usuarioEleccion));

        ImprimirTabla(tabla);


      } while (jugando);
    }

    static void IntroducirValor(string[,] tabla, int eleccion)
    {
      bool riga1 = eleccion == 1 || eleccion == 2 || eleccion == 3;
      bool riga2 = eleccion == 4 || eleccion == 5 || eleccion == 6;
      if (riga1)
      {
        tabla[0, eleccion - 1] = "X";
      }
      else if (riga2)
      {
        tabla[1, eleccion - 4] = "X";
      }
      else
      {
        tabla[2, eleccion - 7] = "X";
      }
    }

    static void ImprimirTabla(string[,] tabla)
    {
      //                                                                  1ª DIMENSION === 3 elementos / lineas
      //                                                                 ______________________________________
      //
      //                                                                  2ª dim   ===  3 elemntos / columnas en cada linea
      //                                                                  __________   __________   __________
      // string[,] tabla = new string[1ª dimension, 2ª dimension] ===> { { e, e, e }, { e, e, e }, { e, e, e } }
      // 
      // .GetLength(X) es 0-based donde X es el numero de la dimension
      // .GetLength(0) => (0 == 1ª dimension) => numero de lineas en la dimension 1  (1ª dimension) { (1 linea){...}, (2 linea){...}, (3 linea){...} } => 3 lineas en la matriz
      // .GetLength(1) => (1 == 2ª dimension) => numero de columnas en cada linea    (2ª dimension) { (1 linea){e,e,e}, (2 linea){e,e,e}, (3 linea){e,e,e} } => 3 elementos en cada linea

      int totalLineas = tabla.GetLength(0);
      int totalElementos = tabla.GetLength(1);
      for (int row = 0; row < totalLineas; row++)
      {
        for (int col = 0; col < totalElementos; col++)
        {
          System.Console.Write($" {tabla[row, col]} {(col < totalElementos - 1 ? "|" : "")}{(col == totalElementos - 1 ? "\n" : "")}");
        }
        if (!(row == totalLineas - 1))
        {
          System.Console.WriteLine("-----------");
        }

      }

    }
  }
}