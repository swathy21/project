using System;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Controllers
{
    public interface IUsersController
    {
        public IActionResult Post(LoginRequest request);
        public IActionResult GetAll();

    }
}