﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFJediTest.ServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EDefCaracteristique", Namespace="http://schemas.datacontract.org/2004/07/EntitiesLayer")]
    public enum EDefCaracteristique : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Strength = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Dexterity = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Perception = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EPhaseTournoi", Namespace="http://schemas.datacontract.org/2004/07/EntitiesLayer")]
    public enum EPhaseTournoi : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale1 = 14,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale2 = 13,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale3 = 12,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale4 = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale5 = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale6 = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale7 = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HuitiemeFinale8 = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QuartFinale1 = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QuartFinale2 = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QuartFinale3 = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QuartFinale4 = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DemiFinale1 = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DemiFinale2 = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Finale = 0,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getJedis", ReplyAction="http://tempuri.org/IService/getJedisResponse")]
        System.Collections.Generic.List<WCFJedi.JediWS> getJedis();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getJedis", ReplyAction="http://tempuri.org/IService/getJedisResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.JediWS>> getJedisAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getStades", ReplyAction="http://tempuri.org/IService/getStadesResponse")]
        System.Collections.Generic.List<WCFJedi.StadeWS> getStades();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getStades", ReplyAction="http://tempuri.org/IService/getStadesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.StadeWS>> getStadesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getMatches", ReplyAction="http://tempuri.org/IService/getMatchesResponse")]
        System.Collections.Generic.List<WCFJedi.MatchWS> getMatches();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getMatches", ReplyAction="http://tempuri.org/IService/getMatchesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.MatchWS>> getMatchesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getTournois", ReplyAction="http://tempuri.org/IService/getTournoisResponse")]
        System.Collections.Generic.List<WCFJedi.TournoiWS> getTournois();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getTournois", ReplyAction="http://tempuri.org/IService/getTournoisResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.TournoiWS>> getTournoisAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getCaracteristiquesOf", ReplyAction="http://tempuri.org/IService/getCaracteristiquesOfResponse")]
        System.Collections.Generic.List<WCFJedi.CaracteristiqueWS> getCaracteristiquesOf(string jediName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/getCaracteristiquesOf", ReplyAction="http://tempuri.org/IService/getCaracteristiquesOfResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.CaracteristiqueWS>> getCaracteristiquesOfAsync(string jediName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/addJedi", ReplyAction="http://tempuri.org/IService/addJediResponse")]
        bool addJedi(WCFJedi.JediWS jedi);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/addJedi", ReplyAction="http://tempuri.org/IService/addJediResponse")]
        System.Threading.Tasks.Task<bool> addJediAsync(WCFJedi.JediWS jedi);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : WCFJediTest.ServiceReference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<WCFJediTest.ServiceReference.IService>, WCFJediTest.ServiceReference.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<WCFJedi.JediWS> getJedis() {
            return base.Channel.getJedis();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.JediWS>> getJedisAsync() {
            return base.Channel.getJedisAsync();
        }
        
        public System.Collections.Generic.List<WCFJedi.StadeWS> getStades() {
            return base.Channel.getStades();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.StadeWS>> getStadesAsync() {
            return base.Channel.getStadesAsync();
        }
        
        public System.Collections.Generic.List<WCFJedi.MatchWS> getMatches() {
            return base.Channel.getMatches();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.MatchWS>> getMatchesAsync() {
            return base.Channel.getMatchesAsync();
        }
        
        public System.Collections.Generic.List<WCFJedi.TournoiWS> getTournois() {
            return base.Channel.getTournois();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.TournoiWS>> getTournoisAsync() {
            return base.Channel.getTournoisAsync();
        }
        
        public System.Collections.Generic.List<WCFJedi.CaracteristiqueWS> getCaracteristiquesOf(string jediName) {
            return base.Channel.getCaracteristiquesOf(jediName);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WCFJedi.CaracteristiqueWS>> getCaracteristiquesOfAsync(string jediName) {
            return base.Channel.getCaracteristiquesOfAsync(jediName);
        }
        
        public bool addJedi(WCFJedi.JediWS jedi) {
            return base.Channel.addJedi(jedi);
        }
        
        public System.Threading.Tasks.Task<bool> addJediAsync(WCFJedi.JediWS jedi) {
            return base.Channel.addJediAsync(jedi);
        }
    }
}
