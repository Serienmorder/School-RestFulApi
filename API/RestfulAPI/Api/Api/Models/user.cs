//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string CreateDate { get; set; }
        public string HashedPassword { get; set; }
        public string hybridauth_provider_name { get; set; }
        public string hybridauth_provider_uid { get; set; }
        public Nullable<int> deleted { get; set; }
        public string poop { get; set; }
    }
}
