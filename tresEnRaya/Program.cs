namespace Program
{
  class TresEnRayaMain
  {
    enum Jugadores
    {
      Jugador1,
      Jugador2
    };
    static void Main(string[] args)
    {
      string[,] tabla = new string[3, 3] {
                                          { " ", " ", " " },
                                          { " ", " ", " " },
                                          { " ", " ", " " }
                                        };

      bool jugando = true;
      bool hasGanado = false;

      Jugadores turno = (Jugadores)DeterminarJugadorInicial();

      string? lecturaUsuario = "";

      do
      {
        System.Console.WriteLine($"\n\nTurno jugador: {turno}");
        System.Console.WriteLine("\nPosiciones: \n 1 2 3 \n 4 5 6 \n 7 8 9");
        System.Console.Write("\nIntroduce tu eleccion: ");
        lecturaUsuario = Console.ReadLine();

        if (string.IsNullOrEmpty(lecturaUsuario)) continue;
        if (lecturaUsuario == "q")
        {
          jugando = false;
          continue;
        }

        int usuarioEleccion = Convert.ToInt32(lecturaUsuario) - 1;

        int linea = DeterminarLinea(usuarioEleccion);
        int columna = DeterminarColumna(usuarioEleccion);

        bool hasIntroducidoValor = IntroducirValor(tabla, linea, columna, turno);

        hasGanado = DeterminarGanador(tabla);
        if (hasGanado)
        {
          jugando = false;
        }

        ImprimirTabla(tabla);

        if (!hasIntroducidoValor)
        {
          System.Console.WriteLine("La celda está ocupada! Intenta otra posición");
          continue;
        }

        if (!hasGanado) turno = Jugadores.Jugador2 ^ turno;



      } while (jugando);

      Console.WriteLine($"\n\nEl ganador es {turno}\n\n");
    }

    static bool IntroducirValor(string[,] tabla, int linea, int columna, Jugadores turno)
    {
      bool puedeIntroducir = ComprobacionCelda(tabla, linea, columna);

      if (puedeIntroducir)
      {
        tabla[linea, columna] = turno == Jugadores.Jugador1 ? "X" : "O";
        return true;
      }
      return false;
    }

    static int DeterminarColumna(int eleccion)
    {
      int columna = ((eleccion + 1) % 3) == 0 ? 2 : ((eleccion + 1) % 3) - 1;
      return columna;
    }

    static int DeterminarLinea(int eleccion)
    {
      int linea;
      if (eleccion == 0 || eleccion == 1 || eleccion == 2)
      {
        linea = 0;
      }
      else if (eleccion == 3 || eleccion == 4 || eleccion == 5)
      {
        linea = 1;
      }
      else
      {
        linea = 2;
      }
      return linea;
    }

    static bool ComprobacionCelda(string[,] tabla, int linea, int columna)
    {
      if (tabla[linea, columna] == "X" || tabla[linea, columna] == "O") return false;
      return true;
    }

    static void ImprimirTabla(string[,] tabla)
    {

      // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
      // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

    static bool DeterminarGanador(string[,] tabla)
    {
      string[,] patterns = new string[8, 3] {
        // Lineas Horizontales
        {tabla[0,0], tabla[0,1], tabla[0,2]},
        {tabla[1,0],tabla[1,1],tabla[1,2]},
        {tabla[2,0],tabla[2,1],tabla[2,2],},
        // Lineas Verticales
        {tabla[0,0],tabla[1,0],tabla[2,0]},
        {tabla[0,1],tabla[1,1],tabla[2,1]},
        {tabla[0,2],tabla[1,2],tabla[2,2]},
        // Lineas Obliquas
        {tabla[0,0],tabla[1,1],tabla[2,2]},
        {tabla[0,2],tabla[1,1],tabla[2,0]},
      };

      int totalLineas = patterns.GetLength(0);

      bool hayGanador = false;

      for (int linea = 0; linea < totalLineas; linea++)
      {
        bool iguales = patterns[linea, 0] == patterns[linea, 1] && patterns[linea, 0] == patterns[linea, 2];
        bool vacios = patterns[linea, 0] == " " || patterns[linea, 1] == " " || patterns[linea, 2] == " ";
        if (iguales && !vacios)
        {
          hayGanador = true;
        }
      }
      return hayGanador;
    }

    static int DeterminarJugadorInicial()
    {
      Random random = new Random();
      double num = random.NextDouble();
      return Convert.ToInt16(Math.Round(num, 1));
    }
  }
}