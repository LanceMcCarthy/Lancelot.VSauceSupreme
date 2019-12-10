using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Menu;
using VSauceSupreme.Config;
using VSauceSupreme.ViewModels;

namespace VSauceSupreme.Sections
{
    public class VSauce2PlaylistsConfig : SectionConfigBase<LocalStorageDataConfig, MenuSchema>
    {
        public override DataProviderBase<LocalStorageDataConfig, MenuSchema> DataProvider => new LocalStorageDataProvider<MenuSchema>();

        public override LocalStorageDataConfig Config =>
            new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/VSauce2Playlists.json"
            };

        public override NavigationInfo ListNavigationInfo => NavigationInfo.FromPage("VSauce2PlaylistsListPage");


        public override ListPageConfig<MenuSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<MenuSchema>
                {
                    Title = "VSauce2 Playlists",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title;
                        viewModel.Image = item.Icon;
                    },
                    NavigationInfo = (item) =>
                    {
                        return item.ToNavigationInfo();
                    }
                };
            }
        }

        public override DetailPageConfig<MenuSchema> DetailPage => null;

        public override string PageTitle => "VSauce2 Playlists";
    }
}
