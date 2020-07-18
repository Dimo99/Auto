using Domain.Entities;
using Dtos;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;

namespace Persistance.Finders.ModelFinder
{
    public class ModelFinder
    {
        private AutoDbContext dbContext;

        public ModelFinder(AutoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Exists(int brandId, string name)
        {
            return dbContext.Models.Any(m => m.Name.ToLower() == name.ToLower() && m.BrandId == brandId);
        }

        public IEnumerable<Model> FilterNonExistingModels(IEnumerable<Model> models)
        {
            var modelNamesAndBrandIds = dbContext.Models.Select(m => new { Name = m.Name.ToLower(), BrandId = m.BrandId }).ToImmutableHashSet();
            return models.Where(m => !modelNamesAndBrandIds.Contains(new { Name = m.Name.ToLower(), BrandId = m.BrandId }));
        }

        public List<ModelSearchDto> GetAll(SourceEnum source)
        {
            return dbContext
                .Models
                .Select(ModelToSearchDto(source))
                .ToList()
                .Where(m => m.ModelKeys != null)
                .ToList();
        }

        public List<ModelDto> GetModels()
        {
            return dbContext.Models.Select(m => new ModelDto { Name = m.Name, BrandId = m.BrandId }).ToList();
        }

        private static Expression<Func<Model, ModelSearchDto>> ModelToSearchDto(SourceEnum source)
        {
            return m => new ModelSearchDto
            {
                Id = m.Id,
                Name = m.Name,
                BrandName = m.Brand.Name,
                ModelKeys = m
                       .ModelKeys
                       .Where(mk => mk.SourceId == (int)source)
                       .Select(mk => mk.Key)
                       .Concat(m
                                .SubModels
                                .SelectMany(sm => sm
                                                    .ModelKeys
                                                    .Where(mk => mk.SourceId == (int)source)
                                                    .Select(mk => mk.Key))).ToList(),
                BrandKey = m.Brand.BrandKeys.SingleOrDefault(bk => bk.SourceId == (int)source).Key
            };
        }
    }
}
