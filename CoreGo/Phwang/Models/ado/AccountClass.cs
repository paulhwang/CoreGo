using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.Models
{

    public class AccountClass
    {
        private string accountId { get; set; }
        private string passWord { get; }

        public string AccountId() { return this.accountId; }

        public void SetAccountId(string account_id_val)
        {
            this.accountId = account_id_val;
        }
    }
}
