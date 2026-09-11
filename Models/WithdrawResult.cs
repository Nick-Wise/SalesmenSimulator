using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesmenSimulator.Models
{
    public class WithdrawResult(bool success, decimal newBalance)
    {
        public bool Success = success;
        public decimal NewBalance = newBalance;
    }
}