using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.Data.DTO
{

    public class AlunosDTO
    {
        
        public long Id { get; set; }

       
        public string Nome { get; set; }

        
        public string Email { get; set; }

      
        public string CPF { get; set; }

        public string Cidade { get; set; }

        public int Idade { get; set; }
    }
}
