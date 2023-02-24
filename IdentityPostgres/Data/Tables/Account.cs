using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class Account
{
    public Guid Id { get; set; }

    public short ProviderId { get; set; }

    public string Email { get; set; } = null!;

    public bool Verified { get; set; }

    public DateTime? VerifiedOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<AccountLogin> AccountLogin { get; } = new List<AccountLogin>();

    public virtual AccountPassword? AccountPassword { get; set; }

    public virtual ICollection<AccountVerification> AccountVerification { get; } = new List<AccountVerification>();

    public virtual AccountProvider Provider { get; set; } = null!;
}
