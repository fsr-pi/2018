using Firma.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using Firma.Mvc.ViewModels;
using System.Collections.Generic;

namespace Firma.Mvc.Controllers
{
  public class ViewLogController : Controller
  {        
    public IActionResult Index()
    {      
      return View();
    }

    public async Task<IActionResult> Show(DateTime dan)
    {
      ViewBag.Dan = dan;
      List<LogEntry> list = new List<LogEntry>();
      string format = dan.ToString("yyyy-MM-dd");
      string filename = Path.Combine(AppContext.BaseDirectory, $"logs/nlog-own-{format}.log");
      if (System.IO.File.Exists(filename))
      {
        String previousEntry = string.Empty;
        using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
        {          
          using (StreamReader reader = new StreamReader(fileStream))
          {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
              if (line.StartsWith(format))
              {
                //počinje novi zapis, starog dodaj u listu
                if (previousEntry != string.Empty)
                {
                  LogEntry logEntry = LogEntry.FromString(previousEntry);
                  list.Add(logEntry);
                }
                previousEntry = line;
              }
              else
              {
                previousEntry += line;
              }
            }
          }
        }
        //dodaj zadnji

        if (previousEntry != string.Empty)
        {
          LogEntry logEntry = LogEntry.FromString(previousEntry);
          list.Add(logEntry);
        }

      }
      return View(list);
    }
  }
}
