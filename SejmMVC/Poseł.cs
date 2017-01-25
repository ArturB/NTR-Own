namespace SejmMVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Poseł
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poseł()
        {
            Głos = new HashSet<Głos>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string Imie { get; set; }

        [Required]
        [StringLength(32)]
        public string Nazwisko { get; set; }

        [Column(TypeName = "image")]
        public byte[] Foto { get; set; }

        [Column("Klub parlamentarny")]
        public int? Klub_parlamentarny { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Głos> Głos { get; set; }

        public virtual Klub Klub { get; set; }
    }
}
