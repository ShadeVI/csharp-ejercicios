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

        for (int divisor = 2; divisor < current; divisor++)
        {
          if (current % divisor == 0)
          {
            esPrimo = false;
            break;
          }
        }
        if (esPrimo) Console.Write(current + " ");

        current++;
      }
    }
  }
}