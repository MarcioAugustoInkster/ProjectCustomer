using ProjectCustomer.Domain.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectCustomer.Models
{
    public class CustomerModel
    {
        public int Codigo { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Cnpj { get; set; }
        
        public string Estado { get; set; }
        public string RamoAtividade { get; set; }
        public int Funcionarios { get; set; }
        public decimal Faturamento { get; set; }
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string TelefoneMovel { get; set; }
        
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }

        public IEnumerable<State> ListaEstados { get; set; }
    }
}
