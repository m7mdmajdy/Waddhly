using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waddhly.Data;
using Waddhly.DTO.User;
using Waddhly.Models;

namespace Waddhly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PortfoliosController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        //get
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPortfolio(string id)
        {
            var userportfolio =  context.Portfolios.Include(u => u.user).Where(p => p.user.Id == id).ToList();
            List<portfolioDto> ports= new List<portfolioDto>();
            if (userportfolio == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in userportfolio)
                {
                    portfolioDto portfolioDto = new portfolioDto();

                    portfolioDto.ProjectUrl = item.ProjectUrl;
                    portfolioDto.Description = item.Description;
                    portfolioDto.Date = item.Date;
                    portfolioDto.portimage = item.image;
                    portfolioDto.Title = item.Title;
                    portfolioDto.ID = item.ID;
                    portfolioDto.userId = item.user.Id;
                    ports.Add(portfolioDto);
                }
                return Ok(ports);
            }
        }

      ///ppost
        [HttpPost]
        public async Task<ActionResult> PostPortfolio( [FromForm]portfolioDto portfoliodto,string id)
        {
           
            var memoryStream = new MemoryStream();
            var userid=context.Users.Where(u=>u.Id==id).Select(u=>u.Id);
            portfoliodto.File.CopyToAsync(memoryStream);
            if (memoryStream.Length < 2097152&&memoryStream.ToArray()!=null&&userid!=null)
            {
                Portfolio portfolio = new Portfolio();
               // portfolio.ID = portfoliodto.ID;
                portfolio.Title= portfoliodto.Title;
                portfolio.Description= portfoliodto.Description;
                portfolio.Date = portfoliodto.Date;
                portfolio.ProjectUrl= portfoliodto.ProjectUrl;
                portfolio.userid = portfoliodto.userId;
                portfolio.image = memoryStream.ToArray();

                context.Portfolios.Add(portfolio);
                await context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("File", "The file is too large.");
            }
            return Ok();
         
        }

        // DELETE: api/Portfolios/
        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio(string uid,int pid)
        {
            var portfolio =  context.Portfolios.Include(u=>u.user).Where(p=>p.user.Id==uid&&p.ID==pid).FirstOrDefault();
            if (portfolio == null)
            {
                return NotFound();
            }

            context.Portfolios.Remove(portfolio);
            await context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
