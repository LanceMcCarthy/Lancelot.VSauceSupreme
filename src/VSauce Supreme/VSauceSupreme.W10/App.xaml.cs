using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using VSauceSupreme.Services;

namespace VSauceSupreme
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Windows.UI.Xaml.Application
    {
        private Guid APP_ID = new Guid("871d6893-8af7-4842-84f8-e6472879b9a7");

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            GetAppData();
            UpdateAppTiles();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(Shell), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void GetAppData()
        {
            string deviceType = IsOnPhoneExecution() ? LocalSettingNames.PhoneValue : LocalSettingNames.WindowsValue;
            ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] = deviceType;
            ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] = ValidateStoreId();
        }

        private bool IsOnPhoneExecution()
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            return (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
        }

        private Guid ValidateStoreId()
        {
            try
            {
                Guid storeId = CurrentApp.AppId;
                if (storeId != Guid.Empty && storeId != APP_ID)
                {
                    return storeId;
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        private void UpdateAppTiles()
        {
            var init = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.TilesInitialized];
            if (init == null || (init is bool && !(bool)init))
            {
                TileServices.CreateFlipTile(@"VSauce Supreme", @"All the VSauces in one app!");

                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.TilesInitialized] = true;
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
