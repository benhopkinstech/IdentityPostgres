using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class ConfigMailProvider
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ConfigMail? ConfigMail { get; set; }
}
