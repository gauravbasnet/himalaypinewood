//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Himalayan_Angular.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductMain()
        {
            this.Enquiries = new HashSet<Enquiry>();
        }
    
        public int id { get; set; }
        public int BrandId { get; set; }
        public int PTypeId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string ProductDetail { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string Thickness { get; set; }
        public int Quantity { get; set; }
        public string Path { get; set; }
        public System.DateTime PostedOn { get; set; }
        public int PostedBy { get; set; }
    
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enquiry> Enquiries { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
