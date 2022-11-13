



using HashidsNet;

namespace Service.Showcase.Infrastructure.Databases.Showcases.Mapping;

using AutoMapper;
using Infrastructure = Models;

internal class ShowcaseMappingProfile : Profile
{

    public ShowcaseMappingProfile(IHashids hashids)
    {

        Action<IMemberConfigurationExpression<Infrastructure.Showcase, Application.Showcase.Entities.Showcase,
            ICollection<string>>> HostingMemberOptions()
        {
            return opt => opt.MapFrom(e => e.Hostings.Select(x => x.Value));
        }

        Action<IMemberConfigurationExpression<Infrastructure.Showcase, Application.Showcase.Entities.Showcase,
            ICollection<string>>> SectorsOptions()
        {
            return opt => opt.MapFrom(e => e.Sectors.Select(x => x.Value));
        }

        Action<IMemberConfigurationExpression<Infrastructure.Showcase, Application.Showcase.Entities.Showcase,
            IEnumerable<string>>> FeatureMemberOptions()
        {
            return opt => opt.MapFrom(e => e.Features.Select(x => x.Value));
        }
        
        Action<IMemberConfigurationExpression<Infrastructure.Showcase, Application.Showcase.Entities.Showcase, string>> IdMemberOptions()
        {
            return opt => opt.MapFrom(sh => hashids.Encode(sh.Id));
        }


        _ = CreateMap<Infrastructure.Showcase, Application.Showcase.Entities.Showcase>()
            .ForMember(dest => dest.Id, IdMemberOptions())
            .ForMember(dest => dest.Features, FeatureMemberOptions())
            .ForMember(dest => dest.Sectors, SectorsOptions())
            .ForMember(dest => dest.Hostings, HostingMemberOptions());
    }
}