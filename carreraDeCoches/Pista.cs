using System.Text;

namespace CarreraCoches
{
  class Pista
  {
    static private (string Tipo, double Valor) ASFALTO = ("asfalto", 0.8);
    static private (string Tipo, double Valor) TIERRA = ("tierra", 0.6);
    static private (string Tipo, double Valor) NIEVE = ("nieve", 0.05);
    static private (string Tipo, double Valor) AGUA = ("agua", 0.2);
    private (string, double)[] tiposPistas = { ASFALTO, TIERRA, NIEVE, AGUA };
    private int longitudPista;
    private (string, double) tipoPista;

    public Pista()
    {
      Random r = new Random();
      this.longitudPista = r.Next(3, 5) * 100;
      this.tipoPista = tiposPistas[r.Next(0, tiposPistas.Length - 1)];
    }

    public void InfoPista()
    {
      Console.WriteLine(nameof(ASFALTO));
      const int columnWidth = 14;
      var info = new StringBuilder();
      info.AppendLine($"{"Tipo".PadRight(columnWidth)}{"Friccion".PadRight(columnWidth)}{"Longitud".PadRight(columnWidth)}");
      info.AppendLine($"{this.tipoPista.Item1.PadRight(columnWidth)}{this.tipoPista.Item2.ToString().PadRight(columnWidth)}{this.longitudPista + " m".PadRight(columnWidth)}");
      Console.WriteLine(info.ToString());
    }

    public int LongitudPista
    {
      get { return this.longitudPista; }
    }
  }
}