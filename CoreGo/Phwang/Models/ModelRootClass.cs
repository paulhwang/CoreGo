using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.Models
{
    public class ModelRootClass
    {
        DbContextClass dbContextObject { get; }

        public ModelRootClass()
        {
            this.dbContextObject = new DbContextClass();
        }

            /*
            private AccountModellClass accountModelOject { get; }
            private IEnumerable<AccountClass> accountsData { get; }

            public ModelRootClass()
            {
                return;
                this.accountModelOject = new Models.AccountModellClass();

                AccountClass account = new AccountClass();
                account.SetAccountId("phwang");
                this.accountModelOject.AccountOperation.Create(account);
                this.accountsData = this.accountModelOject.AccountOperation.Get();
            }
            */
        }
    }
