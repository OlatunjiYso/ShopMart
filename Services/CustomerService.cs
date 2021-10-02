using System;
using System.Collections.Generic;
using System.Linq;
using ShopMart.Data;
using ShopMart.DTO;
using ShopMart.Interface;
using BCryptNet = BCrypt.Net.BCrypt;
using ShopMart.Models;
using Microsoft.EntityFrameworkCore;
using ShopMart.Helpers;
using System.Threading.Tasks;

namespace ShopMart.Services
{
    public class CustomerService : ICustomerService
    {
        private ShopMartContext _context;
        private IJwtUtils _jwtUtils;

        public CustomerService(ShopMartContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }


        /*
         * @desc - Method to Authenticate a customer
         */
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            AuthenticationResponse authResponse = new AuthenticationResponse();
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == request.Email);

            //validate
            try
            {
                if (customer == null || !BCryptNet.Verify(request.Password, customer.Password))
                {
                    authResponse.Message = "Email or password Incorrect";
                    authResponse.Success = false;
                    return authResponse;
                }

                // Successful authentication
                authResponse.JwtToken = _jwtUtils.GenerateToken(customer);
                authResponse.Message = "Authentication successful";
                authResponse.Success = true;
                return authResponse;

            }
            catch(Exception ex)
            {
                authResponse.JwtToken = "Could be an unencripted test password";
                authResponse.Message = ex.Message;
                return authResponse;
            }
            
        }


        /**
         * @desc - Service Method that fetches all customer
         */
        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            return await _context.Customers.Select(x => CustomerToDTO(x)).ToListAsync();
        }


        /*
         * @desc - Service method to find a single customer.
         */
        public async Task<CustomerDTO> GetById(long id)
        {
            var customer = await GetSingleCustomer(id);
            if (customer == null)
                return null;
            return CustomerToDTO(customer);
        }


        /*
         * @desc - Service Method that registers a new user
         */
        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var response = new RegistrationResponse();
            // validation
            if ( await _context.Customers.AnyAsync(x => x.Email == request.Email))
            {  response.HasConflict = true;
                return response;
            }

            var customer = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            // hash password
            customer.Password = BCryptNet.HashPassword(request.Password);

            // save user
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            var newlySavedCustomer = await _context.Customers.FirstAsync(x => x.Email == request.Email);

            response.Message = "Registration successful";
            response.Success = true;
            response.JwtToken = _jwtUtils.GenerateToken(newlySavedCustomer);
            return response;
        }

        /*
         * @desc - Method to update a customer.
         */
        public async Task<CustomerDTO> UpdateCustomer(long id, UpdateCustomerRequest request)
        {
            var customer = await GetSingleCustomer(id);
            if (!string.IsNullOrEmpty(request.Email) && await _context.Customers.AnyAsync(x => x.Email == request.Email))
                throw new AppException("The supplied Email is already taken");
            if (!string.IsNullOrEmpty(request.Password))
                customer.Password = request.Password;
            if (!string.IsNullOrEmpty(request.Email))
                customer.Email = request.Email;
            if (!string.IsNullOrEmpty(request.FirstName))
                customer.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName))
                customer.LastName = request.LastName;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return CustomerToDTO(customer);

        }
        /*
         * @desc - Method to delete a customer.
         */
        public async void DeleteCustomer(long id)
        {
            var customer = await GetSingleCustomer(id);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }



        // HELPERS
        /*
         * @desc - Method to convert Cutomer Object to CustomerDTO
         */
        private static CustomerDTO CustomerToDTO(Customer customer)
        {
            return new CustomerDTO()
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }
        /*
         * @desc - HelperMethod that finds a single customer.
         */
        private async Task<Customer> GetSingleCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer;
        }

    }
}
