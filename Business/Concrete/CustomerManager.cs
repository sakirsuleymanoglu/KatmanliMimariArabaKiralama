﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);

            return new SuccessResult(Messages.SuccessAddCustomer);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);

            return new SuccessResult(Messages.SuccessDeleteCustomer);
        }

        public IDataResult<Customer> Get(int id)
        {
            var result = _customerDal.Get(c => c.Id == id);

            if (result == null)
            {
                return new ErrorDataResult<Customer>(Messages.ErrorGetCustomerById);
            }

            return new SuccessDataResult<Customer>(result, Messages.SuccessGetCustomerById);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();

            if (result.Count == 0)
            {
                return new ErrorDataResult<List<Customer>>(Messages.ErrorListCustomers);
            }

            return new SuccessDataResult<List<Customer>>(result, Messages.SuccessListCustomers);
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);

            return new SuccessResult(Messages.SuccessUpdateCustomer);
        }
    }
}
