﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weather.CombinedServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CombinedServices.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/temperatureConversion", ReplyAction="http://tempuri.org/IService1/temperatureConversionResponse")]
        double temperatureConversion(double x, string choice);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/temperatureConversion", ReplyAction="http://tempuri.org/IService1/temperatureConversionResponse")]
        System.Threading.Tasks.Task<double> temperatureConversionAsync(double x, string choice);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Weather5day", ReplyAction="http://tempuri.org/IService1/Weather5dayResponse")]
        string[] Weather5day(string zipcode, string country, string choice);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Weather5day", ReplyAction="http://tempuri.org/IService1/Weather5dayResponse")]
        System.Threading.Tasks.Task<string[]> Weather5dayAsync(string zipcode, string country, string choice);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/StaffValidation", ReplyAction="http://tempuri.org/IService1/StaffValidationResponse")]
        bool StaffValidation(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/StaffValidation", ReplyAction="http://tempuri.org/IService1/StaffValidationResponse")]
        System.Threading.Tasks.Task<bool> StaffValidationAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RemoveStaffMember", ReplyAction="http://tempuri.org/IService1/RemoveStaffMemberResponse")]
        bool RemoveStaffMember(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RemoveStaffMember", ReplyAction="http://tempuri.org/IService1/RemoveStaffMemberResponse")]
        System.Threading.Tasks.Task<bool> RemoveStaffMemberAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateStaffMember", ReplyAction="http://tempuri.org/IService1/CreateStaffMemberResponse")]
        bool CreateStaffMember(string username, string password, string role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateStaffMember", ReplyAction="http://tempuri.org/IService1/CreateStaffMemberResponse")]
        System.Threading.Tasks.Task<bool> CreateStaffMemberAsync(string username, string password, string role);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Weather.CombinedServices.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Weather.CombinedServices.IService1>, Weather.CombinedServices.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double temperatureConversion(double x, string choice) {
            return base.Channel.temperatureConversion(x, choice);
        }
        
        public System.Threading.Tasks.Task<double> temperatureConversionAsync(double x, string choice) {
            return base.Channel.temperatureConversionAsync(x, choice);
        }
        
        public string[] Weather5day(string zipcode, string country, string choice) {
            return base.Channel.Weather5day(zipcode, country, choice);
        }
        
        public System.Threading.Tasks.Task<string[]> Weather5dayAsync(string zipcode, string country, string choice) {
            return base.Channel.Weather5dayAsync(zipcode, country, choice);
        }
        
        public bool StaffValidation(string username, string password) {
            return base.Channel.StaffValidation(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> StaffValidationAsync(string username, string password) {
            return base.Channel.StaffValidationAsync(username, password);
        }
        
        public bool RemoveStaffMember(string username) {
            return base.Channel.RemoveStaffMember(username);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveStaffMemberAsync(string username) {
            return base.Channel.RemoveStaffMemberAsync(username);
        }
        
        public bool CreateStaffMember(string username, string password, string role) {
            return base.Channel.CreateStaffMember(username, password, role);
        }
        
        public System.Threading.Tasks.Task<bool> CreateStaffMemberAsync(string username, string password, string role) {
            return base.Channel.CreateStaffMemberAsync(username, password, role);
        }
    }
}
