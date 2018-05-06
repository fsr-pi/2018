using System.Runtime.Serialization;

namespace Firma.Mvc.Controllers.AutoComplete
{
  [DataContract]
  public class IdLabel
  {
    [DataMember(Name = "label")]
    public string Label { get; set; }
    [DataMember(Name = "id")]
    public int Id { get; set; }
    public IdLabel() { }
    public IdLabel(int id, string label)
    {
      Id = id;
      Label = label;
    }
  }
}
