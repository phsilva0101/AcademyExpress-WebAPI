using AlunosAPI.Data.DTO;
using AlunosAPI.Model;
using AlunosAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunosRespository _repos;

        public AlunosController(IAlunosRespository repos)
        {
            _repos = repos ?? throw new ArgumentNullException(nameof(repos));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Alunos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<AlunosDTO>>> ObterTodosAlunos()
        {
            try
            {
                var resul = await _repos.ObterTodosAlunos();
                if (resul == null)
                    return NotFound("Nenhum aluno cadastrado para ser listado!");

                return Ok(resul);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}", Name = "ObterAlunosID")]
        [ProducesResponseType(typeof(Alunos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlunosDTO>> ObterAlunosID(long id)
        {
            try
            {
                var resul = await _repos.ObterAlunoId( id);
                if (resul == null)
                    return NotFound($"Nenhum aluno com o ID:{id} encontrado!");

                return Ok(resul);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(Alunos), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlunosDTO>> InscreverAluno([FromBody] AlunosDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Não foi possivel inserir o aluno!");

                var resul = await _repos.InscreverAluno(dto);
                return CreatedAtRoute(nameof(ObterAlunosID), new { id = dto.Id}, dto);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(Alunos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AlunosDTO>> AtualizarDadosAluno(AlunosDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Não foi possivel atualizar os dados do aluno!");

                var resul = await _repos.AtualizarDadosAluno(dto);
                return Ok(resul);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Alunos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ExcluirAluno(long id)
        {
            try
            {

                var status = await _repos.DeletarAluno(id);
                if (!status)
                    return BadRequest();

                return Ok(status);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
