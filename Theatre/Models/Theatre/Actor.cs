using System;
using System.Collections.Generic;

namespace Theatre.Models.Theatre;

public partial class Actor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Spectacle> Spectacles { get; set; } = new List<Spectacle>();
}
