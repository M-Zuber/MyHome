//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ef_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_payment_methods
    {
        public t_payment_methods()
        {
            this.t_expenses = new HashSet<t_expenses>();
            this.t_incomes = new HashSet<t_incomes>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<t_expenses> t_expenses { get; set; }
        public virtual ICollection<t_incomes> t_incomes { get; set; }
    }
}