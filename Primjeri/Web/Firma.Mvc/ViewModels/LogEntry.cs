using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firma.Mvc.ViewModels
{
  public class LogEntry
  {
    public DateTime Time { get; set; }
    public int Id { get; set; }
    public string Controller { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public string Url { get; set; }
    public string Action { get; set; }

    internal static LogEntry FromString(string text)
    {
      string[] arr = text.Split('|');
      LogEntry entry = new LogEntry();
      entry.Time = DateTime.ParseExact(arr[0], "yyyy-MM-dd HH:mm:ss.ffff",System.Globalization.CultureInfo.InvariantCulture);      
      entry.Id = string.IsNullOrWhiteSpace(arr[1]) ? 0 : int.Parse(arr[1]);
      entry.Controller = arr[2];
      entry.Level = arr[3];
      entry.Message = arr[4];
      entry.Url = arr[5].Substring(5); //url: 
      entry.Action = arr[6].Substring(8); //action: 
      return entry;
    }
  }
}
