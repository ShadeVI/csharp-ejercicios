using System;
using System.Text.RegularExpressions;

namespace Program1
{
  class Program
  {
    public static char[,]? mapaSolucion;
    public static char[,]? mapaBase;
    static void Main(string[] args)
    {
      string dimensionMapa = "";
      bool continuar = true;
      while (continuar)
      {
        Console.WriteLine("Cuanto debe ser grande el mapa (ejemplo: 5x5)? ");
        dimensionMapa = Console.ReadLine();
        (int X, int Y) dims = ObtenerDimensiones(dimensionMapa);

        ConfigurarMapaBase(out mapaBase, dims.X, dims.Y);

        ImprimirMapa(mapaBase);


        Console.WriteLine("Tecle Q para salir: ");
        if (Console.ReadLine().ToUpper() == "Q") continuar = false;
      };

    }

    private static void ImprimirMapa(char[,] mapa)
    {
      for (int x = 0; x < mapa.GetLength(0); x++)
      {
        for (int y = 0; y < mapa.GetLength(1); y++)
        {
          Console.Write(mapa[x, y]);
        }
        System.Console.WriteLine();
      }
    }

    private static void ConfigurarMapaBase(out char[,] mapaBase, int dimX, int dimY)
    {
      char[,] newMapaBase = new char[dimX, dimY];
      Random rand = new Random();

      for (int x = 0; x < newMapaBase.GetLength(0); x++)
      {
        for (int y = 0; y < newMapaBase.GetLength(1); y++)
        {
          // genera un numero entre 0 y 1000. Se el numero es menor de 100, genera pared
          bool probabilidadPared = Math.Round(rand.NextDouble() * 1000) < 100 ? true : false;

          if (x == 0 && y == 0)
          {
            newMapaBase[x, y] = 'I';
          }
          else if (x == newMapaBase.GetUpperBound(0) && y == newMapaBase.GetUpperBound(1)) newMapaBase[x, y] = 'F';
          else
          {
            newMapaBase[x, y] = probabilidadPared ? '#' : '·';
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
        return (int.Parse(groups[1].Value), int.Parse(groups[2].Value));

      }

      System.Console.WriteLine("Dimensiones no validas, usando matriz 5x5");
      return (5, 5);
    }
  }
}