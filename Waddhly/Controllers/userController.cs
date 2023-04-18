using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using Waddhly.Data;
using Waddhly.DTO.User;
using Waddhly.Models;

namespace Waddhly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public userController(ApplicationDbContext _context)
        { this.context = _context; }

        [HttpGet("{id}")]
        public IActionResult Get(string id)

        {
            userprofileDTO userprofileDto = new userprofileDTO();
            var user = context.Users.Include(c => c.category).FirstOrDefault(u => u.Id == id);
            var certfic = context.Certificates.Include(u => u.user).FirstOrDefault(u => u.user.Id == id);
            if (user != null || certfic != null)
            {
                userprofileDto.name = $"{user.FirstName} {user.LastName}";
                userprofileDto.fname = user.FirstName; 
                userprofileDto.lname = user.LastName; 
                userprofileDto.summary = user.Summary;
                userprofileDto.title = user.Title;
                userprofileDto.email = user.Email;
                userprofileDto.MoneyAccount = user.MoneyAccount;
                userprofileDto.PhoneNumber = user.PhoneNumber;

                if (user.category != null)
                {
                    userprofileDto.categoryID = user.category.ID;
                    userprofileDto.categoryName = user.category.Name;
                }

                userprofileDto.hourRate = user.HourRate;
                userprofileDto.userimage = user.image;
                userprofileDto.country = user.Country;
                userprofileDto.gender=user.Gender;
                var img = context.Certificates.Include(u => u.user).Where(u => u.user.Id == id).Select(u=>u);
                foreach (var im in img)
                {
                    userprofileDto.certfcimage.Add(new Tuple<string,string,int, byte[]>( im.Title, im.CertificateUrl, im.ID, im.image) );
                    
                }
                
                return Ok(userprofileDto);


            }
            else
            {
                return BadRequest("no data found");
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromForm] userprofileDTO userDTO)
        {
            var memoryStream = new MemoryStream();
            var user = context.Users.Include(c => c.category).FirstOrDefault(u => u.Id == id);

            if (userDTO.File != null)
            {
                await userDTO.File.CopyToAsync(memoryStream);
                user.image = memoryStream.ToArray();
            }

            user.FirstName = userDTO.fname ?? user.FirstName;
            user.LastName = userDTO.lname ?? user.LastName;
            user.PhoneNumber = userDTO.PhoneNumber ?? user.PhoneNumber;
            user.PhoneNumberConfirmed = false;
            user.Email = userDTO.email ?? user.Email;
            user.Title = userDTO.title ?? user.Title;
            user.Summary = userDTO.summary ?? user.Summary;
            user.HourRate = userDTO.hourRate ?? user.HourRate;
            user.Country = userDTO.country ?? user.Country;
            user.AccessFailedCount = 0;
            user.MoneyAccount = userDTO.MoneyAccount ?? user.MoneyAccount;
            user.NormalizedEmail = user.NormalizedEmail;
            user.NormalizedUserName = user.NormalizedUserName;
            user.LockoutEnabled = false;
            user.TwoFactorEnabled = true;
            user.PasswordHash = user.PasswordHash;
            if (userDTO.categoryID != null)
            {
                user.category = context.Categories.FirstOrDefault(x => x.ID == userDTO.categoryID);
            }
            user.Gender = userDTO.gender ?? user.Gender;
            user.ConcurrencyStamp = "";
            user.EmailConfirmed = false;
            user.UserName = userDTO.userName ?? user.UserName;


            await context.SaveChangesAsync();
            return Ok(user);
        }




        [HttpPost]
        [Route("user-profile")]
        public ActionResult create( [FromForm]certificateDTO cert)
        {
            var memoryStream = new MemoryStream();        
                 cert.File.CopyToAsync(memoryStream);
            if (memoryStream.Length < 2097152)
            {
                Certificate file = new Certificate()
                {
                    Title = cert.Title,
                    CertificateUrl = cert.CertificateUrl,
                    userid = cert.userid,
                    image = memoryStream.ToArray()
                };

                context.Certificates.Add(file);
                context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("File", "The file is too large.");
            }
            return Ok();
        }
        [HttpDelete]
        [Route("deletcertificate")]
        public async Task<ActionResult<Certificate>> Deletecertifct(int id)
        {
            var cert = await context.Certificates.FindAsync(id);
            if (cert == null)
            {
                return NotFound("certfict id not foun");
            }

            context.Certificates.Remove(cert);
            await context.SaveChangesAsync();

            return cert;
        }
    }
}
