using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectCustomer.Domain.Entity
{
    public class Customer
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public State Estado { get; set; }
        public string RamoAtividade { get; set; }
        public string Cnpj { get; set; }
        public int Funcionarios { get; set; }
        public decimal Faturamento { get; set; }
        public string Telefone { get; set; }
        public string TelefoneMovel { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CodigoPostal { get; set; }
    }
}