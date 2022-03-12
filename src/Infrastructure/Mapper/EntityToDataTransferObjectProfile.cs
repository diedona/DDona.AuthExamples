using AutoMapper;
using Domain.DataTransferObjects.User;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class EntityToDataTransferObjectProfile : Profile
    {
        public EntityToDataTransferObjectProfile()
        {
            CreateMap<UserEntity, UserAuthorizationDTO>();
        }
    }
}
