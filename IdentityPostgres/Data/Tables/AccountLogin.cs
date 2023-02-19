using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace IdentityPostgres.Data.Tables;

[Table("account_login", Schema = "identity")]
public partial class AccountLogin
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("account_id")]
    public Guid? AccountId { get; set; }

    [Column("email")]
    [StringLength(256)]
    public string Email { get; set; } = null!;

    [Column("successful")]
    public bool Successful { get; set; }

    [Column("ip_address")]
    public IPAddress? IpAddress { get; set; }

    [Column("created_on")]
    public DateTime CreatedOn { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountLogin")]
    public virtual Account? Account { get; set; }
}
