using System;
using System.Text.RegularExpressions;

namespace Program1
{
  class Program
  {
    public static char[,]? mapaSolucion;
    public static char[,]? mapaBase;

    private static (int X, int Y) coordenadasInicio = (0, 0);
    private static (int? X, int? Y) coordenadasFin = (null, null);

    private static char PARED = '\u2586';
    private static char FIN = '\u25EF';
    private static char INICIO = '\u26A1';

    private static char DERECHA = '\u25BA';
    private static char ARRIBA = '\u25B2';
    private static char IZQUIERDA = '\u25C4';
    private static char ABAJO = '\u25BC';

    private static char LLEGADA = '\u2705';
    public static int numeroPasosTotales = 0;
    static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      string dimensionMapa = "";

      bool continuar = true;

      while (continuar)
      {
        // reset variables
        numeroPasosTotales = 0;
        mapaSolucion = null;
        mapaBase = null;

        Console.WriteLine("Cuanto debe ser grande el mapa (ejemplo: 5x5)? ");
        dimensionMapa = Console.ReadLine();
        (int X, int Y) dims = ObtenerDimensiones(dimensionMapa);

        ConfigurarMapaBase(out mapaBase, dims.X, dims.Y);
        ImprimirMapa(mapaBase);

        // Los array se pasan por referencia. Necesitamos crear copias para guardar el estado anterior
        char[,] cloneMapaBase = ClonarMapa(mapaBase);

        Console.WriteLine($"\nBuscando ruta...\n");
        //Recorremos el mapa, empieza la recursividad.
        // RecorrerMapa(mapa, posX, posY, pasosActuales, posXanterior, posYanterior)
        RecorrerMapa(cloneMapaBase, coordenadasInicio.X, coordenadasInicio.Y, 0, coordenadasInicio.X, coordenadasInicio.Y);

        if (numeroPasosTotales > 0)
        {
          Console.WriteLine($"\nRuta mas rapida: {numeroPasosTotales} pasos\n");
          ImprimirMapa(mapaSolucion);
        }
        else
        {
          Console.WriteLine($"\nSolucion no encontrada.\n");
        }

        Console.WriteLine("\nTecle Q para salir o ENTER para continuar: ");
        if (Console.ReadLine().ToUpper() == "Q") continuar = false;
      };

    }

    private static void RecorrerMapa(char[,] mapa, int posX, int posY, int pasosActuales, int anteriorX, int anteriorY)
    {
      // Si hemos llegado al final ------ CASO BASE funcion recursiva --------
      if (mapa[posX, posY] == FIN)
      {
        // marcamos la fin
        mapa[posX, posY] = LLEGADA;
        if (numeroPasosTotales == 0 || pasosActuales < numeroPasosTotales)
        {
          // Si aun no se habia hallado la solucion O si los pasos de la actual solucion son menores a los pasosTotales de una solucion anterior, marcarla como solucion.
          mapaSolucion = ClonarMapa(mapa);
          numeroPasosTotales = pasosActuales;
        }
      }
      // Si no chocamos con una pared Y si ya no hemos pasado por esa casilla Y si los
      // pasosActuales son menores a los pasosTotal de una posibile solucion anterior O si no se ha hallado aun una solucion
      // seguimos buscando una ruta.
      else if (mapa[posX, posY] != PARED && (mapa[posX, posY] != ARRIBA && mapa[posX, posY] != ABAJO && mapa[posX, posY] != DERECHA && mapa[posX, posY] != IZQUIERDA) && (pasosActuales < numeroPasosTotales || numeroPasosTotales == 0))
      {
        if (mapa[posX, posY] == INICIO)
        {
          mapa[posX, posY] = INICIO;
        }
        // Marcar la casilla dependiendo de la posicion anterior
        else if (posX > anteriorX)
        {
          mapa[posX, posY] = ABAJO;
        }
        else if (posX < anteriorX)
        {
          mapa[posX, posY] = ARRIBA;
        }
        else if (posY > anteriorY)
        {
          mapa[posX, posY] = DERECHA;
        }
        else
        {
          mapa[posX, posY] = IZQUIERDA;
        }


        // logica siguiente paso
        // paso ARRIBA
        if (posX > 0) RecorrerMapa(ClonarMapa(mapa), posX - 1, posY, pasosActuales + 1, posX, posY);
        // paso ABAJO
        if (posX < mapa.GetUpperBound(0)) RecorrerMapa(ClonarMapa(mapa), posX + 1, posY, pasosActuales + 1, posX, posY);
        // paso IZQUIERDA
        if (posY > 0) RecorrerMapa(ClonarMapa(mapa), posX, posY - 1, pasosActuales + 1, posX, posY);
        // paso DERECHA
        if (posY < mapa.GetUpperBound(1)) RecorrerMapa(ClonarMapa(mapa), posX, posY + 1, pasosActuales + 1, posX, posY);

      }
    }

    private static char[,] ClonarMapa(char[,] mapa)
    {
      char[,] clone = new char[mapa.GetLength(0), mapa.GetLength(1)];

      for (int x = 0; x < mapa.GetLength(0); x++)
      {
        for (int y = 0; y < mapa.GetLength(1); y++)
        {
          clone[x, y] = mapa[x, y];
        }
      }
      return clone;
    }

    private static void ImprimirMapa(char[,] mapa)
    {
      for (int x = 0; x < mapa.GetLength(0); x++)
      {
        for (int y = 0; y < mapa.GetLength(1); y++)
        {
          Console.Write(mapa[x, y] + " ");
        }
        System.Console.WriteLine();
      }
    }

    private static void ConfigurarMapaBase(out char[,] mapaBase, int dimX, int dimY)
    {
      char[,] newMapaBase = new char[dimX, dimY];
      Random rand = new Random();

      coordenadasInicio = (rand.Next(0, dimX), rand.Next(0, dimY));
      coordenadasFin = (rand.Next(0, dimX), rand.Next(0, dimY));

      while (coordenadasFin.X == coordenadasInicio.X && coordenadasFin.Y == coordenadasInicio.Y)
      {
        coordenadasFin = (rand.Next(0, dimX), rand.Next(0, dimY));
      }

      for (int x = 0; x < newMapaBase.GetLength(0); x++)
      {
        for (int y = 0; y < newMapaBase.GetLength(1); y++)
        {
          // genera un numero entre 0 y 1000. Se el numero es menor de 100, genera pared
          bool probabilidadPared = Math.Round(rand.NextDouble() * 1000) < 100 ? true : false;

          if (x == coordenadasInicio.X && y == coordenadasInicio.Y)
          {
            newMapaBase[x, y] = INICIO;
          }
          else if (x == coordenadasFin.X && y == coordenadasFin.Y)
          {
            newMapaBase[x, y] = FIN;
          }
          else
          {
            newMapaBase[x, y] = probabilidadPared ? PARED : '·';
          }
        }
      }

      mapaBase = newMapaBase;
    }

    private static (int, int) ObtenerDimensiones(string dimension)
    {
      Regex regPattern = new Regex(@"([0-9]{1,2})x([0-9]{1,2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      MatchCollection matches = regPattern.Matches(dimension);

      if (matches.Count > 0)
      {

        GroupCollection groups = matches[0].Groups;
        System.Console.WriteLine($"\nCreando mapa base {int.Parse(groups[1].Value)}x{int.Parse(groups[2].Value)}\n");
        return (int.Parse(groups[1].Value), int.Parse(groups[2].Value));

      }

      System.Console.WriteLine("Dimensiones no validas, usando matriz 7x7\n");
      return (7, 7);
    }
  }
}