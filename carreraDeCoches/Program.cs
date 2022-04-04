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
          numeroDeCoches = 2;
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

      bool todosEnAveria = false;

      dynamic[] estadisticasCochesRealtime = new dynamic[coches.Length];
      for (int i = 0; i < estadisticasCochesRealtime.Length; i++)
      {
        dynamic datosRealtime = new ExpandoObject();
        datosRealtime.nombre = coches[i].Nombre;
        datosRealtime.velocidadFinal = 0;
        datosRealtime.recorridoX = 0;
        datosRealtime.totalVelocidades = 0;
        datosRealtime.recorridoTotal = 0;
        datosRealtime.tiempoTranscurrido = 0;
        estadisticasCochesRealtime[i] = datosRealtime;
      }

      double maxRecorrido = 0;
      do
      {
        Thread.Sleep(tiempoActualizacion);

        /* RANDOMIZAR AVeRIA ANTES DE CALCULOS*/
        foreach (Coche coche in coches)
        {
          if (!coche.Averia)
            coche.RandomizarAveria();
        }

        /* ------- LOOP COCHES -------*/
        /* CALCULOS DE LOS DATOS EN LA FRACCION DE TIEMPO */

        for (int i = 0; i < coches.Length; i++)
        {
          double velocidadInicial = 0;

          if (!coches[i].Averia)
          {
            velocidadInicial = r.NextDouble() * (coches[i].VelocidadMaxima + 1);
            estadisticasCochesRealtime[i].velocidadFinal = CalculoVelocidadFinal(velocidadInicial, pista.Friccion);
            estadisticasCochesRealtime[i].recorridoX = EspacioRecorrido(estadisticasCochesRealtime[i].velocidadFinal);
            estadisticasCochesRealtime[i].totalVelocidades += estadisticasCochesRealtime[i].velocidadFinal;
            estadisticasCochesRealtime[i].recorridoTotal += estadisticasCochesRealtime[i].recorridoX;
          }
          else
          {
            estadisticasCochesRealtime[i].velocidadFinal = 0;
          }

          estadisticasCochesRealtime[i].tiempoTranscurrido++;
          maxRecorrido = estadisticasCochesRealtime[i].recorridoTotal > maxRecorrido ? estadisticasCochesRealtime[i].recorridoTotal : maxRecorrido;
        }

        /*-------- GRAFICO -----------*/
        Console.Clear();
        MuestraDatos(coches, pista);
        MostrarGrafico(coches, estadisticasCochesRealtime, FIN);

        todosEnAveria = coches.All((coche) => coche.Averia);

      } while (maxRecorrido <= FIN && !todosEnAveria);

      if (!todosEnAveria)
      {
        string ganador = estadisticasCochesRealtime.Where(datoCoche => datoCoche.recorridoTotal == maxRecorrido).Select(datoCoche => datoCoche.nombre).Single();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"\nEl ganador es: {ganador}\n\n");
        Console.ResetColor();

      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\nTodos los coches en averias. No hay ganador.\n\n");
        Console.ResetColor();
      }

    }

    private static void MostrarGrafico(Coche[] coches, dynamic[] datos, int FIN)
    {
      int totalCeldas = 30;
      var header = new StringBuilder();

      /* crea header */
      header.Append($"{"Nombre".PadRight(13)}");
      header.Append($"| {"Trayecto".PadRight(30)}");
      header.Append($"| {"Velocidad Actual".PadRight(15)}");
      header.Append($"| {"Velocidad Media".PadRight(15)}");
      header.Append($"| Tot.recorrido".PadRight(15));
      header.AppendLine($"| Estado Averia");
      header.AppendLine(string.Concat(Enumerable.Repeat("-", Console.WindowWidth)));

      var grafico = new StringBuilder();
      /* Por cada coche crea una linea */
      for (int i = 0; i < coches.Length; i++)
      {
        int celdasRecorridas = Convert.ToInt32(Math.Floor((double)(datos[i].recorridoTotal * totalCeldas) / FIN));

        if (celdasRecorridas > totalCeldas) celdasRecorridas = totalCeldas;

        string celdas = "";
        for (int j = 0; j < celdasRecorridas; j++)
        {
          celdas += "#";
        }
        /* crea linea */
        grafico.Append($"{coches[i].Nombre.PadRight(13)}");
        grafico.Append($"| {celdas.PadRight(30)}");
        grafico.Append($"| {datos[i].velocidadFinal:f2} km/h".PadRight(18));
        grafico.Append($"| {(datos[i].recorridoTotal / datos[i].tiempoTranscurrido):f2} km/h".PadRight(17));
        grafico.Append($"| {datos[i].recorridoTotal:f2} m".PadRight(15));
        string status = coches[i].Averia == true ? coches[i].ComponenteEnAveria.Item1 : "Todo bien";
        grafico.AppendLine($"| {status}");
      }
      Console.WriteLine(header);
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine(grafico);

      Console.ResetColor();
    }

    private static double CalculoVelocidadFinal(double velocidadInicial, (string, double) friccion)
    {
      double calc = velocidadInicial - (friccion.Item2 * 9.8 * 1);
      if (calc <= 0) calc = 0;
      return calc;
    }

    private static double EspacioRecorrido(double velocidad)
    {
      return velocidad * (1000d / 3600d) * 1;
    }
  }
}