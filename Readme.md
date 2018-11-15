<!-- default file list -->
*Files to look at*:

* [Window1.xaml](./CS/DXGridSample/Window1.xaml) (VB: [Window1.xaml](./VB/DXGridSample/Window1.xaml))
* [Window1.xaml.cs](./CS/DXGridSample/Window1.xaml.cs) (VB: [Window1.xaml](./VB/DXGridSample/Window1.xaml))
* [DXGridService.svc](./CS/WcfSample/DXGridService.svc) (VB: [DXGridService.svc](./VB/WcfSample/DXGridService.svc))
* [DXGridService.svc.cs](./CS/WcfSample/DXGridService.svc.cs) (VB: [DXGridService.svc](./VB/WcfSample/DXGridService.svc))
<!-- default file list end -->
# How to provide data to a grid via WCF Web Services in server mode


<p>The following example demonstrates how to use WCF (<a href="http://msdn.microsoft.com/en-us/netframework/aa663324.aspx"><u>Windows Communication Foundation</u></a>) to provide data for the DXGrid control. Note that in this case the DXGrid control works in <a href="https://documentation.devexpress.com/#WPF/CustomDocument6279"><u>server mode</u></a>. This guarantees the best possible performance with large datasources for passing data from a server to a client-side DXGrid control. <br><br><strong>Important Note</strong>: For version 11 and lower, this example makes use of the open source <a href="http://www.codeplex.com/interlinq/"><u>InterLINQ library</u></a>. Therefore, if you want to use it in your applications, you should review its <a href="http://interlinq.codeplex.com/license"><u>license</u></a>. For a detailed description of this approach, refer to the following Knowledge Base article: <a href="https://www.devexpress.com/Support/Center/p/K18300">K18300</a></p>


<h3>Description</h3>

<p>To see this approach in action, click the &quot;Download Source Code&quot; button on this page. After the solution is opened in Visual Studio, it might be required to set the &quot;DXGridSample&quot; project item as the Start Up project. You can then run the application.</p><p>This application has two modes:</p><p>- <strong>Local WCF Service</strong>. In this case, the grid obtains data from a local web service, which starts when the application runs.</p><p>To see how the web service obtains data from a local SQL server, you will first need to create a &quot;DXGridServerModeDB&quot; database on this server. This can be done by clicking the &quot;Generate table and Start Demo&quot; button on the Data Binding / LINQ Server Mode module of the DXGrid main demo. After that, you will need to assign the correct connection string for your environment for the &quot;DXGridServerModeDB&quot; connection in the Web.config file of this web service. By default, this is set to &quot;data source=(local);integrated security=SSPI;initial catalog=DXGridServerModeDB&quot;.</p><p>If the connection string isn&#39;t specified, the web service will automatically create a small sample datasource, but in this case you won&#39;t be able to see the benefits of server mode (for example, the performance during grouping, filtering and scrolling).</p><p>- <strong>Internet WCF Service</strong>. In this case, the grid obtains data from a web service located at the DevExpress Web Site (<a href="http://demos.devexpress.com/DXGridServerModeService/DXGridService.svc">http://demos.devexpress.com/DXGridServerModeService/DXGridService.svc</a>).</p><p>This mode emulates a real-life application where a client machine is located anywhere all over the world, and where it obtains data from a web service which resides on a public host and which has a connection to a data server located in its private network.</p>

<br/>


