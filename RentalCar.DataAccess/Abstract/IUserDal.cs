﻿using System;
using System.Collections.Generic;
using System.Text;
using RentalCar.Core.DataAccess;
using RentalCar.Core.Entities.Concrete;

namespace RentalCar.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(int userId);
    }
}
