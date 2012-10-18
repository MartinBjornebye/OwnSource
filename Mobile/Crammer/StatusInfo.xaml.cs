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
    public partial class StatusInfo : PhoneApplicationPage
    {
        public StatusInfo()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State[CrammerConstants.OPTION_CHOSEN] = "Status";
            this.NavigationService.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            if (parameters.ContainsKey("Total"))
            {
                txtTotalEntries.Text = parameters["Total"];
                txtActive.Text = parameters["Active"];
                //txtCompleted.Text = parameters["Completed"];
            }
        }

    }
}