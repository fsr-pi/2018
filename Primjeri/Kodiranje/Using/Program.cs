using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Using
{
	class Program
	{
		static void Main(string[] args)
		{			
			try
			{
				Razred r1 = new Razred("A1");

				using (Razred r2 = new Razred("B2"))
				using (Razred r4 = new Razred("D4")) 
				{
					Razred r3 = new Razred("C3");					    
          throw new Exception("Poruka");
				}        
				r1.Dispose();
			}
			catch (Exception exc)
			{
				Console.WriteLine("Dogodila se pogreska: " +  exc.Message);
			}
			
		}
	}
}
