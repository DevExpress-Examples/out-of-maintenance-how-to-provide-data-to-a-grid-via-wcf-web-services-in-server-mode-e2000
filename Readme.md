<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128652549/12.2.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2000)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/E2000/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/E2000/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/E2000/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/E2000/MainWindow.xaml.vb))
* [MessagesWcfDataService.svc.cs](./CS/E2000Web/MessagesWcfDataService.svc.cs) (VB: [MessagesWcfDataService.svc.vb](./VB/E2000Web/MessagesWcfDataService.svc.vb))
<!-- default file list end -->
# How to provide data to a grid via WCF Web Services in server mode


<p>The following example demonstrates how to use WCF (<a href="http://msdn.microsoft.com/en-us/netframework/aa663324.aspx"><u>Windows Communication Foundation</u></a>) to provide data for the DXGrid control. Note that in this case the DXGrid control works in <a href="https://documentation.devexpress.com/#WPF/CustomDocument6279"><u>server mode</u></a>. This guarantees the best possible performance with large datasources for passing data from a server to a client-side DXGrid control. 


