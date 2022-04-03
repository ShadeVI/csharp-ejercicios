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
      const int columnWidth = 14;
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\nINFO COCHES");
      Console.ResetColor();
      Console.WriteLine($"{"Nombre".PadRight(columnWidth)}{"Modelo".PadRight(columnWidth)}{"Color".PadRight(columnWidth)}{"Peso".PadRight(columnWidth)}{"Velocidad Maxima".PadRight(columnWidth)}\n");
      for (int i = 0; i < coches.Length; i++)
      {
        coches[i].InfoCoche();
      }
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("\nINFO PISTA\n");
      Console.ResetColor();
      pista.InfoPista();

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
        datosRealtime.velocidadFinal = 0;
        datosRealtime.recorridoX = 0;
        estadisticasCochesRealtime[i] = datosRealtime;
      }

      double maxRecorrido = 0;
      do
      {
        Thread.Sleep(tiempoActualizacion);

        /* ------- LOOP COCHES -------*/

        /* CALCULOS DE LOS DATOS EN LA FRACCION DE TIEMPO */

        for (int i = 0; i < coches.Length; i++)
        {
          double velocidadInicial = r.NextDouble() * (coches[i].VelocidadMaxima + 1);
          dynamic datosRealtime = new ExpandoObject();
          datosRealtime.nombre = coches[i].Nombre;
          datosRealtime.velocidadFinal = CalculoVelocidadFinal(velocidadInicial, pista.Friccion, tiempoActualizacion);
          datosRealtime.recorridoX = EspacioRecorrido(datosRealtime.velocidadFinal, tiempoActualizacion) + estadisticasCochesRealtime[i].recorridoX;
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
      Console.ResetColor();
    }

    private static void MostrarGrafico(Coche[] coches, dynamic[] datos, int FIN)
    {
      int totalCeldas = 30;
      var header = new StringBuilder();

      /* crea header */
      header.Append($"{"Nombre".PadRight(13)}");
      header.Append($"| {"".PadRight(30)}");
      header.Append($"| {"Velocidad Actual".PadRight(20)}");
      header.AppendLine($"| Tot.recorrido");
      header.AppendLine(string.Concat(Enumerable.Repeat("-", Console.WindowWidth)));

      /* Por cada coche crea una linea */
      for (int i = 0; i < coches.Length; i++)
      {
        var linea = new StringBuilder();
        int celdasRecorridas = Convert.ToInt32(Math.Floor((datos[i].recorridoX * totalCeldas) / FIN));

        if (celdasRecorridas > totalCeldas) celdasRecorridas = totalCeldas;

        string celdas = "";
        for (int j = 0; j < celdasRecorridas; j++)
        {
          celdas += "#";
        }
        /* crea linea */
        Console.ForegroundColor = ConsoleColor.Cyan;
        linea.Append($"{coches[i].Nombre.PadRight(13)}");
        linea.Append($"| {celdas.PadRight(30)}");
        linea.Append($"| {datos[i].velocidadFinal:f2} km/h".PadRight(22));
        linea.AppendLine($"| {datos[i].recorridoX:f2} m");
        Console.WriteLine($"{linea}");
        Console.ResetColor();
      }


    }

    private static double CalculoVelocidadFinal(double velocidadInicial, (string, double) friccion, int tiempo = 1000)
    {
      double calc = velocidadInicial - (friccion.Item2 * 9.8 * 1);
      if (calc <= 0) calc = 0;
      return calc;
    }

    private static double EspacioRecorrido(double velocidad, int tiempoX = 1000)
    {
      return velocidad * (1000d / 3600d) * 1;
    }
  }
}