using MediatR;
using SampleProductProject.Commands;
using SampleProductProject.Services;
using SampleProductProject.ViewModels;

namespace SampleProductProject.Handler
{
    public class ImportUserProductHandler : IRequestHandler<ImportUserProductCommand, UserProductsResult>
    {
        private readonly ProductImportService _importService;

        public ImportUserProductHandler(ProductImportService importService)
        {
            _importService = importService;
        }

        public async Task<UserProductsResult> Handle(ImportUserProductCommand request, CancellationToken cancellationToken)
        {
            using var stream = request.File.OpenReadStream();
            return await _importService.ImportCsvAsync(request.UserId, stream);
        }
    }
}

