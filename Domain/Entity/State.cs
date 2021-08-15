using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCustomer.Domain.Entity
{
    public class State
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }
}