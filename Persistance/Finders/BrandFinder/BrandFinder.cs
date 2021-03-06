﻿using Domain.Entities;
using Dtos;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Persistance.Finders.BrandFinder
{
    public class BrandFinder
    {
        private AutoDbContext dbContext;

        public BrandFinder(AutoDbContext autoDbContext)
        {
            this.dbContext = autoDbContext;
        }

        public IEnumerable<Brand> FilterNonExistingBrands(IEnumerable<Brand> brands)
        {
            ImmutableHashSet<string> brandNames = dbContext.Brands.Select(b => b.Name.ToLower()).ToImmutableHashSet();
            return brands
                         .Where(b => b.Name != string.Empty)
                         .Where(b => !b.Name.ToLower().Contains("всички"))
                         .Where(b => !brandNames.Contains(b.Name.ToLower()));
        }

        public List<BrandSearchDto> GetBrandSearchDtos(SourceEnum source)
        {
            return dbContext.Brands
                    .Select(b => new BrandSearchDto
                    {
                        Id = b.Id,
                        BrandKey = b.BrandKeys.Single(bk => bk.SourceId == (int)source).Key
                    })
                    .OrderBy(b => b.Id)
                    .ToList();
        }

        public List<string> GetBrandNames()
        {
            return dbContext.Brands.OrderBy(b => b.Id).Select(b => b.Name).ToList();
        }
    }
}
