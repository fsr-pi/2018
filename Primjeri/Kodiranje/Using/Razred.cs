using System;

namespace Using
{
  public class Razred : IDisposable
	{
		public string Id { get; set; }
		public void Dispose()
		{
			//zatvaranje datoteke, konekcije i sličnih "dragocjenih" resursa
			Console.WriteLine("**  {0} : Dispose **", Id);
		}

		public Razred(string id)
		{
			Id = id;
			Console.WriteLine("----> {0} : Kreiran", Id);
		}		
	}
}
