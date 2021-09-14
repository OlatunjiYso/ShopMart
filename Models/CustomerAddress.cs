using System;

namespace ShopMart.Models
{
    public class CustomerAddress
    {
        public long CustomerAddressId { get; set; }
        public long CustomerId { get; set; }
        public string AddressTitle { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber {get; set; }

    }
}