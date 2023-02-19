using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class Config
{
    public short Id { get; set; }

    public Guid? MailId { get; set; }

    public bool AccountVerificationRequired { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ConfigMail? Mail { get; set; }
}
