using System;
using System.Collections.Generic;
using System.Net;

namespace IdentityPostgres.Data.Tables;

public partial class AccountLogin
{
    public long Id { get; set; }

    public Guid? AccountId { get; set; }

    public string Email { get; set; } = null!;

    public bool Successful { get; set; }

    public IPAddress? IpAddress { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Account? Account { get; set; }
}
