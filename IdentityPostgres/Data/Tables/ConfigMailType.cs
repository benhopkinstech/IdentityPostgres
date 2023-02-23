using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class ConfigMailType
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;
}
