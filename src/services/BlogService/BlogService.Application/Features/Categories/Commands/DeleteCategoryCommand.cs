using MediatR;

namespace BlogService.Application.Features.Categories.Commands;

public class DeleteCategoryCommand : IRequest<bool>
{
    public string Id { get; set; }
}