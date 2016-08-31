using Microsoft.Owin;
using Owin;

namespace Librarian
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
