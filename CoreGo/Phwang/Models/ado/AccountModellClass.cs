using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.Models
{
    public class AccountModellClass
    {
        private DataOperationInterface<AccountClass> accountOperation = null;

        public DataOperationInterface<AccountClass> AccountOperation
        {
            get
            {
                if (this.accountOperation == null)
                {
                    this.accountOperation = new AccountDataOperationClass();
                }
                return this.accountOperation;
            }
        }
    }
}
