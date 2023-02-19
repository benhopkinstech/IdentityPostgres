using System;
using System.Collections.Generic;

namespace IdentityPostgres.Data.Tables;

public partial class ConfigMail
{
    public Guid Id { get; set; }

    public short ProviderId { get; set; }

    public string ApiKey { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<Config> Config { get; } = new List<Config>();

    public virtual ICollection<ConfigMailTemplate> ConfigMailTemplate { get; } = new List<ConfigMailTemplate>();

    public virtual ConfigMailProvider Provider { get; set; } = null!;
}
