using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Waddhly.Models
{
    public class Certificate
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public byte[] image { get; set; }
        public string CertificateUrl { get; set; }
        [ForeignKey("user")]
        public string userid { get; set; }
        public virtual User user { get; set; }

    }
}
