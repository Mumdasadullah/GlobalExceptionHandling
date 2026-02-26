using System;
using System.Collections.Generic;

namespace InMemoryDBSpecificationRepositoryUOWProject.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; }

    public Guid UserId { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime CreatedDate { get; set; }
}
