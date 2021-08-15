using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProjectCustomer.Domain.Entity;
using ProjectCustomer.Domain.Entity.DTO;
using ProjectCustomer.Domain.IRepository;
using ProjectCustomer.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjectCustomer.Infra.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        // Faz a injeção de dependência para a classe
        public readonly IConfiguration _configuration;

        // Instancia a classe para utilidade de recursos
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AlteraCliente(Customer customer)
        {
            MySqlTransaction tr = null;
            MySqlConnection conn = null;

            try
            {
                using (conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
                {
                    conn.Open();
                    tr = conn.BeginTransaction();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE customer SET st_name=@st_name, st_operation=@st_operation, ";
                    cmd.CommandText += "st_cnpj=@st_cnpj, it_amount=@it_amount, dc_billing=@dc_billing, ";
                    cmd.CommandText += "st_phone=@st_phone, st_mobile=@st_mobile, st_address=@st_address, ";
                    cmd.CommandText += "st_district=@st_district, st_city=@st_city, cd_country=@cd_country, ";
                    cmd.CommandText += "st_zip_code=@st_zip_code WHERE cd_customer=@cd_customer;";

                    // Cria os atributos para retornar os tipos de dados da tabela
                    cmd.Parameters.Add(new MySqlParameter("@cd_customer", MySqlDbType.Int32) { Value = customer.Codigo });
                    cmd.Parameters.Add(new MySqlParameter("@st_name", MySqlDbType.VarChar) { Value = customer.Nome });
                    cmd.Parameters.Add(new MySqlParameter("@st_operation", MySqlDbType.VarChar) { Value = customer.RamoAtividade });
                    cmd.Parameters.Add(new MySqlParameter("@st_cnpj", MySqlDbType.VarChar) { Value = customer.Cnpj });
                    cmd.Parameters.Add(new MySqlParameter("@it_amount", MySqlDbType.Int32) { Value = customer.Funcionarios });
                    cmd.Parameters.Add(new MySqlParameter("@dc_billing", MySqlDbType.Decimal) { Value = customer.Faturamento });
                    cmd.Parameters.Add(new MySqlParameter("@st_phone", MySqlDbType.VarChar) { Value = customer.Telefone });
                    cmd.Parameters.Add(new MySqlParameter("@st_mobile", MySqlDbType.VarChar) { Value = customer.TelefoneMovel });
                    cmd.Parameters.Add(new MySqlParameter("@st_address", MySqlDbType.VarChar) { Value = customer.Endereco });
                    cmd.Parameters.Add(new MySqlParameter("@st_district", MySqlDbType.VarChar) { Value = customer.Bairro });
                    cmd.Parameters.Add(new MySqlParameter("@st_city", MySqlDbType.VarChar) { Value = customer.Cidade });
                    cmd.Parameters.Add(new MySqlParameter("@cd_country", MySqlDbType.VarChar) { Value = customer.Estado.Sigla });
                    cmd.Parameters.Add(new MySqlParameter("@st_zip_code", MySqlDbType.VarChar) { Value = customer.CodigoPostal });

                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }
            }
            catch (InvalidOperationException ex)
            {
                tr.Rollback();
                throw new InvalidOperationException(ex.Message);
            }   
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public void CadastraCliente(Customer customer)
        {
            // Instancia uma conecção com a Base de dados
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Abre a conecção
                conn.Open();

                // Inicia uma instância para percurso de objetos
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                
                // Cria um comando para instrução de objetos
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO customer ";
                cmd.CommandText += "(st_name, st_operation, st_cnpj, it_amount, dc_billing, st_phone, ";
                cmd.CommandText += "st_mobile, st_address, st_district, st_city, cd_country, st_zip_code) ";
                cmd.CommandText += "VALUES (@st_name, @st_operation, @st_cnpj, @it_amount, @dc_billing, @st_phone, ";
                cmd.CommandText += "@st_mobile, @st_address, @st_district, @st_city, @cd_country, @st_zip_code);";

                // Cria os atributos para retornar os tipos de dados da tabela
                cmd.Parameters.Add(new MySqlParameter("@st_name", MySqlDbType.VarChar) { Value = customer.Nome });
                cmd.Parameters.Add(new MySqlParameter("@st_operation", MySqlDbType.VarChar) { Value = customer.RamoAtividade });
                cmd.Parameters.Add(new MySqlParameter("@st_cnpj", MySqlDbType.VarChar) { Value = customer.Cnpj });
                cmd.Parameters.Add(new MySqlParameter("@it_amount", MySqlDbType.Int32) { Value = customer.Funcionarios });
                cmd.Parameters.Add(new MySqlParameter("@dc_billing", MySqlDbType.Decimal) { Value = customer.Faturamento });
                cmd.Parameters.Add(new MySqlParameter("@st_phone", MySqlDbType.VarChar) { Value = customer.Telefone });
                cmd.Parameters.Add(new MySqlParameter("@st_mobile", MySqlDbType.VarChar) { Value = customer.TelefoneMovel });
                cmd.Parameters.Add(new MySqlParameter("@st_address", MySqlDbType.VarChar) { Value = customer.Endereco });
                cmd.Parameters.Add(new MySqlParameter("@st_district", MySqlDbType.VarChar) { Value = customer.Bairro });
                cmd.Parameters.Add(new MySqlParameter("@st_city", MySqlDbType.VarChar) { Value = customer.Cidade });
                cmd.Parameters.Add(new MySqlParameter("@cd_country", MySqlDbType.VarChar) { Value = customer.Estado.Sigla });
                cmd.Parameters.Add(new MySqlParameter("@st_zip_code", MySqlDbType.VarChar) { Value = customer.CodigoPostal });

                // Retorna o valor consulta
                cmd.ExecuteNonQuery();
                // Fecha a conecção com a Base de dados
                conn.Close();
            }
        }

        public void DeletaCliente(int codigo)
        {
            MySqlConnection conn = null;

            try
            {
                using (conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "DELETE FROM customer WHERE cd_customer=@cd_customer;";
                    
                    // Cria os atributos para retornar os tipos de dados da tabela
                    cmd.Parameters.Add(new MySqlParameter("@cd_customer", MySqlDbType.Int32) { Value = codigo });

                    cmd.ExecuteNonQuery();
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public IEnumerable<CustomerDTO> ListaClientes()
        {
            // Cria uma lista de objetos
            List<CustomerDTO> customer = new List<CustomerDTO>();
            // Retorno condicional
            bool hasRows = false;

            // Inicializa uma instância de comunicação com a Base de Dados
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Abre a conecção
                conn.Open();
                // Inicializa o comando de identificação de objeto
                MySqlCommand cmd = new MySqlCommand("select * from customer", conn);
                // Prepara para ler os dados
                MySqlDataReader dr = cmd.ExecuteReader();

                // Remete a busca os parâmetros dos objetos
                while (dr.Read())
                {
                    // Instancia a classe de objetos
                    CustomerDTO ctmData = new CustomerDTO()
                    {
                        Codigo = Convert.ToInt32(dr["cd_customer"]),
                        Nome = dr["st_name"].ToString(),
                        Cnpj = dr["st_cnpj"].ToString(),
                        RamoAtividade = dr["st_operation"].ToString(),
                        TelMovel = dr["st_mobile"].ToString()
                    };
                    // Insere na lista da classe de objetos
                    customer.Add(ctmData);
                }

                // Condicional do retorno dos registros
                if (dr.HasRows && customer.Count > 0)
                    hasRows = true;
                
                // Fecha a conecção com a Base de dados
                conn.Close();
            }

            // Condicional de retorno da classe
            if (hasRows)
                return customer;

            return null;
        }

        public CustomerModel BuscaClientePorId(int codigo)
        {
            // Inicializa a instância de comunicação com a Base de dados
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Abre a conecção com Banco
                conn.Open();
                // Inicia um comando para buscar os objetos da tabela por identidade
                MySqlCommand cmd = new MySqlCommand("select * from customer where cd_customer=@cd_customer", conn);
                // Os atributos são definidos com tipos
                cmd.Parameters.Add("@cd_customer", MySqlDbType.Int32).Value = codigo;
                // Prepara o retorno dos objetos
                MySqlDataReader dr = cmd.ExecuteReader();

                // Faz leitura dos registros percorridos
                if (dr.Read())
                {
                    // Instancia e alimenta os objetos da classe
                    CustomerModel model = new CustomerModel()
                    {
                        Codigo = Convert.ToInt32(dr["cd_customer"]),
                        Nome = dr["st_name"].ToString(),
                        Cnpj = dr["st_cnpj"].ToString(),
                        RamoAtividade = dr["st_operation"].ToString(),
                        Funcionarios = Convert.ToInt32(dr["it_amount"]),
                        Faturamento = Convert.ToDecimal(dr["dc_billing"]),
                        Telefone = dr["st_phone"].ToString(),
                        TelefoneMovel = dr["st_mobile"].ToString(),
                        CEP = dr["st_zip_code"].ToString(),
                        Endereco = dr["st_address"].ToString(),
                        Bairro = dr["st_district"].ToString(),
                        Cidade = dr["st_city"].ToString(),
                        Estado = dr["cd_country"].ToString()
                    };

                    // Encerra a conecção com Banco e retorna para a classe
                    conn.Close();
                    return model;
                }
            }
            return null;
        }

        public Customer ExibeClienteInfo(int codigo)
        {
            // Instancia a classe de conecção com o Banco de dados
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Abre a conecção
                conn.Open();
                // Executa o comando de chamada da tabela
                MySqlCommand cmd = new MySqlCommand("select * from customer where cd_customer=@cd_customer", conn);
                // Define os objetos e os tipos de atributos
                cmd.Parameters.Add("@cd_customer", MySqlDbType.Int32).Value = codigo;
                // Prepara para leitura da identidade
                MySqlDataReader dr = cmd.ExecuteReader();

                // Retorna leitura de objetos
                if (dr.Read())
                {
                    // Instancia os objetos da classe
                    Customer model = new Customer()
                    {
                        Codigo = Convert.ToInt32(dr["cd_customer"]),
                        Nome = dr["st_name"].ToString(),
                        Cnpj = dr["st_cnpj"].ToString(),
                        RamoAtividade = dr["st_operation"].ToString(),
                        Funcionarios = Convert.ToInt32(dr["it_amount"]),
                        Faturamento = Convert.ToDecimal(dr["dc_billing"]),
                        Telefone = dr["st_phone"].ToString(),
                        TelefoneMovel = dr["st_mobile"].ToString(),
                        CodigoPostal = dr["st_zip_code"].ToString(),
                        Endereco = dr["st_address"].ToString(),
                        Bairro = dr["st_district"].ToString(),
                        Cidade = dr["st_city"].ToString(),
                        Estado = BuscaEstadoPorSigla(dr["cd_country"].ToString())
                    };

                    // Fecha a conecção com Banco e retorna a classe
                    conn.Close();
                    return model;
                }
            }
            return null;
        }

        public State BuscaEstadoPorSigla(string sigla)
        {
            // Cria uma conecção com o Banco, usando a String de conecção
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Inicia a conecção
                conn.Open();
                // Cria os parâmetros de syntaxe para a tabela
                MySqlCommand cmd = new MySqlCommand("select * from country where cd_country=@cd_country", conn);
                // Gera a captura de identidade da tabela
                cmd.Parameters.Add("@cd_country", MySqlDbType.VarChar).Value = sigla;
                // Prepara para leitura de registros
                MySqlDataReader dr = cmd.ExecuteReader();

                // Inicia busca de registros
                if (dr.Read())
                {
                    // Inicia a classe de objetos
                    State state = new State()
                    {
                        Nome = dr["st_country_description"].ToString(),
                        Sigla = dr["cd_country"].ToString()
                    };
                    // Encerra a instância de conecção
                    conn.Close();
                    // retorna o objeto
                    return state;
                }
            }
            return null;
        }

        public IEnumerable<State> ListaEstados()
        {
            // Cria uma lista de objetos
            List<State> estados = new List<State>();

            // Inicializa uma instância de comunicação com a Base de Dados
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Abre a conecção
                conn.Open();
                // Inicializa o comando de identificação de objeto
                MySqlCommand cmd = new MySqlCommand("select * from country", conn);
                // Prepara para ler os dados
                MySqlDataReader dr = cmd.ExecuteReader();

                // Remete a busca os parâmetros dos objetos
                while (dr.Read())
                {
                    // Instancia a classe de objetos
                    State est = new State()
                    {
                        Sigla = dr["cd_country"].ToString(),
                        Nome = dr["st_country_description"].ToString()
                    };
                    // Insere na lista da classe de objetos
                    estados.Add(est);
                }

                // Fecha a conecção com a Base de dados
                conn.Close();
                
                return estados;
            }
        }

        public bool VerificaClienteExistente(CustomerModel model)
        {
            // Inicializa uma instância de comunicação com a Base de Dados
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("MySql1")))
            {
                // Abre a conecção
                conn.Open();
                // Inicializa o comando de identificação de objeto
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM customer WHERE cd_customer=@cd_customer", conn);
                // Gera a captura de identidade da tabela
                cmd.Parameters.Add("@cd_customer", MySqlDbType.Int32).Value = model.Codigo;
                // Prepara para ler os dados
                MySqlDataReader dr = cmd.ExecuteReader();

                // Remete a busca os parâmetros dos objetos
                if (dr.HasRows)
                {
                    // Fecha a conecção com a Base de dados
                    conn.Close();
                    return true;
                }
                // Fecha a conecção com a Base de dados
                conn.Close();
            }
            return false;
        }
    }
}
