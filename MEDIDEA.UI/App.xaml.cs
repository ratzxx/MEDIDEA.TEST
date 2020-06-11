using System;
using System.Windows;
using System.Windows.Threading;
using MEDIDEA.Infrastructure;

namespace MEDIDEA.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            Bootstrapper.BuildContainer();
            await MedideaContextSeed.SeedAsync();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = $"Error: {e.Exception.Message}";
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)

        {
            var exception = e.ExceptionObject as Exception;
            var terminatingMessage = e.IsTerminating ? " The application is terminating." : string.Empty;
            var exceptionMessage = exception?.Message ?? "An unmanaged exception occured.";
            MessageBox.Show(exceptionMessage, terminatingMessage);
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
