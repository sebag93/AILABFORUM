using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(AILABFORUM.Startup))]

namespace AILABFORUM
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Aby uzyskać więcej informacji o sposobie konfigurowania aplikacji, odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
