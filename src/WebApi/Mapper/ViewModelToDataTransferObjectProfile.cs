using AutoMapper;
using Domain.DataTransferObjects.User;
using WebApi.ViewModels.User;

namespace WebApi.Mapper
{
    public class ViewModelToDataTransferObjectProfile : Profile
    {
        public ViewModelToDataTransferObjectProfile()
        {
            CreateMap<UserLoginRequestViewModel, UserLoginRequestDTO>();
        }
    }
}
