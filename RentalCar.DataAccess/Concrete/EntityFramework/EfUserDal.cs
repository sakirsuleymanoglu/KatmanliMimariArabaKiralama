﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RentalCar.Core.DataAccess.EntityFramework;
using RentalCar.DataAccess.Abstract;
using RentalCar.Core.Entities.Concrete;

namespace RentalCar.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RentalCarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(int userId)
        {
          using(var context = new RentalCarContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == userId
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();                      
            }
        }
    }
}
