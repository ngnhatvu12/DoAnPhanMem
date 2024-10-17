using System.ComponentModel.DataAnnotations;

namespace DoAnPhanMem.Models
{
    public class Loai
    {
        [Key]
        [StringLength(10)]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(30)]
        public string TenLoai { get; set; }
    }
}
