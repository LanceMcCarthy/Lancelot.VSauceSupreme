using System;
using System.Collections.Generic;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using VSauceSupreme.Config;
using VSauceSupreme.ViewModels;

namespace VSauceSupreme.Sections
{
    public class TwitterConfig : SectionConfigBase<TwitterDataConfig, TwitterSchema>
    {
        public override DataProviderBase<TwitterDataConfig, TwitterSchema> DataProvider =>
            new TwitterDataProvider(new TwitterOAuthTokens
            {
                ConsumerKey = ApiKeys.TwitterConsumerKey,
                ConsumerSecret = ApiKeys.TwitterConsumerSecret,
                AccessToken = ApiKeys.TwitterToken,
                AccessTokenSecret = ApiKeys.TwitterTokenSecret
            });

        public override TwitterDataConfig Config =>
            new TwitterDataConfig
            {
                QueryType = TwitterQueryType.Search,
                Query = @"#vsauce"
            };

        public override NavigationInfo ListNavigationInfo => NavigationInfo.FromPage("TwitterListPage");


        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "Twitter",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.Description = null;
                        viewModel.Image = item.UserProfileImageUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return new NavigationInfo
                        {
                            NavigationType = NavigationType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage => null;

        public override string PageTitle => "Twitter";
    }
}
