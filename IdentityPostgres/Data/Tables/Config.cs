using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("config", Schema = "identity")]
public partial class Config
{
    [Key]
    [Column("id")]
    public short Id { get; set; }

    [Column("mail_id")]
    public Guid? MailId { get; set; }

    [Column("account_verification_required")]
    public bool AccountVerificationRequired { get; set; }

    [Column("updated_on")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("MailId")]
    [InverseProperty("Config")]
    public virtual ConfigMail? Mail { get; set; }
}
