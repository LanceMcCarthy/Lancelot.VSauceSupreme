using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.YouTube;
using VSauceSupreme;
using VSauceSupreme.Sections;
using VSauceSupreme.ViewModels;

namespace VSauceSupreme.Views
{
    public sealed partial class VSauce2ListPage : PageBase
    {
        public VSauce2ListPage()
        {
            this.ViewModel = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new VSauce2Config());
            this.InitializeComponent();
}

        public ListViewModel<YouTubeDataConfig, YouTubeSchema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
