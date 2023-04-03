using System.ComponentModel.DataAnnotations;

namespace Waddhly.Models
{
    public class Certificate
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public byte[] image { get; set; }
        public string CertificateUrl { get; set; }
        public virtual User user { get; set; }

    }
}
