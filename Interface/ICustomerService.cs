using System;
using System.Collections;
using System.Collections.Generic;
using ShopMart.DTO;

namespace ShopMart.Interface
{
   public interface ICustomerService
    {
      public  AuthenticationResponse Authenticate(AuthenticationRequest request);
      public  RegistrationResponse Register(RegistrationRequest request);
      public  IEnumerable<CustomerDTO> GetAll();
      public  CustomerDTO GetById(long id);
      public CustomerDTO UpdateCustomer(long id, UpdateCustomerRequest request);
      public void DeleteCustomer(long id);
    }
}
