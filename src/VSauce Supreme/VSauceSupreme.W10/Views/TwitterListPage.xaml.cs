using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.Twitter;
using VSauceSupreme;
using VSauceSupreme.Sections;
using VSauceSupreme.ViewModels;

namespace VSauceSupreme.Views
{
    public sealed partial class TwitterListPage : PageBase
    {
        public TwitterListPage()
        {
            this.ViewModel = new ListViewModel<TwitterDataConfig, TwitterSchema>(new TwitterConfig());
            this.InitializeComponent();
}

        public ListViewModel<TwitterDataConfig, TwitterSchema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
