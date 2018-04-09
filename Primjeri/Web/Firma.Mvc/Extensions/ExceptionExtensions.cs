using System;
using System.Text;

namespace Firma.Mvc.Extensions
{
  /// <summary>
  /// Razred sa proširenjima za iznimke
  /// </summary>
  public static class ExceptionExtensions
  {
    /// <summary>
    /// Vraća sadržaj poruka cijele hijerarhije neke iznimke. Za predanu iznimku provjerava se postoji li unutarnja iznimka.
    /// Ako da, poruka unutanje iznimke dodaje se u rezultat te se dalje provjerava postoji li unutarnja iznimka unutarnje iznimke itd...    
    /// </summary>
    /// <param name="exc">Iznimka čija se kompletna hijerarhija poruka treba ispisati</param>
    /// <returns>String formiran od poruka svih unutarnjih iznimki. Poruka svake iznimmke dodana je u novi redak</returns>
    public static string CompleteExceptionMessage(this Exception exc)
    {
      StringBuilder sb = new StringBuilder();
      while (exc != null)
      {
        sb.AppendLine(exc.Message);
        exc = exc.InnerException;
      }
      return sb.ToString();
    }
  }
}
