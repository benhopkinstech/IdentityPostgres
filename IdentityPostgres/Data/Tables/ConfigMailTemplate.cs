using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class ConfigMailTemplate
{
    public Guid Id { get; set; }

    public Guid MailId { get; set; }

    public short TypeId { get; set; }

    public string ProviderTemplateIdentifier { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ConfigMail Mail { get; set; } = null!;

    public virtual ConfigMailType Type { get; set; } = null!;
}
