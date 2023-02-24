using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class AccountVerification
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Account Account { get; set; } = null!;
}
