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
        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            AuthenticationResponse authResponse = new AuthenticationResponse();
            var customer = _context.Customers.SingleOrDefault(x => x.Email == request.Email);

            //validate
            if(customer == null | BCryptNet.Verify(request.Password, customer.Password))
            {
                throw new ApplicationException("Invalid Email or password");
            }

            // Successful authentication
            authResponse.JwtToken = _jwtUtils.GenerateToken(customer);
            authResponse.Message = "Authentication successful";
            authResponse.Success = true;
            return authResponse;
        }


        /**
         * @desc - Methods that fetches all customer
         */
        public IEnumerable<CustomerDTO> GetAll()
        {
            return _context.Customers.Select(x => CustomerToDTO(x)).ToList();
        }


        /*
         * @desc - Service method to find a single customer.
         */
        public CustomerDTO GetById(long id)
        {
            var customer = GetSingleCustomer(id);
            return CustomerToDTO(customer);
        }


        /*
         * @desc - Method that Creates a new user
         */
        public RegistrationResponse Register(RegistrationRequest request)
        {
            // validate
            if (_context.Customers.Any(x => x.Email == request.Email))
                throw new AppException("Email '" + request.Email + "' is already taken");

            var customer = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            // hash password
            customer.Password = BCryptNet.HashPassword(request.Password);

            // save user
            _context.Customers.Add(customer);
            _context.SaveChanges();
            var newlySavedCustomer = _context.Customers.First(x => x.Email == request.Email);

            var response = new RegistrationResponse();
            response.Message = "Registration successful";
            response.Success = true;
            response.JwtToken = _jwtUtils.GenerateToken(newlySavedCustomer);
            return response;
        }

        /*
         * @desc - Method to update a customer.
         */
        public CustomerDTO UpdateCustomer(long id, UpdateCustomerRequest request)
        {
            var customer = GetSingleCustomer(id);
            if (!string.IsNullOrEmpty(request.Email) && _context.Customers.Any(x => x.Email == request.Email))
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
            _context.SaveChanges();

            return CustomerToDTO(customer);

        }
        /*
         * @desc - Method to delete a customer.
         */
        public void DeleteCustomer(long id)
        {
            var customer = GetSingleCustomer(id);

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }



        // HELPERS
        /*
         * @desc - Method to convert Cutomer Object to CustomerDTO
         */
        public CustomerDTO CustomerToDTO(Customer customer)
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
        private Customer GetSingleCustomer(long id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                throw new KeyNotFoundException("User not found");
            return customer;
        }

    }
}
