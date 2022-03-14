namespace Program
{
  class FrasePAC
  {
    static void Main(string[] args)
    {
      string frase_PAC = "Esta es la frase PAC de prueba, con dígitos 12394 y mas dígitos 198 138 1387 y símbolos también # € % . AIE Á ÈÈÙ";

      string VOCALES = "AEIOUÁÉÍÓÚÀÈÌÒÙ";

      string frase_PAC_Mayuscula = frase_PAC.ToUpper();

      int letrasTotales = frase_PAC.Length;

      int vocalesTotales = 0;
      string soloVocales = "";

      // ------ 1) USANDO FOREACH Y METODO LINQ CONTAINS ------
      foreach (char letra in frase_PAC)
      {
        if (VOCALES.Contains(letra) || VOCALES.ToLower().Contains(letra))
        {
          vocalesTotales++;
          soloVocales += letra;
        }
        else soloVocales += " ";
      }

      /* ------ 2) USANDO FOR ANIDADOS ------

      bool vocalAnadida = false;
      for (int letraIdx = 0; letraIdx < frase_PAC.Length; letraIdx++)
      {
        vocalAnadida = false;
        for (int vocalIdx = 0; vocalIdx < VOCALES.Length; vocalIdx++)
        {
          if (VOCALES[vocalIdx] == frase_PAC_Mayuscula[letraIdx])
          {
            vocalesTotales++;
            soloVocales += frase_PAC[letraIdx];
            vocalAnadida = true;
          }
        }
        if (!vocalAnadida) soloVocales += " ";
      } */

      /* ------ 3) USANDO FOREACH ANIDADOS ------

      bool vocalAnadida = false;
      foreach (char letra in frase_PAC)
      {
        vocalAnadida = false;
        foreach (char vocal in VOCALES)
        {
          if (Convert.ToString(letra).ToUpper() == Convert.ToString(vocal))
          {
            vocalesTotales++;
            soloVocales += letra;
          }
        }
        if (!vocalAnadida) soloVocales += " ";
      }
*/

      Console.WriteLine($"Totales letras { letrasTotales}\n");
      Console.WriteLine($"Totales vocales { vocalesTotales}\n");
      Console.WriteLine($"En MAYUSCULAS { frase_PAC_Mayuscula}\n");
      Console.WriteLine($"Solo Vocales { soloVocales.Replace(' ', '\0')}\n");
      Console.WriteLine($"Solo Vocales { soloVocales}\n");
    }
  }
}