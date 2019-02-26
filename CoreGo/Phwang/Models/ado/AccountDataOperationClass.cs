using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.Models
{
    public class AccountDataOperationClass : DataOperationInterface<AccountClass>
    {
        private string connectionString = @"Server=(localdb)\v11.0;
                                            Integrated Security=true;
                                            AttachDbFileName=" + Environment.CurrentDirectory + @"\GoData.mdf;";

        public IEnumerable<AccountClass> Get()
        {
            System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(this.connectionString);
            System.Data.IDbCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM Accounts");
            cmd.Connection = connection;
            //connection.Open();

            connection.Close();
            return null;

        }

        public void Create(AccountClass account_val)
        {
            System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(this.connectionString);
            System.Data.IDbCommand cmd = new System.Data.SqlClient.SqlCommand(@"INSERT INTO Accounts (AccountId) VALUES (@AccountId)");
            cmd.Connection = connection;
            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountId", account_val.AccountId()));
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(AccountClass item_val)
        {
            throw new NotImplementedException();

        }

        public void Delete(AccountClass item_val)
        {
            throw new NotImplementedException();

        }
    }
}
