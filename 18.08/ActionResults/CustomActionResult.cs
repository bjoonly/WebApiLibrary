using _18._08.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.ActionResults
{
    public class CustomActionResult : IActionResult
    {
        private readonly CustomActionResultVM _customResult;
        public CustomActionResult(CustomActionResultVM customResult)
        {
            _customResult = customResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var obj = new ObjectResult(_customResult.Exception ?? _customResult.Publisher as object)
            {
                StatusCode = _customResult.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };
            await obj.ExecuteResultAsync(context);
        }

    }

}

