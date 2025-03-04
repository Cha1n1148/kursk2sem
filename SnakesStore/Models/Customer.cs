using System;
using System.Collections.Generic;

namespace SnakesStore.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    // Добавляем свойство FullName
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}"; // Возвращаем полное имя клиента
        }
    }
}
