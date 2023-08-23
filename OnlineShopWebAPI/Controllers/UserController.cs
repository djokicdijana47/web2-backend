using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Interface;

namespace OnlineShopWebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISendEmailService _emailService;

        public UserController(IUserService userService, ISendEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("getSellers")]
        [Authorize(Roles = "admin")]
        public IActionResult GetSellers()
        {
            return Ok(_userService.GetSellers());
        }

        [HttpGet("getShoppers")]
        [Authorize(Roles = "admin")]
        public IActionResult GetShoppers()
        {
            return Ok(_userService.GetShoppers());
        }


        [HttpGet("getUserData")]
        [Authorize(Roles = "admin, seller, shopper")]
        public IActionResult GetUserData(string email)
        {
            return Ok(_userService.GetUserProfile(email));
        }

        [HttpPost("blockSeller")]
        [Authorize(Roles = "admin")]
        public IActionResult BlockMerchant(string sellerId)
        {
            try
            {
                _userService.BlockSeller(sellerId);
                _emailService.BlockedEmailNotification(sellerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verifySeller")]
        [Authorize(Roles = "admin")]
        public IActionResult VerifySeller(string sellerId)
        {
            try
            {
                _userService.VerifySeller(sellerId);
                _emailService.VerifiedEmailNotification(sellerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("uploadImage"), DisableRequestSizeLimit]
        [Authorize(Roles = "admin, seller, shopper")]
        public async Task<IActionResult> UploadImage([FromQuery] string email)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            return Ok(_userService.UploadImage(file, email));
        }

        [HttpPost("updateAccount")]
        [Authorize(Roles = "admin, seller, shopper")]
        public IActionResult UpdateAccount(UserDto account)
        {
            return Ok(_userService.UpdateAccount(account));
        }

        [HttpPost("updatePassword")]
        public IActionResult UpdatePassword(string email, string oldPassword, string newPassword, string repeatedPassword) 
        {
            bool res = _userService.UpdatePassword(email, oldPassword, newPassword, repeatedPassword);
            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }   

        }

    }
}
