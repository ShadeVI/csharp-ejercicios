using System.Text;

namespace CarreraCoches
{
  class Coche
  {
    private int velocidadMaxima;
    private string color;
    private string nombre;
    private string modelo;
    private int peso;
    private (string, int)[] componentes = { ("motor", 3), ("ruedas", 30), ("arbol de trasmision", 15), ("caja de cambio", 10) };
    private (string, int) componenteEnAveria;
    private bool averia;
    static private int falsoContador = 1;

    public Coche()
    {
      Random r = new Random();
      this.nombre = "Coche " + falsoContador;
      this.modelo = "Modelo " + falsoContador;
      this.color = ImpostarColor(r);
      this.peso = ImpostarPeso(r);
      this.velocidadMaxima = ImpostarVelocidadMaxima(r);
      this.averia = false;
      Coche.falsoContador++;
    }

    public Coche(string nombre, string modelo)
    {
      Random r = new Random();
      this.nombre = nombre;
      this.modelo = modelo;
      this.color = ImpostarColor(r);
      this.peso = ImpostarPeso(r);
      this.velocidadMaxima = ImpostarVelocidadMaxima(r);
      this.averia = false;
      Coche.falsoContador++;
    }

    public string Nombre
    {
      get { return this.nombre; }
    }

    public string Color
    {
      get { return this.color; }
    }
    public int Peso
    {
      get { return this.peso; }
    }

    public int VelocidadMaxima
    {
      get { return this.velocidadMaxima; }
    }
    public bool Averia
    {
      get { return this.averia; }
    }

    public (string, int) ComponenteEnAveria
    {
      get { return this.componenteEnAveria; }
    }

    private int ImpostarVelocidadMaxima(Random r)
    {
      velocidadMaxima = r.Next(100, 350);
      return velocidadMaxima;
    }

    private int ImpostarPeso(Random r)
    {
      int peso = r.Next(1000, 3000);
      return peso;
    }

    private string ImpostarColor(Random r)
    {
      ConsoleColor[] colores = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
      return colores[r.Next(0, colores.Length - 1)].ToString();
    }

    public bool RandomizarAveria()
    {
      Random r = new Random();
      int rIndex = r.Next(0, componentes.Length);
      if (rIndex >= 0 && rIndex <= componentes.Length - 1)
      {
        (string, int) componente = componentes[rIndex];
        int probabilidadAveria = r.Next(0, 1000);
        if (probabilidadAveria <= componente.Item2)
        {
          this.componenteEnAveria = componente;
          this.averia = true;
          return true;
        }
      }
      return false;
    }

    public void InfoCoche()
    {
      const int columnWidth = 14;
      var info = new StringBuilder();
      //info.AppendLine($"{"Nombre".PadRight(columnWidth)}{"Modelo".PadRight(columnWidth)}{"Color".PadRight(columnWidth)}{"Velocidad Maxima".PadRight(columnWidth)}");
      info.AppendLine($"{this.nombre.PadRight(columnWidth)}{this.modelo.PadRight(columnWidth)}{this.color.PadRight(columnWidth)}{this.peso + " Kg".PadRight(columnWidth)}{this.velocidadMaxima + " km/h".PadRight(columnWidth)}");
      Console.WriteLine(info.ToString());
    }
  }
}