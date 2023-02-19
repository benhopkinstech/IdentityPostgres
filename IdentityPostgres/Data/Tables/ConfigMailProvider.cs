using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("config_mail_provider", Schema = "identity")]
public partial class ConfigMailProvider
{
    [Key]
    [Column("id")]
    public short Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [InverseProperty("Provider")]
    public virtual ConfigMail? ConfigMail { get; set; }
}
