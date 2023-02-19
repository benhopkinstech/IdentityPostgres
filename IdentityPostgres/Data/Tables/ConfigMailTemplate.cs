using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("config_mail_template", Schema = "identity")]
[Index("MailId", "TypeId", Name = "u_config_mail_template", IsUnique = true)]
public partial class ConfigMailTemplate
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("mail_id")]
    public Guid MailId { get; set; }

    [Column("type_id")]
    public short TypeId { get; set; }

    [Column("provider_template_identifier")]
    [StringLength(100)]
    public string ProviderTemplateIdentifier { get; set; } = null!;

    [Column("created_on")]
    public DateTime CreatedOn { get; set; }

    [Column("updated_on")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("MailId")]
    [InverseProperty("ConfigMailTemplate")]
    public virtual ConfigMail Mail { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("ConfigMailTemplate")]
    public virtual ConfigMailType Type { get; set; } = null!;
}
