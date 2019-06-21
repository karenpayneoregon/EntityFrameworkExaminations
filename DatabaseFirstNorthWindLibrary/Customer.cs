namespace DatabaseFirstNorthWindLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int CustomerIdentifier { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(30)]
        public string ContactName { get; set; }

        public int? ContactIdentifier { get; set; }

        public int? ContactTypeIdentifier { get; set; }

        [StringLength(60)]
        public string Street { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        public int? CountryIdentfier { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? InUse { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
