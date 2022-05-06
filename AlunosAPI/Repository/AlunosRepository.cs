using AlunosAPI.Config;
using AlunosAPI.Data.Context;
using AlunosAPI.Data.DTO;
using AlunosAPI.Model;
using AlunosAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AlunosAPI.Repository
{
    public class AlunosRepository : IAlunosRespository
    {
        private readonly AlunosDbContext _context;
        private readonly IMapper _mapper;

        public AlunosRepository(AlunosDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AlunosDTO> ObterAlunoId(long id)
        {
            Alunos aluno = await _context.Alunos.Where(a => a.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<AlunosDTO>(aluno);
        }

        public async Task<IEnumerable<AlunosDTO>> ObterTodosAlunos()
        {
            List<Alunos> alunos = await _context.Alunos.AsNoTracking().ToListAsync();
            return _mapper.Map<List<AlunosDTO>>(alunos);
        }
        public async Task<AlunosDTO> InscreverAluno(AlunosDTO alunos)
        {
            Alunos aluno = _mapper.Map<Alunos>(alunos);

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            return _mapper.Map<AlunosDTO>(aluno);
        }

        public async Task<AlunosDTO> AtualizarDadosAluno(AlunosDTO alunos)
        {
            Alunos aluno = _mapper.Map<Alunos>(alunos);

             _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return _mapper.Map<AlunosDTO>(aluno);
        }

        public async Task<bool> DeletarAluno(long id)
        {
           var result = await _context.Alunos.Where(a => a.Id == id).AsNoTracking().FirstOrDefaultAsync();

            if (result == null)
                return false;

            _context.Alunos.Remove(result);
            await _context.SaveChangesAsync();

            return true;

        }

       

       
    }
}
