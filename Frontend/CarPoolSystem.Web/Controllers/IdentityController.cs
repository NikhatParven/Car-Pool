using CarPoolSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolSystem.Web.Controllers
{
    public class IdentityController : Controller
    {

        private readonly IConfiguration _configuration;

        public IdentityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        

        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDto registrationDto)
        {
            if (ModelState.IsValid)
            {
                string identityApiUrl = _configuration["ServiceUrls:IdentityAPI"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(identityApiUrl);

                    // Send the registration data to your Identity API
                    var response = await client.PostAsJsonAsync("api/user/register", registrationDto);

                    if (response.IsSuccessStatusCode)
                    {

                        // Registration was successful. You can redirect or display a success message.
                        Response.Redirect("https://localhost:7106/Identity/Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    }
                }
            }
            return View(registrationDto);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                string identityApiUrl = _configuration["ServiceUrls:IdentityAPI"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(identityApiUrl);

                    // Send the login data to your Identity API
                    var response = await client.PostAsJsonAsync("api/user/login", loginDto);

                    if (response.IsSuccessStatusCode)
                    {
                        // Login was successful. You can redirect or perform other actions.
                        // For example, you can store a token in a cookie or session for authentication.
                        Response.Redirect("https://localhost:7106"); // Redirect to the home page after successful login
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                    }
                }
            }
            return View(loginDto);
        }
    }
}
