using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class AccountProvider
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Account { get; } = new List<Account>();
}
