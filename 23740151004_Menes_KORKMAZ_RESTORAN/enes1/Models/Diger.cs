using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Models
{
    [Area("Admin"),Authorize(Policy= "AdminPolicy")]
    public static class Diger
    {
        public const string Role_Admin = "Admin";
    }
}
