//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Oauth.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    public partial class claim
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public int identid { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ident ident { get; set; }
        
    }
}
