using System.Text;

namespace CarreraCoches
{
  class Coche
  {
    private int velocidadMaxima;
    private string color;
    private string nombre;
    private string modelo;
    static private int falsoContador = 1;

    public Coche()
    {
      this.nombre = "Coche " + falsoContador;
      this.modelo = "Modelo " + falsoContador;
      this.color = "white";
      this.velocidadMaxima = ImpostarVelocidadMaxima();
      Coche.falsoContador++;
    }

    public Coche(string nombre, string modelo)
    {
      this.nombre = nombre;
      this.modelo = modelo;
      this.color = "white";
      this.velocidadMaxima = ImpostarVelocidadMaxima();
    }

    public string Nombre
    {
      get { return this.nombre; }
    }

    public int VelocidadMaxima
    {
      get { return this.velocidadMaxima; }
    }

    private int ImpostarVelocidadMaxima()
    {
      Random r = new Random();
      velocidadMaxima = r.Next(100, 150);
      return velocidadMaxima;
    }

    public void InfoCoche()
    {
      const int columnWidth = 14;
      var info = new StringBuilder();
      //info.AppendLine($"{"Nombre".PadRight(columnWidth)}{"Modelo".PadRight(columnWidth)}{"Color".PadRight(columnWidth)}{"Velocidad Maxima".PadRight(columnWidth)}");
      info.AppendLine($"{this.nombre.PadRight(columnWidth)}{this.modelo.PadRight(columnWidth)}{this.color.PadRight(columnWidth)}{this.velocidadMaxima + " km/h".PadRight(columnWidth)}");
      Console.WriteLine(info.ToString());
    }
  }
}