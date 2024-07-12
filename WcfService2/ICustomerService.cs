using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfService2.Services;


namespace WcfService2
{
    [ServiceContract]
    public interface ICustomerService
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
                  RequestFormat = WebMessageFormat.Json,
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "Customer/{customerId}")]
        Customer GetCustomer(string customerId);

        [OperationContract]
        [WebInvoke(Method = "GET",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "Customers")]
        List<Customer> GetAllCustomers();

        [OperationContract]
        [WebInvoke(Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "Customer")]
        void AddCustomer(Customer customer);

        [OperationContract]
        [WebInvoke(Method = "PUT",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "customer")]
        void UpdateCustomer(Customer customer);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "Customer/{customerId}")]
        void DeleteCustomer(string customerId);
    }
}
