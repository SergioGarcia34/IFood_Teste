using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IfoodAPI.Models
{
    [Table("PRODUTO")]
    public partial class Produto
    {
        [Key]
        [Column("ID_PROD")]
        public int IdProd { get; set; }
        [Required]
        [Column("NOM_PROD")]
        [StringLength(255)]
        public string NomProd { get; set; }
        [Column("VL_VEND", TypeName = "decimal(18, 2)")]
        public decimal VlVend { get; set; }
        [Column("DESC_PROD")]
        [StringLength(255)]
        public string DescProd { get; set; }
        [Column("STR_IMAG")]
        public byte[] StrImag { get; set; }
        [Column("DT_INCLUSAO", TypeName = "datetime")]
        public DateTime? DtInclusao { get; set; }
          
    }
}
