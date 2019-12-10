using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStudio.Common;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Twitter;
using AppStudio.DataProviders.LocalStorage;
using VSauceSupreme.Sections;


namespace VSauceSupreme.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems)
        {
            PageTitle = "VSauce Supreme";
            VSauce = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new VSauceConfig(), visibleItems);
            VSauce2 = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new VSauce2Config(), visibleItems);
            VSauce3 = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new VSauce3Config(), visibleItems);
            VSauce2Playlists = new ListViewModel<LocalStorageDataConfig, MenuSchema>(new VSauce2PlaylistsConfig());
            WeSauce = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new WeSauceConfig(), visibleItems);
            Twitter = new ListViewModel<TwitterDataConfig, TwitterSchema>(new TwitterConfig(), visibleItems);
            Actions = new List<ActionInfo>();

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = new RelayCommand(Refresh),
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

        public string PageTitle { get; set; }
        public ListViewModel<YouTubeDataConfig, YouTubeSchema> VSauce { get; private set; }
        public ListViewModel<YouTubeDataConfig, YouTubeSchema> VSauce2 { get; private set; }
        public ListViewModel<YouTubeDataConfig, YouTubeSchema> VSauce3 { get; private set; }
        public ListViewModel<LocalStorageDataConfig, MenuSchema> VSauce2Playlists { get; private set; }
        public ListViewModel<YouTubeDataConfig, YouTubeSchema> WeSauce { get; private set; }
        public ListViewModel<TwitterDataConfig, TwitterSchema> Twitter { get; private set; }

        public RelayCommand<INavigable> SectionHeaderClickCommand
        {
            get
            {
                return new RelayCommand<INavigable>(item =>
                    {
                        NavigationService.NavigateTo(item);
                    });
            }
        }

        public DateTime? LastUpdated
        {
            get
            {
                return GetViewModels().Select(vm => vm.LastUpdated)
                            .OrderByDescending(d => d).FirstOrDefault();
            }
        }

        public List<ActionInfo> Actions { get; private set; }

        public bool HasActions
        {
            get
            {
                return Actions != null && Actions.Count > 0;
            }
        }

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData)
                                        .Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<DataViewModelBase> GetViewModels()
        {
            yield return VSauce;
            yield return VSauce2;
            yield return VSauce3;
            yield return VSauce2Playlists;
            yield return WeSauce;
            yield return Twitter;
        }
    }
}
