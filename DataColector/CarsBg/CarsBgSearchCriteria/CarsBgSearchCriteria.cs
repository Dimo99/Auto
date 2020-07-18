using Common;
using DataColector.CarsBg.Enums;
using Dtos;
using Resources;
using System;

namespace DataColector.CarsBg
{
    public class CarsBgSearchCriteria
    {
        private ParametersDictionary parameters = new ParametersDictionary();

        private string section;

        private bool isSearchResult;

        private bool offerFromPrivatePerson;
        private bool offerFromCarDealership;
        private bool offerFromOfficialImporters;

        private CarsBgCoupeCategory coupeCategory;
        private CarsBgNumberOfDoors numberOfDoors;
        private CarsBgFuelType fuelType;
        private CarsBgTransmission transmission;
        private CarsBgSortBy sortBy;

        private int? yearFrom;
        private int? yearTo;

        private int? priceFrom;
        private int? priceTo;

        private int page;

        public CarsBgSearchCriteria()
        {
            this.Section = "cars";

            parameters["fromhomeu"] = "1";
            parameters["publishedtime"] = "0";
            parameters["showPrice"] = "1";
            parameters["autotype"] = "1";
            parameters["currencyId"] = "1";
            parameters["locationId"] = "0";
            parameters["radius"] = "1";
            parameters["location"] = string.Empty;

            this.IsSearchResult = true;
            this.OfferFromPrivatePerson = true;
            this.OfferFromCarDealership = true;
            this.OfferFromOfficialImporters = true;
            this.CoupeCategory = CarsBgCoupeCategory.All;
            this.NumberOfDoors = CarsBgNumberOfDoors.All;
            this.FuelType = CarsBgFuelType.All;
            this.Transmission = CarsBgTransmission.All;
            this.SortBy = CarsBgSortBy.Newest;
            this.YearFrom = null;
            this.YearTo = null;
            this.PriceFrom = null;
            this.PriceTo = null;
            this.Page = 1;
        }

        public bool IsSearchResult
        {
            get { return this.isSearchResult; }

            set
            {
                parameters["search"] = BoolToString(value);
                parameters["advanced"] = BoolToString(!value);

                this.isSearchResult = value;
            }
        }

        private string Section
        {
            get { return this.section; }

            set
            {
                if (value != "cars")
                {
                    throw new ArgumentException(AutoResourceFile.CurrentlyOnlyCarsSearchIsSupported);
                }

                parameters["go"] = value;
                parameters["section"] = value;

                this.section = value;
            }
        }

        public bool OfferFromPrivatePerson
        {
            get { return this.offerFromPrivatePerson; }

            set
            {
                parameters["offerFromD"] = BoolToString(value);
                this.offerFromPrivatePerson = value;
            }
        }

        public bool OfferFromCarDealership
        {
            get { return this.offerFromCarDealership; }

            set
            {
                parameters["offerFromA"] = BoolToString(value);
                this.offerFromCarDealership = value;
            }
        }

        public bool OfferFromOfficialImporters
        {
            get { return this.offerFromOfficialImporters; }

            set
            {
                parameters["offerFromB"] = BoolToString(value);
                this.offerFromOfficialImporters = value;
            }
        }

        public CarsBgCoupeCategory CoupeCategory
        {
            get { return this.coupeCategory; }

            set
            {
                parameters["categoryId"] = ((int)value).ToString();
                this.coupeCategory = value;
            }
        }

        public CarsBgNumberOfDoors NumberOfDoors
        {
            get { return this.numberOfDoors; }

            set
            {
                parameters["doorId"] = ((int)value).ToString();
                this.numberOfDoors = value;
            }
        }

        public void AddModel(ModelSearchDto model)
        {
            if (parameters.ContainsKey("brandId") && model.BrandKey != parameters["brandId"])
            {
                throw new ArgumentException(
                    AutoResourceFile.OnlyModelsUnderTheSameBrandCanBeUsedForSearchCriteria);
            }

            parameters["modelId"] = "0";

            parameters.Add("models[]", model.ModelKeys);
        }

        public CarsBgFuelType FuelType
        {
            get { return this.fuelType; }

            set
            {
                parameters["fuelId"] = ((int)value).ToString();
                this.fuelType = value;
            }
        }

        public CarsBgTransmission Transmission
        {
            get { return this.transmission; }

            set
            {
                parameters["gearId"] = ((int)value).ToString();
                this.transmission = value;
            }
        }

        public CarsBgSortBy SortBy
        {
            get { return this.sortBy; }

            set
            {
                parameters["filterOrderBy"] = ((int)value).ToString();
                this.sortBy = value;
            }
        }

        public int? YearFrom
        {
            get { return this.yearFrom; }

            set
            {
                parameters["yearFrom"] = value != null ? value.ToString() : string.Empty;
                this.yearFrom = value;
            }
        }

        public int? YearTo
        {
            get { return this.yearTo; }

            set
            {
                parameters["yearTo"] = value != null ? value.ToString() : string.Empty;
                this.yearTo = value;
            }
        }

        public int? PriceFrom
        {
            get { return this.priceFrom; }

            set
            {
                if (value == null)
                {
                    parameters["priceFrom"] = string.Empty;
                }
                else
                {
                    parameters["man_currencyId"] = "1";
                    parameters["manual_price"] = "1";
                    parameters["man_priceFrom"] = value.ToString();
                }

                this.priceFrom = value;
            }
        }

        public int? PriceTo
        {
            get { return this.priceTo; }

            set
            {
                if (value == null)
                {
                    parameters["priceTo"] = string.Empty;
                }
                else
                {
                    parameters["man_currencyId"] = "1";
                    parameters["manual_price"] = "1";
                    parameters["man_priceTo"] = value.ToString();
                }

                this.priceTo = value;
            }
        }

        public int Page
        {
            get { return this.page; }

            set
            {
                this.page = value;
                parameters["page"] = value.ToString();
            }
        }

        public override string ToString()
        {
            return $"https://www.cars.bg/?{parameters.GetEncodedParameters()}";
        }

        private string BoolToString(bool value)
        {
            return value ? "1" : "0";
        }
    }
}
