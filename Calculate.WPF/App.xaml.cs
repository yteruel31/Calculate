using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Squirrel;

namespace Calculate.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CheckForUpdate();
        }

        private async Task CheckForUpdate()
        {
            using (var manager = new UpdateManager(@"C:\Temp\Releases"))
            {
                await manager.UpdateApp();
            }
        }
    }
}