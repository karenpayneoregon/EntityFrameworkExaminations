namespace DatabaseFirstNorthWindLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContactContactDevice
    {
        [Key]
        public int Identifier { get; set; }

        public int? ContactIdentifier { get; set; }

        public int? PhoneTypeIdenitfier { get; set; }

        public string PhoneNumber { get; set; }

        public bool? Active { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual PhoneType PhoneType { get; set; }
    }
}
