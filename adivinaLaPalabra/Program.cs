namespace Program
{
  class AdivinaLaPalabra
  {
    static void Main(string[] args)
    {
      string[] palabras = Utilidades.Archivos.LeerArchivos();
      string palabra = Utilidades.Palabras.PRandom(palabras);
      string palabraOculta = Utilidades.Palabras.Ocultar(palabra);

      bool hasPerdido = false;
      bool juegoTerminado = false;

      int intentos = 5;
      string letraOPalabra = "";

      string letrasUsada = "";

      Introduccion();

      while (!hasPerdido && intentos > 0 && !juegoTerminado)
      {

        Console.WriteLine("Te queda{0} {2} intento{1}", (intentos > 1 ? "n" : ""), (intentos > 1 ? "s" : ""), intentos);
        Console.WriteLine(palabra);
        Console.WriteLine(palabraOculta);


        try
        {
          Console.Write("\n\nInserir la letra o palabra: ");
          letraOPalabra = Console.ReadLine() ?? "";

          bool existeEnPalabra = Utilidades.Palabras.Controlar(letraOPalabra, palabra);
          bool esLetraUsada = letraOPalabra.Length == 1 ? letrasUsada.Contains(letraOPalabra) : false;

          if (!existeEnPalabra)
          {
            intentos--;
          }
          else if (esLetraUsada)
          {
            Console.WriteLine("Ya has usado esta letra.");
          }
          else
          {
            palabraOculta = Utilidades.Palabras.Desvelar(letraOPalabra, palabra, palabraOculta);
            letrasUsada += letraOPalabra;
            intentos++;
          }

        }
        catch (FormatException)
        {
          Console.WriteLine("Caracter no valido.");
        }

        if (palabra == palabraOculta) juegoTerminado = true;

      }

      if (juegoTerminado && !hasPerdido)
      {
        Console.WriteLine("Muy bien, has ganado!");
      }
      else
      {
        Console.WriteLine("Oh, no. Has perdido!");
      }

    }

    static void Introduccion()
    {
      string cabecera = "";
      cabecera += "#####################################\n";
      cabecera += "#                                   #\n";
      cabecera += "#       Bienvenido al juego         #\n";
      cabecera += "#                                   #\n";
      cabecera += "#####################################\n\n";
      cabecera += "El objectivo del juego es adivinar la palabra.\n\n";
      cabecera += "Tienes 5 intentos para empezar.\n";
      cabecera += "Cada error resta un intento.\n";
      cabecera += "Si adivina una letra, ganas un 1 intento.\n"; ;
      cabecera += "\n______________________________________\n\n\n";
      Console.WriteLine(cabecera);
    }
  }
}

namespace Program.Utilidades
{
  class Archivos
  {
    private static string currentDirectory = Directory.GetCurrentDirectory();
    public static string[] LeerArchivos(string nombreArchivo = "palabras3.txt")
    {
      string filesDirectory = Path.Combine(currentDirectory, $"utilidades");
      IEnumerable<string> archivos = Directory.EnumerateFiles(filesDirectory);

      string contenido = "";

      foreach (string archivo in archivos)
      {
        contenido += "\n" + File.ReadAllText(archivo);
      }

      contenido = contenido.Trim();

      return contenido.Split("\n");

    }
  }

  class Palabras
  {
    public static string PRandom(string[] arrPalabras)
    {
      Random random = new Random();
      int index = random.Next(0, arrPalabras.Length);
      string palabraRandom = arrPalabras[index];
      return palabraRandom.ToLower().Trim();
    }

    public static string Ocultar(string palabraOrigina)
    {
      string mask = "";
      foreach (char letra in palabraOrigina)
      {
        mask += "_";
      }
      return mask;
    }

    public static bool Controlar(string letraOPalabra, string palabraOriginal)
    {
      if (letraOPalabra.Length > 1 && letraOPalabra.Length < palabraOriginal.Length) return false;

      return palabraOriginal.Contains(letraOPalabra.ToLower()) ? true : false;
    }

    public static string Desvelar(string letra, string palabraOriginal, string oculta)
    {
      string nuevaOculta = "";

      if (letra.Length == palabraOriginal.Length && letra == palabraOriginal) return palabraOriginal;

      for (int i = 0; i < palabraOriginal.Length; i++)
      {
        bool letraEsOculta = oculta[i] == '_';
        bool letrasIguales = Convert.ToChar(letra.ToLower()) == palabraOriginal[i];
        Console.WriteLine($"{letraEsOculta}, {letrasIguales}");
        if (letraEsOculta && letrasIguales)
        {
          nuevaOculta += palabraOriginal[i];
        }
        else
        {
          nuevaOculta += oculta[i];
        }
      }

      return nuevaOculta;
    }
  }
}