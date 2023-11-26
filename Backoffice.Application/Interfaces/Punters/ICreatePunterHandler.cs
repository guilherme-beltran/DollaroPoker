using Backoffice.Application.UseCases.Punters.Create;
using Backoffice.Domain.Shared;

namespace Backoffice.Application.Interfaces.Punters;

public interface ICreatePunterHandler
{
    Task<Response> Handle(CreatePunterCommand request, CancellationToken cancellationToken);
}
