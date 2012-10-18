using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;

using MB.Crammer;

namespace Crammer
{
    public partial class OptionsPage : PhoneApplicationPage
    {
        #region Attributes
        //private CrammerDictionary mCurrentDictionaryRef = null;
        //private PickEngine mEntryEngineRef = null;
        //private MainPage mMainPage = null;
        #endregion

        public OptionsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void cmdStartOver_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "StartOver";
            this.NavigationService.GoBack();
        }

        private void cmdShuffle_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "Shuffle";
            this.NavigationService.GoBack();
        }

        private void cmdFlipSequence_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "Flip";
            this.NavigationService.GoBack();
        }

        private void cmdAscending_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "Ascending";
            this.NavigationService.GoBack();
        }

        private void cmdDescending_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "Descending";
            this.NavigationService.GoBack();
        }

        private void cmdTimestampAscending_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "TimestampAscending";
            this.NavigationService.GoBack();
        }

        private void cmdTimestampDescending_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "TimestampDescending";
            this.NavigationService.GoBack();
        }

        private void cmdNewDictionary_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/NewDictionary.xaml", UriKind.Relative));
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/EditEntries.xaml", UriKind.Relative));
        }

        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/OpenDictionary.xaml", UriKind.Relative));
        }


    }
}