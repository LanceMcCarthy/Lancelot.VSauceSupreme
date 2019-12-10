using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppStudio.Common.Navigation;
using Windows.UI.Xaml;

namespace VSauceSupreme.Navigation
{
    public class AppNavigation
    {
        private NavigationNode _active;

        static AppNavigation()
        {

        }

        public NavigationNode Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (_active != null)
                {
                    _active.IsSelected = false;
                }
                _active = value;
                if (_active != null)
                {
                    _active.IsSelected = true;
                }
            }
        }


        public ObservableCollection<NavigationNode> Nodes { get; private set; }

        public void LoadNavigation()
        {
            Nodes = new ObservableCollection<NavigationNode>();

            Nodes.Add(new ItemNavigationNode
            {
                Title = @"VSauce Supreme",
                Label = "Home",
                IsSelected = true,
                NavigationInfo = NavigationInfo.FromPage("HomePage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "VSauce",
                NavigationInfo = NavigationInfo.FromPage("VSauceListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "VSauce2",
                NavigationInfo = NavigationInfo.FromPage("VSauce2ListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "VSauce3",
                NavigationInfo = NavigationInfo.FromPage("VSauce3ListPage")
            });

            Nodes.Add(new GroupNavigationNode
            {
                Label = "VSauce2 Playlists",
                Visibility = Visibility.Visible,
                Nodes = new ObservableCollection<NavigationNode>()
                {
                    new ItemNavigationNode
                    {
                        Label = "Mind Blow",
                        NavigationInfo = NavigationInfo.FromPage("MindBlowListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "FAK",
                        NavigationInfo = NavigationInfo.FromPage("FAKListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "54321",
                        NavigationInfo = NavigationInfo.FromPage("Section1ListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "Weirdos of the Month",
                        NavigationInfo = NavigationInfo.FromPage("WeirdosOfTheMonthListPage")
                    },
                }
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "WeSauce",
                NavigationInfo = NavigationInfo.FromPage("WeSauceListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Twitter",
                NavigationInfo = NavigationInfo.FromPage("TwitterListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "About",
                NavigationInfo = NavigationInfo.FromPage("AboutPage")
            });
            Nodes.Add(new ItemNavigationNode
            {
                Label = "Privacy terms",
                NavigationInfo = new NavigationInfo()
                {
                    NavigationType = NavigationType.DeepLink,
                    TargetUri = new Uri("http://w8privacy.azurewebsites.net/privacy?dev=Lancelot%20Software&app=VSauce%20Supreme&mail=bGFuY2Vsb3RAb3V0bG9vay5jb20=&lng=En", UriKind.Absolute)
                }
            });
        }

        public NavigationNode FindPage(Type pageType)
        {
            return GetAllItemNodes(Nodes).FirstOrDefault(n => n.NavigationInfo.NavigationType == NavigationType.Page && n.NavigationInfo.TargetPage == pageType.Name);
        }

        private IEnumerable<ItemNavigationNode> GetAllItemNodes(IEnumerable<NavigationNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node is ItemNavigationNode)
                {
                    yield return node as ItemNavigationNode;
                }
                else if(node is GroupNavigationNode)
                {
                    var gNode = node as GroupNavigationNode;

                    foreach (var innerNode in GetAllItemNodes(gNode.Nodes))
                    {
                        yield return innerNode;
                    }
                }
            }
        }
    }
}
