namespace Program
{
  class NumerosPrimos
  {
    static void Main(string[] args)
    {
      int numeroMax = 25;
      int current = 2;

      while (current <= numeroMax)
      {
        bool esPrimo = true;

        for (int divisor = 2; divisor < current && esPrimo; divisor++)
        {
          if (current % divisor == 0)
          {
            esPrimo = false;
          }
        }

        if (esPrimo) Console.Write(current + " ");

        current++;
      }
    }
  }
}