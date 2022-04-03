using System.Threading;
using System.Text;

namespace CarreraCoches
{
  class Program
  {
    static void Main(string[] args)
    {
      string empezar;
      int numeroDeCoches = 2;


      bool salir = false;
      do
      {
        Console.Write("Cuantos coches van a correr? (MAX: 10) ");
        try
        {
          string numCochesUsuario = Console.ReadLine() ?? "2";
          numeroDeCoches = Int32.Parse(numCochesUsuario);
          if (numeroDeCoches > 10 || numeroDeCoches < 2) throw new ArgumentOutOfRangeException();
        }
        catch (FormatException)
        {
          System.Console.WriteLine("Formato no valido...\n\n");
          continue;
        }
        catch (ArgumentOutOfRangeException)
        {
          System.Console.WriteLine("Numero de coches no valido. Valor usado: 2.\n\n");
        }

        Console.WriteLine("Empezamos la carrera?");
        empezar = Console.ReadLine() ?? "";
        if (empezar.ToLower() == "s" && !string.IsNullOrEmpty(empezar))
        {
          Coche[] listaDeCoches = new Coche[numeroDeCoches];

          for (int i = 0; i < numeroDeCoches; i++)
          {
            listaDeCoches[i] = new Coche();
          }

          Pista pista = new Pista();


          Console.Clear();
          EmpezarCarrera(listaDeCoches, pista);
        }
        else
        {
          salir = !salir;
        }
      } while (!salir);

    }

    private static void MuestraDatos(Coche[] coches, Pista pista)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\nINFO COCHES");
      Console.ForegroundColor = ConsoleColor.Black;
      for (int i = 0; i < coches.Length; i++)
      {
        coches[i].InfoCoche();
      }
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\nINFO PISTA");
      Console.ForegroundColor = ConsoleColor.Black;
      pista.InfoPista();

      Console.WriteLine("-------------------------------\n");
    }

    private static void EmpezarCarrera(Coche[] coches, Pista pista)
    {
      Random r = new Random();
      int FIN = pista.LongitudPista;
      int tiempoActualizacion = 1000 / 300;

      double recorridoX = 0;
      double velocidad = 0;
      double[] totalesRecorridos = new double[coches.Length];

      double maxRecorrido;
      do
      {
        Thread.Sleep(tiempoActualizacion);

        /* ------- LOOP COCHES -------*/

        for (int i = 0; i < coches.Length; i++)
        {
          velocidad = r.NextDouble() * (coches[i].VelocidadMaxima + 1);
          recorridoX = PercorridoEnTiempoX(velocidad, tiempoActualizacion);
          totalesRecorridos[i] += recorridoX;
        }
        maxRecorrido = totalesRecorridos.Max();

        /*-------- GRAFICO -----------*/
        Console.Clear();
        MuestraDatos(coches, pista);
        MostrarGrafico(coches, totalesRecorridos, FIN);
      } while (maxRecorrido <= FIN);

      string ganador = coches[Array.IndexOf(totalesRecorridos, maxRecorrido)].Nombre;
      System.Console.WriteLine(ganador);
    }

    private static void MostrarGrafico(Coche[] coches, double[] totalesRecorridos, int FIN)
    {
      int totalCeldas = 30;
      var grafico = new StringBuilder();


      /* Por cada coche crea una linea */
      for (int i = 0; i < coches.Length; i++)
      {
        int celdasRecorridas = Convert.ToInt32(Math.Floor((totalesRecorridos[i] * totalCeldas) / FIN));

        if (celdasRecorridas > totalCeldas) celdasRecorridas = totalCeldas;

        string celdas = "";
        for (int j = 0; j < celdasRecorridas; j++)
        {
          celdas += "#";
        }

        grafico.AppendLine($"{coches[i].Nombre.PadRight(13)}|{celdas.PadRight(30)}| FIN | Tot.recorrido: {totalesRecorridos[i]:f2} m");
      }

      System.Console.WriteLine($"{grafico}");

    }

    private static double PercorridoEnTiempoX(double velocidad, int tiempoX)
    {
      return velocidad * (1000d / 3600d) * ((double)tiempoX / 1000d);
    }
  }
}