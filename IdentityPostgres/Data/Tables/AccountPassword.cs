using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("account_password", Schema = "identity")]
public partial class AccountPassword
{
    [Key]
    [Column("account_id")]
    public Guid AccountId { get; set; }

    [Column("hash")]
    [StringLength(60)]
    public string Hash { get; set; } = null!;

    [Column("updated_on")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountPassword")]
    public virtual Account Account { get; set; } = null!;
}
