using Helpers;
using Models;
using System.Security.Claims;

namespace Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;

        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? token = context.Request.Headers["Authorization"];

            if (token is not null)
            {
                int index = token.IndexOf(' ') + 1;
                token = token.Substring(index);
                string? email = AuthHelper.TokenDecode(token)?.First(claim => claim.Type == ClaimTypes.Email).Value;

                Console.WriteLine(context.Request.Headers);

                if (email is null)
                {
                    context.Request.Headers.Remove("Authorization");
                }

                ShopDb db = new ShopDb();
                UserToken? userToken = db.UserTokens.FirstOrDefault(option =>
                    option.UserEmail == email &&
                    option.AccessToken == token
                );

                if (userToken is null)
                {
                    context.Request.Headers.Remove("Authorization");
                }

                if (userToken?.IsActive == 0)
                {
                    context.Request.Headers.Remove("Authorization");
                }
            }

            await next.Invoke(context);
        }
    }
}
