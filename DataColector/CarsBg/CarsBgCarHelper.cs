using Common;
using Domain.Entities;
using Domain.Entities.Enums;
using Dtos;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Persistance;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace DataColector.CarsBg
{
    public partial class CarsBgDataCollector
    {
        private Dictionary<string, string> CarSelectors = new Dictionary<string, string>
        {
            {nameof(Car.ManufactureDate), "//*[@src='https://assets.cars.bg/desktop/images/calendar.gif']/parent::*/following-sibling::td" },
            {nameof(Car.Millage), "//*[@src='https://assets.cars.bg/desktop/images/mileage.gif']/parent::*/following-sibling::td" },
            {nameof(Car.FuelType), "//*[@src='https://assets.cars.bg/desktop/images/petrol.gif']/parent::*/following-sibling::td" },
            {nameof(Car.Transmision), "//*[@src='https://assets.cars.bg/desktop/images/gear.gif']/parent::*/following-sibling::td" },
            {nameof(Car.Power), "//*[@src='https://assets.cars.bg/desktop/images/power.gif']/parent::*/following-sibling::td" },
            {nameof(Car.EngineDisplacement),"//*[@src='https://assets.cars.bg/desktop/images/cubature.gif']/parent::*/following-sibling::td" },
            {nameof(Car.NumberOfDoors), "//*[@src='https://assets.cars.bg/desktop/images/door.gif']/parent::*/following-sibling::td" },
            {nameof(Car.Color), "//*[@src='https://assets.cars.bg/desktop/images/palette.gif']/parent::*/following-sibling::td" },
            {nameof(Car.Price), "//span[@class='ver20black']/strong" },
            {nameof(Car.AdditionalInfo), "//*[text()[contains(., 'Допълнителна')][contains(., 'информация')]]/parent::*/parent::*/following-sibling::*/following-sibling::*" },
            {nameof(Car.ComfortInfoString), "//*[text()[contains(., 'Особености')][contains(., 'Екстри')]]/parent::*/parent::*/parent::*//b[text()='Комфорт:']/parent::*/following-sibling::*" },
            {nameof(Car.SafetyInfoString), "//*[text()[contains(., 'Особености')][contains(., 'Екстри')]]/parent::*/parent::*/parent::*//b[text()='Сигурност:']/parent::*/following-sibling::*" },
            {nameof(Car.OtherInfoString), "/*[text()[contains(., 'Особености')][contains(., 'Екстри')]]/parent::*/parent::*/parent::*//b[text()='Друго:']/parent::*/following-sibling::*" }
        };

        private Car GetCar(ModelSearchDto model, string adUrl)
        {
            WebPage carPage = null;
            int counter = 0;
            while (carPage == null)
            {
                if (counter == 10)
                {
                    return null;
                }
                try
                {
                    carPage = browser.NavigateToPage(new Uri(adUrl));
                    counter++;
                }
                catch (IOException)
                {
                    logger.LogWarning("Connection failed, retry");
                }
                catch (SocketException)
                {
                    logger.LogWarning("Connection failed, retry");
                }
                catch (WebException)
                {
                    logger.LogWarning("Timed out, retry");
                }
                catch (Exception)
                {
                    logger.LogWarning("Unknow error during navigation, retry");
                }
            }
#if DEBUG
            File.WriteAllText(@"./CarsBg/" + model.BrandName.Trim() + "/" + model.Name.Trim() + "/" + adUrl.Substring(25) + ".html", carPage.Content);
#endif
            HtmlNode pageNode = carPage.Html;

            Car car = new Car();
            car.SourceId = (int)SourceEnum.CarsBg;
            car.AdUrl = adUrl;
            car.ModelId = model.Id;

            foreach (PropertyInfo property in typeof(Car).GetProperties())
            {
                if (CarSelectors.ContainsKey(property.Name))
                {
                    string xpathSelector = CarSelectors[property.Name];
                    HtmlNode node = pageNode.SelectSingleNode(xpathSelector);
                    if (node != null)
                    {
                        AssignProperty(car, property.Name, node.InnerText.Trim());
                    }
                }
            }

            return car;
        }

        private void AssignProperty(Car car, string propertyName, string nodeInnerText)
        {
            switch (propertyName)
            {
                case nameof(Car.ManufactureDate):
                    car.ManufactureDate = DateTime.Parse(nodeInnerText);
                    break;

                case nameof(Car.Millage):
                    string formatedKilometars = nodeInnerText.Replace("км", "").Trim();
                    try
                    {
                        car.Millage = int.Parse(
                            formatedKilometars,
                            NumberStyles.AllowThousands,
                            CultureInfo.InvariantCulture);
                    }
                    catch (OverflowException)
                    {
                        logger.LogWarning(car.AdUrl + " Millage overflow exception");
                    }
                    break;

                case nameof(Car.FuelType):
                    car.FuelType = FuelTypeFromString(nodeInnerText);
                    break;

                case nameof(Car.Transmision):
                    car.Transmision = TransmisionFromString(nodeInnerText);
                    break;

                case nameof(Car.Power):
                    string formatedPower = nodeInnerText.Replace("к.с.", "").Trim();
                    try
                    {
                        car.Power = int.Parse(formatedPower);
                    }
                    catch (OverflowException)
                    {
                        logger.LogWarning(car.AdUrl + " Power overflow exception");
                    }
                    break;

                case nameof(Car.EngineDisplacement):
                    string formatedEngineDisplacement = nodeInnerText.Replace("см3", "").Trim();
                    try
                    {
                        car.EngineDisplacement = int.Parse(formatedEngineDisplacement);
                    }
                    catch (OverflowException)
                    {
                        logger.LogWarning(car.AdUrl + " EngineDisplacement overflow exception");
                    }
                    break;

                case nameof(Car.NumberOfDoors):
                    car.NumberOfDoors = NumberOfDoorsFromString(nodeInnerText);
                    break;

                case nameof(Car.Color):
                    car.Color = nodeInnerText;
                    break;

                case nameof(Car.Price):
                    car.Price = decimal.Parse(
                        nodeInnerText,
                        NumberStyles.AllowThousands,
                        CultureInfo.InvariantCulture);
                    break;

                case nameof(Car.AdditionalInfo):
                    car.AdditionalInfo = nodeInnerText;
                    break;

                case nameof(Car.ComfortInfoString):
                    car.ComfortInfoString = nodeInnerText;
                    CarComfortConverter(car, nodeInnerText);
                    break;

                case nameof(Car.SafetyInfoString):
                    car.SafetyInfoString = nodeInnerText;
                    CarSafetyConverter(car, nodeInnerText);
                    break;

                case nameof(Car.OtherInfoString):
                    car.OtherInfoString = nodeInnerText;
                    OtherInfoConverter(car, nodeInnerText);
                    break;
            }
        }

        private static void OtherInfoConverter(Car car, string otherInfo)
        {
            if (otherInfo.Contains("7 места (6+1)"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.SevenPlaces);
            }

            if (otherInfo.Contains("TAXI"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.TAXI);
            }

            if (otherInfo.Contains("Автопилот"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.AutoPilot);
            }

            if (otherInfo.Contains("Бордови компютър"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.BoardComputer);
            }

            if (otherInfo.Contains("Гаранция"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.Warranty);
            }

            if (otherInfo.Contains("Десен волан"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.RightHandDrive);
            }

            if (otherInfo.Contains("Навигационна система"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.NavigationSystem);
            }

            if (otherInfo.Contains("Панорамен покрив"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.PanoramicRoof);
            }

            if (otherInfo.Contains("Ретро"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.Retro);
            }

            if (otherInfo.Contains("Сервизна книжка"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.ServiceBook);
            }

            if (otherInfo.Contains("Серво управление"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.PowerSteering);
            }

            if (otherInfo.Contains("Теглич"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.Towbar);
            }

            if (otherInfo.Contains("Типтроник/Мултитроник"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.Multitronic);
            }

            if (otherInfo.Contains("Тунинг"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.Tunning);
            }

            if (otherInfo.Contains("Хладилен"))
            {
                car.OtherInfo = car.OtherInfo.Add(OtherInfo.Refrigeration);
            }

            car.OtherInfoString = otherInfo;
        }

        private static void CarSafetyConverter(Car car, string carSafetyInfo)
        {
            if (carSafetyInfo.Contains("4x4"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.FourXFour);
            }

            if (carSafetyInfo.Contains("ABS"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.ABS);
            }

            if (carSafetyInfo.Contains("Airbag"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.Airbag);
            }

            if (carSafetyInfo.Contains("ASR/Тракшън контрол"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.ASR);
            }

            if (carSafetyInfo.Contains("ESP"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.ESP);
            }

            if (carSafetyInfo.Contains("Аларма"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.Alarm);
            }

            if (carSafetyInfo.Contains("Безключово палене"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.KeylessIgnition);
            }

            if (carSafetyInfo.Contains("Брониран"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.Armored);
            }

            if (carSafetyInfo.Contains("Застраховка"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.Insurance);
            }

            if (carSafetyInfo.Contains("Имобилайзер"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.Immobilizer);
            }

            if (carSafetyInfo.Contains("Ксенонови фарове"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.XenonLights);
            }

            if (carSafetyInfo.Contains("Парктроник"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.Parktronic);
            }

            if (carSafetyInfo.Contains("Старт-Стоп система"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.StartStopSystem);
            }

            if (carSafetyInfo.Contains("Халогенни фарове"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.HalogenLights);
            }

            if (carSafetyInfo.Contains("Центр. заключване"))
            {
                car.SafetyInfo = car.SafetyInfo.Add(Safety.CentralLocking);
            }

            car.SafetyInfoString = carSafetyInfo;
        }

        private static void CarComfortConverter(Car car, string carComfortInfo)
        {
            if (carComfortInfo.Contains("DVD/TV"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.DVDTV);
            }

            if (carComfortInfo.Contains("Алуминиеви джанти"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.AlloyWheels);
            }

            if (carComfortInfo.Contains("Ел.огледала"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.ElMirrors);
            }

            if (carComfortInfo.Contains("Ел.седалки"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.ElSeats);
            }

            if (carComfortInfo.Contains("Ел.стъкла"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.ElWindows);
            }

            if (carComfortInfo.Contains("Климатик"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.AirConditioner);
            }

            if (carComfortInfo.Contains("Климатроник"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.Climatronic);
            }

            if (carComfortInfo.Contains("Кожен салон"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.LeatherInterior);
            }

            if (carComfortInfo.Contains("Мултифункционален волан"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.MultifunctionSteeringWheel);
            }

            if (carComfortInfo.Contains("Подгряване на седалки"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.SeatHeating);
            }

            if (carComfortInfo.Contains("Стерео уредба"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.Stereo);
            }

            if (carComfortInfo.Contains("Шибедах"))
            {
                car.ComfortInfo = car.ComfortInfo.Add(Comfort.Shibedah);
            }

            car.ComfortInfoString = carComfortInfo;
        }

        private static FuelType FuelTypeFromString(string fuelType)
        {
            switch (fuelType)
            {
                case "Бензин":
                    return FuelType.Petrol;
                case "Дизел":
                    return FuelType.Diesel;
                case "Газ/Бензин":
                    return FuelType.GasPetrol;
                case "Метан/Бензин":
                    return FuelType.MethanePetrol;
                case "Хибрид":
                    return FuelType.Hybrid;
                case "Електричество":
                    return FuelType.Electric;
                default:
                    throw new Exception("Not recognized fuel type");
            }
        }

        private static Transmision TransmisionFromString(string transmission)
        {
            switch (transmission)
            {
                case "Ръчни":
                    return Transmision.Manual;
                case "Автоматични":
                    return Transmision.Automatic;
                case "Полуавтоматични":
                    return Transmision.SemiAutomatic;
                default:
                    throw new Exception("Not recognized transmission type");
            }
        }

        private static NumberOfDoors NumberOfDoorsFromString(string numberOfDoors)
        {
            switch (numberOfDoors)
            {
                case "2/3 врати":
                    return NumberOfDoors.TwoThree;
                case "4/5 врати":
                    return NumberOfDoors.FourFive;
                default:
                    throw new Exception("Not recognized NumberOfDoors pattern");
            }
        }
    }
}
