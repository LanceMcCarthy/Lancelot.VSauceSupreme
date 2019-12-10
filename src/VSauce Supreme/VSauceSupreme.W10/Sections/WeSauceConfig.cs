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
    public class WeSauceConfig : SectionConfigBase<YouTubeDataConfig, YouTubeSchema>
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
                    QueryType = YouTubeQueryType.Channels,
                    Query = @"wesauce",
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("WeSauceListPage");
            }
        }

        public override ListPageConfig<YouTubeSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<YouTubeSchema>
                {
                    Title = "WeSauce",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = "";
                        viewModel.Description = null;
                        viewModel.Image = item.ImageUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return NavigationInfo.FromPage("WeSauceDetailPage", true);
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
                    Title = "WeSauce",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        public override string PageTitle
        {
            get { return "WeSauce"; }
        }

    }
}
