using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Repositories.Extensions
{
    public static class IntegrityChecking
    {
        // ------------------ Ship Integrity ------------------
        
        public static bool CheckCreation(this Ship model,
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
                    message = DuplicateNameMessage(ShipCaption, obj.Name);
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
                        message = DuplicateRegistrationNumberMessage(ShipCaption, obj.RegistrationNumber);
                    }
                    else
                    {
                        message += AndRegistrationNumberMessage(obj.RegistrationNumber);
                    }
                }
            }
            return result;
        }

        public static bool CheckUpdating(this Ship model,
            IUnitOfWork unitOfWork, out string message)
        {

            message = null!;
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
                    message = DuplicateNameMessage(ShipCaption, obj.Name);
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
                        message = DuplicateRegistrationNumberMessage(ShipCaption, obj.RegistrationNumber);
                    }
                    else
                    {
                        message += AndRegistrationNumberMessage(obj.RegistrationNumber);
                    }
                }
            }
            return result;
        }

        // -------------------- TypeOfShip --------------------

        public static bool CheckCreation(this TypeOfShip model,
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
                    message = DuplicateNameMessage(TypeOfShipCaption, obj.Name);
                }
            }
            return result;
        }

        public static bool CheckUpdating(this TypeOfShip model,
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
                    message = DuplicateNameMessage(TypeOfShipCaption, obj.Name);
                }
            }

            if (result && model.Parent != null)
            {
                var all = unitOfWork.TypeOfShipsRepository.GetAll(); // .ToList()

                var descendants = new HashSet<int>();
                var queue = new Queue<int>();
                queue.Enqueue(model.Id);
                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    foreach (var child in all.Where(e => e.Parent?.Id == current))
                    {
                        if (descendants.Add(child.Id))
                            queue.Enqueue(child.Id);
                    }
                }

                if (model.Parent.Id == model.Id || descendants.Contains(model.Parent.Id))
                {
                    result = false;
                    message = HierarchyCycleMessage(TypeOfShipCaption);
                }
            }

            return result;
        }

        public static bool CheckDeleting(this TypeOfShip model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (unitOfWork.TypeOfShipsRepository.GetAll()
                    .Any(e => e.Parent?.Key == model.Key))
            {
                message = DeletingForbiddenMessage(TypeOfShipCaption, model.Key, TypeOfShipsHeader);
            }
            if (unitOfWork.ShipsRepository.GetAll()
                    .Any(e => e.TypeOfShip?.Key == model.Key))
            {
                if (message == null)
                {
                    message = DeletingForbiddenMessage(TypeOfShipCaption, model.Key, ShipsHeader);
                }
                else
                {
                    message += AndDependentMessage(ShipsHeader);
                }
            }
            return message == null;
        }

        // -------------------- Shipyard --------------------

        public static bool CheckCreation(this Shipyard model,
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
                    message = DuplicateNameMessage(ShipyardCaption, obj.Name);
                }
            }
            return result;
        }

        public static bool CheckUpdating(this Shipyard model,
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
                    message = DuplicateNameMessage(ShipyardCaption, obj.Name);
                }
            }
            return result;
        }
        public static bool CheckDeleting(this Shipyard model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (unitOfWork.ShipProjectsRepository.GetAll()
                    .Any(e => e.Shipyard?.Key == model.Key))
            {
                message = DeletingForbiddenMessage(ShipyardCaption, model.Key, ProjectsHeader);
            }
            if (unitOfWork.ShipsRepository.GetAll()
                    .Any(e => e.Shipyard?.Key == model.Key))
            {
                if (message == null)
                {
                    message = DeletingForbiddenMessage(ShipyardCaption, model.Key, ShipsHeader);
                }
                else
                {
                    message += AndDependentMessage(ShipsHeader);
                }
            }
            return message == null;
        }

        // -------------------- ShipProject --------------------

        public static bool CheckCreation(this ShipProject model,
                IUnitOfWork unitOfWork, out string? message)
        {
            message = null;

            if (!unitOfWork.ShipProjectsRepository.GetAll().Any())
                return true;

            bool result = true;

            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.ShipProjectsRepository.GetAll()
                    .FirstOrDefault(e => string.Compare(e.Name, model.Name, true) == 0);

                if (obj != null)
                {
                    result = false;
                    message = DuplicateNameMessage(ProjectCaption, obj.Name);
                }
            }

            return result;
        }

        public static bool CheckUpdating(this ShipProject model,
            IUnitOfWork unitOfWork, out string? message)
        {
            message = null;
            if (!unitOfWork.ShipProjectsRepository.GetAll().Any())
                return true;
            bool result = true;
            if (!string.IsNullOrEmpty(model.Name))
            {
                var obj = unitOfWork.ShipProjectsRepository.GetAll()
                    .FirstOrDefault(e => e.Id != model.Id
                       && string.Compare(e.Name, model.Name, true) == 0);
                if (obj != null)
                {
                    result = false;
                    message = DuplicateNameMessage(ProjectCaption, obj.Name);
                }
            }
            return result;
        }

        public static bool CheckDeleting(this ShipProject model,
            IUnitOfWork unitOfWork, out string? message)
        {

            message = null;
            if (unitOfWork.ShipsRepository.GetAll()
                    .Any(e => e.Project?.Key == model.Key))
            {
                message = DeletingForbiddenMessage(ProjectCaption, model.Key, ShipsHeader);
            }
            return message == null;
        }
     }
}
