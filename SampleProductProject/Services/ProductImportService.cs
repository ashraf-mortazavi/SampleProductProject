using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using SampleProductProject.Data;
using SampleProductProject.Models;
using SampleProductProject.ViewModels;
using System.Formats.Asn1;
using System.Globalization;

namespace SampleProductProject.Services
{
    public class ProductImportService
    {
        private readonly AppDbContext _context;

        public ProductImportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProductsResult> ImportCsvAsync(int userId, Stream fileStream)
        {
            var result = new UserProductsResult();

            using (var reader = new StreamReader(fileStream))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.IgnoreBlankLines = true;
                csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;

                var importedUserProducts = csv.GetRecords<UserProduct>().ToList();

                var invalidProductTitleOrCode = importedUserProducts
                    .Where(p => string.IsNullOrWhiteSpace(p.Title) || string.IsNullOrWhiteSpace(p.Code))
                    .ToList();


               if (invalidProductTitleOrCode.Any())
               {
                    result.Errors.Add("Missing Title or Code for a product.");      
               }

                var validProductPrices = importedUserProducts
                     .Where(p => p.Price.HasValue && p.Price <= 0)
                     .ToList();

                if (validProductPrices.Any()) 
                {
                    result.Errors.Add($"Invalid Price fond!");
                }

                var userProductsToDic = importedUserProducts.ToDictionary(p => p.Code, p => p);

                var existingUserProductsByCode = await _context.UserProducts
                    .Where(p => p.UserId == userId && userProductsToDic.Keys.Contains(p.Code))
                    .ToListAsync();

                if (existingUserProductsByCode.Any() || existingUserProductsByCode is not null)
                {
                    existingUserProductsByCode.ForEach(dbProduct =>
                    {
                        dbProduct.Title = userProductsToDic[dbProduct.Code].Title;
                        dbProduct.Price = userProductsToDic[dbProduct.Code].Price;
                    });
                }

                else
                {
                    var existingUserProductsByTitle = await _context.UserProducts
                     .Where(p => p.UserId == userId && userProductsToDic.Values.Select(d => d.Title).ToList().Contains(p.Title))
                     .ToListAsync();

                    if (existingUserProductsByTitle.Any() || existingUserProductsByTitle is not null)
                    {
                        result.Warnings.AddRange(existingUserProductsByTitle.Select(t => $"Title '{t}' already exists for this user."));
                    }
                }

                var newProducts = importedUserProducts
                    .Where(p => !existingUserProductsByCode!.Select(c => c.Code).Contains(p.Code))
                    .Select(p =>
                    {
                        p.UserId = userId;
                        return p;
                    })
                    .ToList();

                _context.UserProducts.AddRange(newProducts);
                await _context.SaveChangesAsync();
            }
            
            var hasErrors = result.Errors.Count > 0;
            result.Success = !hasErrors;
            result.Status = hasErrors ? "Failed" : "Success";
        
            return result;
        }
    }
}

