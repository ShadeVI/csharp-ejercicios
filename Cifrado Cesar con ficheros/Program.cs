namespace Cifrado
{
  class CifradoCesar
  {
    static void Main(string[] args)
    {

      LecturaNombresFicheros(out string nombreFicheroEntrada, out string nombreFicheroSalida);

      try
      {
        EscrituraLecturaFicheros(nombreFicheroEntrada, nombreFicheroSalida);

        string respuestaVisualizar;
        do
        {
          Console.Write("Ejecucion correcta. Visualizar el contenido de los ficheros creados? (S/N) : ");
          respuestaVisualizar = Console.ReadLine();

          if (respuestaVisualizar.ToLower() == "n")
          {
            return;
          }
          else if (respuestaVisualizar.ToLower() == "s")
          {
            MostrarContenidoFicheros(new string[] { nombreFicheroEntrada, nombreFicheroSalida });
            return;
          }
          else
          {
            Console.WriteLine("No valido");
          }
        } while (respuestaVisualizar != "n" && respuestaVisualizar != "s");
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("El fichero no existe.");
      }
      catch (Exception e)
      {
        Console.WriteLine("Error, algo salio mal: " + e.Message);
      }



    }

    static void MostrarContenidoFicheros(string[] nombresFicheros)
    {
      foreach (string nombreFichero in nombresFicheros)
      {
        FileStream fichero = new FileStream(nombreFichero, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fichero);
        Console.WriteLine($"Contenido del fichero {fichero}:");
        try
        {
          string linea;

          do
          {
            linea = sr.ReadLine();
            if (linea != null)
            {
              Console.WriteLine(sr.ReadLine());
            }
          } while (linea != null);
        }
        catch (IOException e)
        {
          Console.WriteLine("Error de E/S: " + e.Message);
        }
        catch (Exception e)
        {
          Console.WriteLine("Error: " + e.Message);
        }
        sr.Close();
        fichero.Close();
        Console.WriteLine("_____________________________________\n\n");
      }
    }

    static void LecturaNombresFicheros(out string ficheroEntrada, out string ficheroSalida)
    {
      Console.Write("Nombre fichero de entrada: ");
      ficheroEntrada = Console.ReadLine();
      Console.Write("\nNombre fichero de salida: ");
      ficheroSalida = Console.ReadLine();
    }

    static void EscrituraLecturaFicheros(string nombreFicheroEntrada, string nombreFicheroSalida)
    {
      // Abrimos el fichero de Entrada
      FileStream ficheroEntrada = new FileStream(nombreFicheroEntrada, FileMode.Open, FileAccess.Read);

      // Abrimos el fichero de Salida
      FileStream ficheroSalida = new FileStream(nombreFicheroSalida, FileMode.CreateNew, FileAccess.Write);

      // Creamos flujo de lectura
      StreamReader srEntrada = new StreamReader(ficheroEntrada);
      // Creamos flujo de escritura
      StreamWriter srSalida = new StreamWriter(ficheroSalida);
      try
      {
        string lineaEntrada;

        do
        {
          lineaEntrada = srEntrada.ReadLine();
          if (lineaEntrada != null)
          {
            srSalida.WriteLine(ROT13Algo(lineaEntrada));
          }
        } while (lineaEntrada != null);
      }
      catch (IOException e)
      {
        Console.WriteLine("Error de E/S: " + e.Message);
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: " + e.Message);
      }
      finally
      {
        srSalida.Close();
        srEntrada.Close();
        ficheroSalida.Close();
        ficheroEntrada.Close();
      }
    }

    private static string ROT13Algo(string texto)
    {

      string textoCifrado = "";

      foreach (char letra in texto)
      {
        int letraInt = (int)letra;

        bool esLetraValida = (letraInt >= 65 && letraInt <= 90) || (letraInt >= 97 && letraInt <= 122);

        if (esLetraValida)
        {
          if ((letraInt >= 78 && letraInt <= 90) || letraInt >= 109)
          {
            letraInt -= 13;
          }
          else
          {
            letraInt += 13;
          }
          textoCifrado += (char)letraInt;
        }
        else
        {
          textoCifrado += letra;
        }

      }
      return textoCifrado;
    }
  }
}