using Microsoft.AspNetCore.Mvc;
using WiseMaestroRBAC.Models;
using WiseMaestroRBAC.Authorization;
using WiseMaestroRBAC.Services;

namespace WiseMaestroRBAC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        [HttpGet("viewer-content")]
        [RequireRole(UserRoles.Viewer)]
        public IActionResult GetViewerContent()
        {
            return Ok("Content for all authenticated users");
        }

        [HttpPost("editor-action")]
        [RequireRole(UserRoles.Editor)]
        public IActionResult EditorAction()
        {
            return Ok("Editor action performed");
        }

        [HttpPut("admin-update")]
        [RequireRole(UserRoles.Admin)]
        public IActionResult AdminUpdate()
        {
            return Ok("Admin update performed");
        }

        [HttpDelete("super-admin-delete")]
        [RequireRole(UserRoles.SuperAdmin)]
        public IActionResult SuperAdminDelete()
        {
            return Ok("Super Admin delete performed");
        }
    }
}
