using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CorporatePortalAPI.Models;

public partial class Entry
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int Hours { get; set; }

    public string? Description { get; set; }

    public int TaskId { get; set; }
    public virtual Task Task { get; set; } = null!;
}
