using Bank.Domain.Entities;
using Bank.Service.DTOs;
using Bank.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Bank.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var users = service.GetAllAsync();
            return View(users.Result); 
        }

        [HttpPost]
        public async Task<IActionResult> Create (UserForCreationDto dto)
        {
            var addedUser = await service.AddAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await service.GetAsync(u => u.Id == id);
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await service.DeleteAsync(u => u.Id == id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }
        public async Task<IActionResult> Update(int id, UserForCreationDto dto)
        {
            var updateUser = await service.UpdateAsync(u => u.Id == id, dto);
            return RedirectToAction("Index");
        }

    }
}
