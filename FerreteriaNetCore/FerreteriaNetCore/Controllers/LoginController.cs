using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FerreteriaNetCore.DAO;
using FerreteriaNetCore.Models.DTOs.RequestsDTO;
using FerreteriaNetCore.Models.DTOs.ResponseDTO;
using FerreteriaNetCore.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace FerreteriaNetCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(LoginDTO loginDTO)
        {
            using(DAOFactory daoFactory = new DAOFactory())
            {
                UserModel user = daoFactory.UserDAO.GetUser(loginDTO.Username, loginDTO.Password);

                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                UserResponse userResponse = new UserResponse
                {
                    UserName = user.Username
                };

                HttpContext.Session.Set<UserResponse>(
                    "UsuarioLogueado",
                    userResponse
                );

                return RedirectToAction("Index", "Product");
            }
        }
    }
}
