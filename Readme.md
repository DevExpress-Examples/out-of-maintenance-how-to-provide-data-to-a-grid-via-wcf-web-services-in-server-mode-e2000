<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/E2000/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/E2000/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/E2000/MainWindow.xaml.cs) (VB: [MainWindow.xaml](./VB/E2000/MainWindow.xaml))
* [MessagesWcfDataService.svc.cs](./CS/E2000Web/MessagesWcfDataService.svc.cs) (VB: [MessagesWcfDataService.svc.vb](./VB/E2000Web/MessagesWcfDataService.svc.vb))
<!-- default file list end -->
# How to provide data to a grid via WCF Web Services in server mode


<p>The following example demonstrates how to use WCF (<a href="http://msdn.microsoft.com/en-us/netframework/aa663324.aspx"><u>Windows Communication Foundation</u></a>) to provide data for the DXGrid control. Note that in this case the DXGrid control works in <a href="https://documentation.devexpress.com/#WPF/CustomDocument6279"><u>server mode</u></a>. This guarantees the best possible performance with large datasources for passing data from a server to a client-side DXGrid control. <br><br><strong>Important Note</strong>: For version 11 and lower, this example makes use of the open source <a href="http://www.codeplex.com/interlinq/"><u>InterLINQ library</u></a>. Therefore, if you want to use it in your applications, you should review its <a href="http://interlinq.codeplex.com/license"><u>license</u></a>. For a detailed description of this approach, refer to the following Knowledge Base article: <a href="https://www.devexpress.com/Support/Center/p/K18300">K18300</a></p>

<br/>


