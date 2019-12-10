using System;
using System.Collections.Generic;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.YouTube;
using VSauceSupreme.Config;
using VSauceSupreme.ViewModels;

namespace VSauceSupreme.Sections
{
    public class FAKConfig : SectionConfigBase<YouTubeDataConfig, YouTubeSchema>
    {
        public override DataProviderBase<YouTubeDataConfig, YouTubeSchema> DataProvider
        {
            get
            {
                return new YouTubeDataProvider(new YouTubeOAuthTokens
                {
                    ApiKey = ApiKeys.YouTube
                });
            }
        }

        public override YouTubeDataConfig Config
        {
            get
            {
                return new YouTubeDataConfig
                {
                    QueryType = YouTubeQueryType.Playlist,
                    Query = @"PL2391259B3E2211F7",
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("FAKListPage");
            }
        }

        public override ListPageConfig<YouTubeSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<YouTubeSchema>
                {
                    Title = "FAK",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = "";
                        viewModel.Description = "";
                        viewModel.Image = item.ImageUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return NavigationInfo.FromPage("FAKDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<YouTubeSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, YouTubeSchema>>();

                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = null;
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.Image = null;
                    viewModel.Content = item.EmbedHtmlFragment;
                });

				var actions = new List<ActionConfig<YouTubeSchema>>
				{
                    ActionConfig<YouTubeSchema>.Link("Go To Source", (item) => item.ExternalUrl.ToSafeString()),
				};

                return new DetailPageConfig<YouTubeSchema>
                {
                    Title = "FAK",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        public override string PageTitle
        {
            get { return "FAK"; }
        }

    }
}
