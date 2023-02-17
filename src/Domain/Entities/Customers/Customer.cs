using CleanCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCRM.Domain.Entities.Customers;

public class Customer : BaseEntity<int>
{
    public string? Name { get; set; }
}
