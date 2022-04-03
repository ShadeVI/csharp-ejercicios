using System.Text;

namespace CarreraCoches
{
  class Pista
  {
    private int longitudPista;
    private string tipoPista;

    private string[] tiposPistas = { "asfalto", "tierra", "arena" };

    public Pista()
    {
      Random r = new Random();
      this.longitudPista = r.Next(3, 5) * 100;
      this.tipoPista = tiposPistas[r.Next(0, tiposPistas.Length - 1)];
    }

    public void InfoPista()
    {
      const int columnWidth = 13;
      var info = new StringBuilder();
      info.AppendLine($"{"Tipo".PadRight(columnWidth)}{"Longitud".PadRight(columnWidth)}");
      info.AppendLine($"{this.tipoPista.PadRight(columnWidth)}{this.longitudPista + " m".PadRight(columnWidth)}");
      Console.WriteLine(info.ToString());
    }

    public int LongitudPista
    {
      get { return this.longitudPista; }
    }
  }
}