using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using DataObjects;
using DevExpress.Data.Linq;
using InterLinq;
using InterLinq.Communication;
using InterLinq.Objects;
using Microsoft.ServiceModel.Samples;

namespace DXGridSample {

    public partial class Window1 : Window {

        public Window1() {
            InitializeComponent();

            ChooseConnectionWindow connectionWindow = new ChooseConnectionWindow();
            connectionWindow.ShowDialog();

            bool useLocalConnection = (bool)connectionWindow.Tag;
            string configName = "InterLinqServiceNetTcp" + (useLocalConnection ? "_local" : string.Empty);
            QueryRemoteHandlerWrapper wrapper = 
                new QueryRemoteHandlerWrapper(new ChannelFactory<IQueryRemoteHandler>(configName).CreateChannel());
            ClientQueryHandler clientQueryHandler = 
                new ClientQueryHandler(wrapper);
            SimpleExampleContext simpleExampleContext = 
                new SimpleExampleContext(clientQueryHandler);
            
            LinqServerModeSource collectionSource = new LinqServerModeSource() {
                ElementType = typeof(WpfServerSideGridTest),
                QueryableSource = simpleExampleContext.Objects,
                KeyExpression = "OID"
            };

            grid.ItemsSource = collectionSource;

            infoContainer.DataContext = wrapper;
            scrollViewer.ScrollToEnd();
            logHeaderText.Text = 
                "ServerProcessingTime(ms)\tTotalProcessingTime(ms)\tRequestSize(bytes)\tResponseSize(bytes)";
        }
    }
    public class QueryInfo {
        public int RequestSize { get; set; }
        public int ResponseSize { get; set; }
    }

    public class QueryRemoteHandlerWrapper : DependencyObject, IQueryRemoteHandler {
        public int TotalBytesSent {
            get { return (int)GetValue(TotalBytesSentProperty); }
            set { SetValue(TotalBytesSentProperty, value); }
        }
        
        public static readonly DependencyProperty TotalBytesSentProperty =
            DependencyProperty.Register("TotalBytesSent", typeof(int), typeof(QueryRemoteHandlerWrapper), 
            new UIPropertyMetadata(0));
        
        public int TotalBytesReceived {
            get { return (int)GetValue(TotalBytesReceivedProperty); }
            set { SetValue(TotalBytesReceivedProperty, value); }
        }

        public static readonly DependencyProperty TotalBytesReceivedProperty =
            DependencyProperty.Register("TotalBytesReceived", typeof(int), typeof(QueryRemoteHandlerWrapper), 
            new UIPropertyMetadata(0));
        
        public bool NotInServerMode {
            get { return (bool)GetValue(NotInServerModeProperty); }
            set { SetValue(NotInServerModeProperty, value); }
        }

        public static readonly DependencyProperty NotInServerModeProperty =
            DependencyProperty.Register("NotInServerMode", typeof(bool), typeof(QueryRemoteHandlerWrapper), 
            new UIPropertyMetadata(false));

        const int MaxLogSize = 20;
        readonly IQueryRemoteHandler handler;
        public ObservableCollection<string> Log { get; private set; }
        QueryInfo currentInfo;

        public QueryRemoteHandlerWrapper(IQueryRemoteHandler handler) {
            GZipMessageEncoderFactory.MessageReceived += 
                new MeassageSizeEventHandler(GZipMessageEncoderFactory_MessageReceived);
            GZipMessageEncoderFactory.MessageSent += 
                new MeassageSizeEventHandler(GZipMessageEncoderFactory_MessageSent);

            this.handler = handler;

            Log = new ObservableCollection<string>();
            for (int i = 0; i < MaxLogSize; i++) {
                Log.Add(string.Empty);
            }
        }

        void GZipMessageEncoderFactory_MessageSent(object sender, MessageSizeEventArgs e) {
            currentInfo.RequestSize = e.MessageSize;
        }

        void GZipMessageEncoderFactory_MessageReceived(object sender, MessageSizeEventArgs e) {
            currentInfo.ResponseSize = e.MessageSize;
        }

        #region IQueryRemoteHandler Members
        public object Retrieve(InterLinq.Expressions.SerializableExpression expression) {
            currentInfo = new QueryInfo();
            long tickCount = Environment.TickCount;
            SeverInfoObject pair = (SeverInfoObject)handler.Retrieve(expression);
            RefreshLog(pair.Milliseconds, (int)(Environment.TickCount - tickCount), pair.IsServerMode);

            return pair.Obj;
        }

        void RefreshLog(int serverProcessingTime, int totalProcessingTime, bool isServerMode) {
            string logEntry = string.Format("{0:### ##0}\t\t\t\t{1:### ##0}\t\t\t\t{2:### ##0}\t\t\t{3:### ##0}",
                serverProcessingTime, totalProcessingTime, currentInfo.RequestSize, currentInfo.ResponseSize);

            for (int i = 0; i < MaxLogSize - 1; i++) {
                Log[i] = Log[i + 1];
            }

            Log[MaxLogSize - 1] = logEntry;
            TotalBytesSent += currentInfo.RequestSize;
            TotalBytesReceived += currentInfo.ResponseSize;
            NotInServerMode = !isServerMode;
        }
        #endregion
    }
}
