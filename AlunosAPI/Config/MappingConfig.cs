using AlunosAPI.Data.DTO;
using AlunosAPI.Model;
using AutoMapper;

namespace AlunosAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AlunosDTO, Alunos>();
                config.CreateMap<Alunos, AlunosDTO>();
            });
            return mappingConfig;
        }
    }
}
