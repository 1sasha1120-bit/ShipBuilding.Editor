using ShipBuilding.Processing.Models;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Processing.Analysis
{
    public static class IntegrityChecking
    {
        public static bool CheckCreation(this TypeOfShipModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (!unitOfWork.TypeOfShipsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.TypeOfShipsRepository.GetAll()
                    .FirstOrDefault(e => string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = "Помилка: вже існує запис " +
                        $"про {TypeOfShipCaption.ToLower()} з назвою \"{obj.Name}\"";
                }
            }
            return result;
        }

        public static bool CheckUpdating(this TypeOfShipModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (!unitOfWork.TypeOfShipsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.TypeOfShipsRepository.GetAll()
                    .FirstOrDefault(e => e.Id != model.Id
                       && string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = "Помилка: вже існує запис " +
                        $"про {TypeOfShipCaption.ToLower()} з назвою \"{obj.Name}\"";
                }
            }
            return result;
        }

        public static bool CheckDeleting(this TypeOfShipModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (unitOfWork.TypeOfShipsRepository.GetAll()
                    .Any(e => e.Parent?.Key == model.Key))
            {
                message = "Видалення заборонене, оскільки " +
                    $"для {TypeOfShipCaption.ToLower()} {model.Key} є дані про " +
                    $"підпорядковані {TypeOfShipsHeader.ToLower()}";
            }
            if (unitOfWork.ShipsRepository.GetAll()
                    .Any(e => e.TypeOfShip?.Key == model.Key))
            {
                if (message == null)
                {
                    message = "Видалення заборонене, оскільки " +
                        $"для {TypeOfShipCaption.ToLower()} {model.Key} є дані про " +
                        $"підпорядковані {ShipsHeader.ToLower()}";
                }
                else
                {
                    message += $" та підпорядковані {ShipsHeader.ToLower()}";
                }
            }
            return message == null;
        }
        // ------------------------------------------------------------------------------------

        public static bool CheckCreation(this ShipModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (!unitOfWork.ShipsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.ShipsRepository.GetAll()
                    .FirstOrDefault(e => string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = "Помилка: вже існує запис " +
                        $"про {ShipCaption.ToLower()} з назвою \"{obj.Name}\"";
                }
            }
            if (!string.IsNullOrEmpty(model.RegistrationNumber))
            {
                var obj = unitOfWork.ShipsRepository.GetAll()
                    .FirstOrDefault(e => string.Compare(e.RegistrationNumber, 
                            model.RegistrationNumber, true) == 0);
                if (obj != null)
                {
                    result = false;
                    if (message == null)
                    {
                        message = "Помилка: вже існує запис " +
                        $"про {ShipCaption.ToLower()} з номером \"{obj.RegistrationNumber}\"";
                    }
                    else
                    {
                        message += $" та номером \"{obj.RegistrationNumber}\"";
                    }
                }
            }
            return result;
        }

        public static bool CheckUpdating(this ShipModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (!unitOfWork.ShipsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.ShipsRepository.GetAll()
                    .FirstOrDefault(e => e.Id != model.Id
                         && string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = "Помилка: вже існує запис " +
                        $"про {ShipCaption.ToLower()} з назвою \"{obj.Name}\"";
                }
            }
            if (!string.IsNullOrEmpty(model.RegistrationNumber))
            {
                var obj = unitOfWork.ShipsRepository.GetAll()
                    .FirstOrDefault(e => e.Id != model.Id
                        && string.Compare(e.RegistrationNumber, model.RegistrationNumber, true) == 0);
                if (obj != null)
                {
                    result = false;
                    if (message == null)
                    {
                        message = "Помилка: вже існує запис " +
                        $"про {ShipCaption.ToLower()} з номером \"{obj.RegistrationNumber}\"";
                    }
                    else
                    {
                        message += $" та {ShipCaption.ToLower()} з номером \"{obj.RegistrationNumber}\"";
                    }
                }
            }
            return result;
        }

        // --------------------------------------------------------------------------------------------------------

        public static bool CheckCreation(this ShipyardModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (!unitOfWork.ShipyardsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.ShipyardsRepository.GetAll()
                    .FirstOrDefault(e => string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = "Помилка: вже існує запис " +
                        $"про {ShipyardCaption.ToLower()} з назвою \"{obj.Name}\"";
                }
            }
            return result;
        }

        public static bool CheckUpdating(this ShipyardModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (!unitOfWork.ShipyardsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.ShipyardsRepository.GetAll()
                    .FirstOrDefault(e => e.Id != model.Id
                       && string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = "Помилка: вже існує запис " +
                        $"про {ShipyardCaption.ToLower()} з назвою \"{obj.Name}\"";
                }
            }
            return result;
        }
        public static bool CheckDeleting(this ShipyardModel model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (unitOfWork.ShipsRepository.GetAll()
                    .Any(e => e.Shipyard?.Key == model.Key))
            {
                if (message == null)
                {
                    message = "Видалення заборонене, оскільки " +
                        $"для {ShipyardCaption.ToLower()} {model.Key} є дані про " +
                        $"підпорядковані {ShipsHeader.ToLower()}";
                }
                else
                {
                    message += $" та підпорядковані {ShipsHeader.ToLower()}";
                }
            }
            return message == null;
        }

    }
}
