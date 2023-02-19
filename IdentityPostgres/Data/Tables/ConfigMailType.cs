using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("config_mail_type", Schema = "identity")]
public partial class ConfigMailType
{
    [Key]
    [Column("id")]
    public short Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<ConfigMailTemplate> ConfigMailTemplate { get; } = new List<ConfigMailTemplate>();
}
