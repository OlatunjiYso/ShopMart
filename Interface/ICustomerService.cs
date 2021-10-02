using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopMart.DTO;

namespace ShopMart.Interface
{
   public interface ICustomerService
    {
      public   Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
      public  Task<RegistrationResponse> Register(RegistrationRequest request);
      public  Task<IEnumerable<CustomerDTO>> GetAll();
      public  Task<CustomerDTO> GetById(long id);
      public Task<CustomerDTO> UpdateCustomer(long id, UpdateCustomerRequest request);
      public void DeleteCustomer(long id);
    }
}
