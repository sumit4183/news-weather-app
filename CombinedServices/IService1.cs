using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CombinedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        double temperatureConversion(double x, string choice);

        [OperationContract]
        string[] Weather5day(string zipcode, string country, string choice);

        [OperationContract]
        bool StaffValidation(string username, string password);
        // TODO: Add your service operations here

        [OperationContract]
        bool RemoveStaffMember(string username);

        [OperationContract]
        bool CreateStaffMember(string username, string password, string role);


    }
}
