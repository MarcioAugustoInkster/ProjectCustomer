using ProjectCustomer.Domain.Entity;
using ProjectCustomer.Domain.Entity.DTO;
using ProjectCustomer.Models;
using System.Collections.Generic;

namespace ProjectCustomer.Domain.IRepository
{
    public interface ICustomerRepository
    {
        void CadastraCliente(Customer customer);
        
        void AlteraCliente(Customer customer);
        
        void DeletaCliente(int codigo);
        
        IEnumerable<CustomerDTO> ListaClientes();

        IEnumerable<State> ListaEstados();

        CustomerModel BuscaClientePorId(int id);

        Customer ExibeClienteInfo(int id);

        State BuscaEstadoPorSigla(string sigla);

        bool VerificaClienteExistente(CustomerModel model);
    }
}
