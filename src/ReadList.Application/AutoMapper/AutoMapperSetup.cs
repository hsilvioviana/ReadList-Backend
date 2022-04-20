using AutoMapper;

namespace ReadList.Application.AutoMapper
{
    public class AutoMapperSetup
    {
        public static MapperConfiguration RegisterMapping()
        {
            return new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new ViewModelToDomainMappingProfile());
                configuration.AddProfile(new DomainToViewModelMappingProfile());
            });
        }
    }
}