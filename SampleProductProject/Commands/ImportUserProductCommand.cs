using MediatR;
using SampleProductProject.ViewModels;

namespace SampleProductProject.Commands
{
    public record ImportUserProductCommand(int UserId, IFormFile File): IRequest<UserProductsResult>;
    
}
