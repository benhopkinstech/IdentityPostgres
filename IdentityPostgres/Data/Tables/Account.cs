using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("account", Schema = "identity")]
[Index("ProviderId", "Email", Name = "u_account", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("provider_id")]
    public short ProviderId { get; set; }

    [Column("email")]
    [StringLength(256)]
    public string Email { get; set; } = null!;

    [Column("verified")]
    public bool Verified { get; set; }

    [Column("verified_on")]
    public DateTime? VerifiedOn { get; set; }

    [Column("created_on")]
    public DateTime CreatedOn { get; set; }

    [Column("updated_on")]
    public DateTime? UpdatedOn { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<AccountLogin> AccountLogin { get; } = new List<AccountLogin>();

    [InverseProperty("Account")]
    public virtual AccountPassword? AccountPassword { get; set; }

    [ForeignKey("ProviderId")]
    [InverseProperty("Account")]
    public virtual AccountProvider Provider { get; set; } = null!;
}
