namespace GerenciadorUsuarios
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        [Key]
        public long idLog { get; set; }

        public long idUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string Acao { get; set; }

        public DateTime DataAcao { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
