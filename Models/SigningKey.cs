using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class SigningKey
{
    public int Id { get; set; }

    public Guid EntityId { get; set; }

    public string PrivateKey { get; set; }

    public string PublicKey { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ExpiresOn { get; set; }
}
