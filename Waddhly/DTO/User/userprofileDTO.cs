using Waddhly.Models;

namespace Waddhly.DTO.User
{
    public class userprofileDTO
    {
      
        public string fname { get; set; }
        public string lname { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public double hourRate { get; set; }
        public double MoneyAccount { get; set; }
        public string PhoneNumber { get; set; }
        public bool phonenNumberConfirm { get; set; }
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string country { get; set; }
        public string userName { get; set; }
        public string normalizeusername { get; set; }
        public string normalizeemail { get; set; }  
        public string password { get; set; }
        public bool twofactor { get; set; }
        public bool lockoutenable { get; set; }
        public int accessfailedcount { get; set; }
        public string certificateTitle { get; set; }
        public string CertificateUrl { get; set; }
        public byte[] userimage { get; set; }
        public string gender { get; set; }
        public List<Tuple<string,string ,int, byte[]>> certfcimage { get; set; }=new List<Tuple<string,string,int, byte[]>>();
        

    }
}
