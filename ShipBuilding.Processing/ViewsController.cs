using Common.Processing.Editing;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldDividing.Repositories.Extensions;

namespace ShipBuilding.Processing
{
    public class ViewsController : BaseViewsController
    {
        readonly IUnitOfWork unitOfWork;

        public ViewsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }

        protected override string GetStorageDataString()
        {
            //throw new NotImplementedException();
            return unitOfWork.ToDataString();
        }

        protected override bool GetStorageIsEmpty()
        {
            //throw new NotImplementedException();
            return unitOfWork.IsEmpty();

        }

        protected override void Save()
        {
            //throw new NotImplementedException();
            unitOfWork.Save();
        }
    }
}
