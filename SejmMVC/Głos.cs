namespace SejmMVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Głos
    {
        public int ID { get; set; }

        public int Poseł { get; set; }

        public int Ustawa { get; set; }

        public bool? Głosował { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        public virtual Poseł Poseł1 { get; set; }

        public virtual Ustawa Ustawa1 { get; set; }
    }
}
