using AutoMapper;
using Demo22.Mappings;
using Microsoft.Extensions.Logging.Abstractions;

namespace Demo22.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, NullLoggerFactory.Instance);
            Mapper = config.CreateMapper();
        }
    }
}