using System.Windows;

namespace DXGridSample {

    public partial class ChooseConnectionWindow : Window {
        public ChooseConnectionWindow() {
            InitializeComponent();
        }

        private void localButton_Click(object sender, RoutedEventArgs e) {
            Tag = true;
            Close();
        }

        private void internetButton_Click(object sender, RoutedEventArgs e) {
            Tag = false;
            Close();
        }
    }
}
