using System;
using System.Collections.Generic;

namespace Theatre.Models.Theatre;

public partial class Spectacle
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string Lieu { get; set; } = null!;

    public DateTime Date { get; set; }

    public int ActorId { get; set; }

    public virtual Actor Actor { get; set; } = null!;
}
