using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteAPIConsumer.Repositorio
{
    public class RepositorioCliente
    {
        private readonly string conexao;
        public RepositorioCliente()
        {
            conexao = Environment.GetEnvironmentVariable("dbCliente");
        }

        public int Inserir(Model.Cliente cliente)
        {
            string sql = "INSERT INTO Cliente(Id,Nome) Values(@Id, @nome)";
            int count = 0;
            using (var connection = new SqlConnection(conexao))
            {
                try
                {
                    connection.Open();
                    count = connection.Execute(sql, cliente);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
                return count;
            }

        }


    }
    
}
