using System;

namespace ProjectCustomer.Domain.Entity.DTO
{
    public class CustomerDTO
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string RamoAtividade { get; set; }
        public string Cnpj { get; set; }
        public string TelMovel { get; set; }
    }
}