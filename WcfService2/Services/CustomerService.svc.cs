using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace WcfService2.Services
{

    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomer(string customerId)
        {
            if (!int.TryParse(customerId, out int customerIdInt))
            {
                throw new ArgumentException("Invalid customer ID format.", nameof(customerId));
            }

            _customerRepository.DeleteCustomer(customerIdInt);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer GetCustomer(string customerId)
        {
            if (!int.TryParse(customerId, out int customerIdInt))
            {
                throw new ArgumentException("Invalid customer ID format.", nameof(customerId));
            }

            var customer = _customerRepository.GetCustomer(customerIdInt);
            if (customer == null)
            {
                throw new ArgumentException($"Customer with ID {customerId} not found.");
            }

            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }
    }

    #region Customer
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]

        public string Name { get; set; }

        [DataMember]

        public string Email { get; set; }

        [DataMember]

        public string Phone { get; set; }
        #endregion

    }
}