using System.Threading;
using System.Text;
using System.Dynamic;

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
      const int columnWidth = 13;
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\nINFO COCHES");
      Console.ForegroundColor = ConsoleColor.Black;
      Console.WriteLine($"{"Nombre".PadRight(columnWidth)}{"Modelo".PadRight(columnWidth)}{"Color".PadRight(columnWidth)}{"Velocidad Maxima".PadRight(columnWidth)}\n");
      for (int i = 0; i < coches.Length; i++)
      {
        coches[i].InfoCoche();
      }
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\nINFO PISTA\n");
      Console.ForegroundColor = ConsoleColor.Black;
      pista.InfoPista();

      Console.WriteLine("-------------------------------\n");
    }

    private static void EmpezarCarrera(Coche[] coches, Pista pista)
    {
      Random r = new Random();
      int FIN = pista.LongitudPista;
      int tiempoActualizacion = 500;

      dynamic[] estadisticasCochesRealtime = new dynamic[coches.Length];
      for (int i = 0; i < estadisticasCochesRealtime.Length; i++)
      {
        dynamic datosRealtime = new ExpandoObject();
        datosRealtime.nombre = coches[i].Nombre;
        datosRealtime.velocidad = 0;
        datosRealtime.recorridoX = 0;
        estadisticasCochesRealtime[i] = datosRealtime;
      }

      double maxRecorrido = 0;
      do
      {
        Thread.Sleep(tiempoActualizacion);

        /* ------- LOOP COCHES -------*/

        for (int i = 0; i < coches.Length; i++)
        {
          dynamic datosRealtime = new ExpandoObject();
          datosRealtime.nombre = coches[i].Nombre;
          datosRealtime.velocidad = r.NextDouble() * (coches[i].VelocidadMaxima + 1);
          datosRealtime.recorridoX = RecorridoEnTiempoX(datosRealtime.velocidad) + estadisticasCochesRealtime[i].recorridoX;
          estadisticasCochesRealtime[i] = datosRealtime;
          maxRecorrido = datosRealtime.recorridoX > maxRecorrido ? datosRealtime.recorridoX : maxRecorrido;
        }

        /*-------- GRAFICO -----------*/
        Console.Clear();
        MuestraDatos(coches, pista);
        MostrarGrafico(coches, estadisticasCochesRealtime, FIN);
      } while (maxRecorrido <= FIN);

      string ganador = estadisticasCochesRealtime.Where(datoCoche => datoCoche.recorridoX == maxRecorrido).Select(datoCoche => datoCoche.nombre).Single();

      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write($"\nEl ganador es: {ganador}\n\n");
      Console.ForegroundColor = ConsoleColor.Black;
    }

    private static void MostrarGrafico(Coche[] coches, dynamic[] datos, int FIN)
    {
      int totalCeldas = 30;
      var grafico = new StringBuilder();


      /* Por cada coche crea una linea */
      for (int i = 0; i < coches.Length; i++)
      {
        int celdasRecorridas = Convert.ToInt32(Math.Floor((datos[i].recorridoX * totalCeldas) / FIN));

        if (celdasRecorridas > totalCeldas) celdasRecorridas = totalCeldas;

        string celdas = "";
        for (int j = 0; j < celdasRecorridas; j++)
        {
          celdas += "#";
        }

        grafico.AppendLine($"{coches[i].Nombre.PadRight(13)}|{celdas.PadRight(30)}| V.Actual: {datos[i].velocidad:f2} km/h | Tot.recorrido: {datos[i].recorridoX:f2} m");
      }

      System.Console.WriteLine($"{grafico}");

    }

    private static double RecorridoEnTiempoX(double velocidad, int tiempoX = 1000)
    {
      return velocidad * (1000d / 3600d) * ((double)tiempoX / 1000d);
    }
  }
}