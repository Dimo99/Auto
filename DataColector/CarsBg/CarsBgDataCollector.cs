using Domain.Entities;
using Dtos;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Persistance;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataColector.CarsBg
{
    public partial class CarsBgDataCollector : IDataCollector
    {
        private const string SearchPage = "https://www.cars.bg/?go=cars&search=1&advanced=1&fromhomeu=1&publishedTime=0&filterOrderBy=1&showPrice=0&autotype=1&stateId=1&section=cars&categoryId=0&doorId=0&brandId=0&modelId=0&fuelId=0&gearId=0&yearFrom=&yearTo=&priceFrom=&priceTo=&currencyId=1&man_priceFrom=0&man_priceTo=0&man_currencyId=1&powerFrom=&powerTo=&steering_weel=0&colorId=0&is_carinbg=0&location=&locationId=0&radius=1&offersForD=1&offersForA=1";

        private ScrapingBrowser browser;
        private ILogger logger;
        private ExistingCars existingCars;
        private ExistingBrands existingBrands;
        private ExistingModels existingModels;

        public CarsBgDataCollector(ExistingBrands existingBrands, ExistingModels existingModels, ExistingCars existingCars, ILogger logger)
        {
            this.browser = new ScrapingBrowser();
            this.browser.Encoding = Encoding.UTF8;
            this.existingBrands = existingBrands;
            this.existingModels = existingModels;
            this.existingCars = existingCars;
            this.logger = logger;
        }

        public IEnumerable<Car> GetAllCars(ModelSearchDto model)
        {
#if DEBUG
            Directory.CreateDirectory(@"./CarsBg/" + model.BrandName + "/" + model.Name);
#endif
            CarsBgSearchCriteria carsBgSearchCriteria = new CarsBgSearchCriteria();
            carsBgSearchCriteria.AddModel(model);

            var results = new List<Car>();
            logger.LogDebug("Exploring" + model.BrandName + " " + model.Name);

            while (true)
            {
                // CarsBg bugs and displays the 50 page for any page after 50
                if (carsBgSearchCriteria.Page > 50)
                {
                    break;
                }

                var url = carsBgSearchCriteria.ToString();

                WebPage carsPage = browser.NavigateToPage(new Uri(url));

                HtmlNodeCollection links = carsPage.Html.SelectNodes("//table[@class='tableListResults']//tr[contains(@class, 'odd') or contains(@class, 'even')]/td[2]/a");

                if (links == null || links.Count == 0)
                {
                    break;
                }

                foreach (HtmlNode link in links)
                {
                    string href = link.GetAttributeValue("href");
                    string adUrl = $"https://www.cars.bg/{href}";

                    if (!existingCars.Contains(adUrl) && existingCars.Add(adUrl))
                    {
                        logger.LogDebug("Processing:" + adUrl);
                        Car car = GetCar(model, adUrl);
                        results.Add(car);
                    }
                }

                carsBgSearchCriteria.Page++;
                logger.LogDebug("Exploring page number:" + carsBgSearchCriteria.Page);

            }

            return results;
        }

        public IEnumerable<Brand> GetBrands()
        {
            WebPage searchPage = browser.NavigateToPage(new Uri(SearchPage));

            IEnumerable<HtmlNode> brandOptions = searchPage.Html.CssSelect("select#BrandId option");

            IEnumerable<Brand> brands = brandOptions
                    .Select(BrandFromHtml)
                    .Where(b => b != null);

            return brands;
        }

        public IEnumerable<Model> GetModels(BrandSearchDto brand)
        {
            WebPage modelsPage = browser.NavigateToPage(new Uri($"https://www.cars.bg/?ajax=multimodel&brandId={brand.BrandKey}"));

            IEnumerable<HtmlNode> modelsList = modelsPage.Html.CssSelect("#models_list li");

            Dictionary<string, Model> modelToSeriesModel = new Dictionary<string, Model>();

            return modelsList.Select(input => ModelFromHtml(brand.Id, input, modelToSeriesModel)).Where(m => m != null);
        }

        private Brand BrandFromHtml(HtmlNode option)
        {
            string brandName = option.InnerText.Trim();

            if (existingBrands.Contains(brandName))
            {
                return null;
            }

            if (!existingBrands.Add(brandName))
            {
                return null;
            }

            Brand brand = new Brand
            {
                Name = brandName
            };

            BrandKey brandKey = new BrandKey
            {
                SourceId = (int)SourceEnum.CarsBg,
                Brand = brand,
                Key = option.GetAttributeValue("value")
            };

            brand.BrandKeys.Add(brandKey);

            return brand;
        }

        private Model ModelFromHtml(int brandId, HtmlNode li, Dictionary<string, Model> modelToSeriesModel)
        {
            HtmlNode input = li.CssSelect("input").SingleOrDefault();
            HtmlNode label = li.CssSelect("label").SingleOrDefault();

            if (input == null || label == null)
            {
                return null;
            }

            string modelName = label.InnerText.Trim();

            var modelDto = new ModelDto { BrandId = brandId, Name = modelName };

            if (existingModels.Contains(modelDto))
            {
                return null;
            }

            if (!existingModels.Add(modelDto))
            {
                return null;
            }

            string modelKeyName = input.GetAttributeValue("value");

            Model model = new Model
            {
                BrandId = brandId,
                Name = modelName,
            };

            string dataGroup = input.GetAttributeValue("data-group");

            if (!string.IsNullOrEmpty(dataGroup))
            {
                string[] subModels = dataGroup.Split(',');

                foreach (string subModel in subModels)
                {
                    modelToSeriesModel.Add(subModel, model);
                }
            }

            if (modelToSeriesModel.ContainsKey(modelKeyName))
            {
                Model seriesModel = modelToSeriesModel[modelKeyName];
                model.ParentModel = seriesModel;
                seriesModel.SubModels.Add(model);
            }

            ModelKey modelKey = new ModelKey
            {
                Key = modelKeyName,
                Model = model,
                SourceId = (int)SourceEnum.CarsBg
            };

            model.ModelKeys.Add(modelKey);

            return model;
        }
    }
}
