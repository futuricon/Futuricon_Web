using System;
using System.Threading.Tasks;
using Futuricon.Infrastructure.Services;
using Futuricon.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Futuricon.Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ILogger<ContactModel> _logger;
        private readonly IEmailService _emailService;

        public ContactModel(ILogger<ContactModel> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostMessageAsync(Message o)
        {
            if (string.IsNullOrWhiteSpace(o.Content) || string.IsNullOrWhiteSpace(o.Email))
            {
                return BadRequest("Something went wrong!");
            }

            var TGMsg = $"Hi. There is new message from {o.Name}.\nE-mail:   {o.Email}\nCompany:   {o.Company}\nSubject: {o.Subject}\nContent:   {o.Content}";
            
            try
            {
                await _emailService.SendTGAsync(TGMsg);
            }
            catch (Exception e)
            {
                _logger?.LogError("Could not send email, thrown exception: {Exception}", e);
                return BadRequest();
            }
            
            return new OkObjectResult("SENDING");
        }
    }
}
