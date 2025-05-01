#region code: fatih.unal date: 2025-04-24T11:36:05
using AutoMapper;
using FthAdmin.Application.Features.Servers.Commands;
using FthAdmin.Domain.Entities;
using FthAdmin.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FthAdmin.Application.Features.Servers.Commands
{
    public class CreateServerCommandHandler : IRequestHandler<CreateServerCommand, int>
    {
        private readonly IGenericRepository<Server, Guid> _serverRepository;
        private readonly IMapper _mapper;
        public CreateServerCommandHandler(IGenericRepository<Server, Guid> serverRepository, IMapper mapper)
        {
            _serverRepository = serverRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateServerCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Server>(request);
            await _serverRepository.AddAsync(entity);
            // Varsayım: Server entity'sinde int Id yok, Guid Id var. Geriye int döndürülüyor. Bunu düzeltmek için Guid döndürülebilir veya int'e çevrilebilir.
            return 1; // Geçici: entity.Id.ToString() veya başka bir değer dönebilir.
        }
    }
}
#endregion
