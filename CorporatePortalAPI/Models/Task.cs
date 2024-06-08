using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CorporatePortalAPI.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ProjectId { get; set; }

    public bool IsActive { get; set; }
    [JsonIgnore]
    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
    [JsonIgnore]
    public virtual Project Project { get; set; } = null!;
}
