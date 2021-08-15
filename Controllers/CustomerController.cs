using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectCustomer.Domain.Entity.DTO;
using ProjectCustomer.Domain.IRepository;
using ProjectCustomer.Domain.Entity;
using ProjectCustomer.Models;
using System;

namespace ProjectCustomer.Controllers
{
    [Route("cliente")]
    public class CustomerController : Controller
    {
        // Cria uma chamada externa através da Injeção de Dependência
        private readonly ICustomerRepository _customerRepository;
        
        // Inicializa a classe com chamadas
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [Route("cadastro")]
        public IActionResult Cadastro()
        {
            // Cria uma lista para ser alimentada
            IEnumerable<CustomerDTO> listaCliente = _customerRepository.ListaClientes();

            // Condicional para retorno de registros
            if (listaCliente != null)
                return View(listaCliente);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adiciona-novo")]
        public IActionResult NovoCliente([FromBody] CustomerModel model)
        {
            // Validação LINQ para buscar por erros de retorno
            //var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
            
            try
            {
                // Verifica script de retorno do formulário
                if (ModelState.IsValid)
                {
                    // Instancia a classe com objetos
                    Customer customer = new Customer()
                    {
                        Nome = model.Nome,
                        Cnpj = model.Cnpj,
                        CodigoPostal = model.CEP,
                        Funcionarios = model.Funcionarios,
                        RamoAtividade = model.RamoAtividade,
                        Faturamento = model.Faturamento,
                        Telefone = model.Telefone,
                        TelefoneMovel = model.TelefoneMovel,
                        Endereco = model.Endereco,
                        Bairro = model.Bairro,
                        Cidade = model.Cidade
                    };

                    // Busca por tal registro de consulta
                    State estado = _customerRepository.BuscaEstadoPorSigla(model.Estado);

                    // Condicional de retorno
                    if (estado == null)
                        return Json(new { success = false, statusText = "Estado não encontrado" });

                    // Atribui o objeto de retorno
                    customer.Estado = estado;

                    bool retorno = _customerRepository.VerificaClienteExistente(model);

                    if (retorno && model.Codigo > 0)
                    {
                        customer.Codigo = model.Codigo;
                        // Realiza a inserção de novos registros na tabela
                        _customerRepository.AlteraCliente(customer);

                        return Json(new { success = true, statusText = "Dados de cliente alterados!" });
                    }
                    else
                    {
                        // Realiza a inserção de novos registros na tabela
                        _customerRepository.CadastraCliente(customer);
                        
                        return Json(new { success = true, statusText = "Dados de cliente salvos!" });
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Retorno de exceção
                return Json(new { success = false, statusText = "Ocorreu um erro:\n" + ex.Message });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        [Route("adiciona-novo")]
        [Route("adiciona-novo/{codigo}")]
        public IActionResult NovoCliente(int codigo)
        {
            // Instancia os objetos da classe
            CustomerModel customer = new CustomerModel();

            // Verifica registro
            if (codigo > 0)
            {
                try
                {
                    // Busca por registro para preencher objetos da classe
                    customer = _customerRepository.BuscaClientePorId(codigo);
                }
                catch (InvalidOperationException ex)
                {
                    // Tratamento de exceção
                    throw new InvalidOperationException(ex.Message);
                }
            }
            customer.ListaEstados = _customerRepository.ListaEstados();

            return PartialView("_CadastroPartial", customer);
        }

        [Route("consulta-cliente")]
        public IActionResult Consulta(int codigo)
        {
            // verifica por registro
            if (codigo > 0)
            {
                try
                {
                    // Parâmetro de busca e retorno
                    Customer customer = _customerRepository.ExibeClienteInfo(codigo);

                    // Condicional de retorno
                    if (customer != null)
                        return View(customer);
                }
                catch (InvalidOperationException ex)
                {
                    // Tratamento de exceção
                    throw new InvalidOperationException(ex.Message);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("deleta-cliente")]
        [Route("deleta-cliente/{codigo}")]
        public IActionResult DeletaCliente(int codigo)
        {
            return PartialView("_DeleteClientePartial", codigo);
        }

        [HttpPost]
        [Route("deleta-confirma")]
        [Route("deleta-confirma/{codigo}")]
        public IActionResult DeleteConfirm([FromForm] int codigo)
        {
            if (codigo > 0)
            {
                CustomerModel cm = _customerRepository.BuscaClientePorId(codigo);

                if (cm != null)
                {
                    _customerRepository.DeletaCliente(codigo);

                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}
