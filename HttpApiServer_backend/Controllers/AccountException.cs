using System.Collections;

namespace HttpApiServer_backend.Controllers
{
    public class AccountException : Exception
    {
        public override string Message => "Login or Email already exist!";
    }
}
