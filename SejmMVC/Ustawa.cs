namespace SejmMVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ustawa")]
    public partial class Ustawa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ustawa()
        {
            Głos = new HashSet<Głos>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Nazwa { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public int ZgłoszonaPrzez { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Głos> Głos { get; set; }

        public virtual Klub Klub { get; set; }
    }
}
