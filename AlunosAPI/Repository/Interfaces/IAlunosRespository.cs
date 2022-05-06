using AlunosAPI.Data.DTO;
using AlunosAPI.Model;
using System.Collections.Generic;

namespace AlunosAPI.Repository.Interfaces
{
    public interface IAlunosRespository
    {
        public Task<IEnumerable<AlunosDTO>> ObterTodosAlunos();
        public Task<AlunosDTO> ObterAlunoId(long id);
        public Task<AlunosDTO> InscreverAluno(AlunosDTO alunos);
        public Task<AlunosDTO> AtualizarDadosAluno(AlunosDTO alunos);
        public Task<bool> DeletarAluno(long id);

    }
}
