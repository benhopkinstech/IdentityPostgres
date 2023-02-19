using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("config_mail", Schema = "identity")]
[Index("ProviderId", Name = "u_config_mail", IsUnique = true)]
public partial class ConfigMail
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("provider_id")]
    public short ProviderId { get; set; }

    [Column("api_key")]
    [StringLength(256)]
    public string ApiKey { get; set; } = null!;

    [Column("email")]
    [StringLength(256)]
    public string Email { get; set; } = null!;

    [Column("name")]
    [StringLength(70)]
    public string Name { get; set; } = null!;

    [Column("created_on")]
    public DateTime CreatedOn { get; set; }

    [Column("updated_on")]
    public DateTime? UpdatedOn { get; set; }

    [InverseProperty("Mail")]
    public virtual ICollection<Config> Config { get; } = new List<Config>();

    [InverseProperty("Mail")]
    public virtual ICollection<ConfigMailTemplate> ConfigMailTemplate { get; } = new List<ConfigMailTemplate>();

    [ForeignKey("ProviderId")]
    [InverseProperty("ConfigMail")]
    public virtual ConfigMailProvider Provider { get; set; } = null!;
}
