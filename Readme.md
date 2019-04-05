<!-- default file list -->
*Files to look at*:

* [SimpleExampleContext.cs](./CS/DataObjects/SimpleExampleContext.cs) (VB: [SimpleExampleContext.vb](./VB/DataObjects/SimpleExampleContext.vb))
* [ChooseConnectionWindow.xaml](./CS/DXGridSample/ChooseConnectionWindow.xaml) (VB: [ChooseConnectionWindow.xaml](./VB/DXGridSample/ChooseConnectionWindow.xaml))
* [ChooseConnectionWindow.xaml.cs](./CS/DXGridSample/ChooseConnectionWindow.xaml.cs) (VB: [ChooseConnectionWindow.xaml.vb](./VB/DXGridSample/ChooseConnectionWindow.xaml.vb))
* [Window1.xaml](./CS/DXGridSample/Window1.xaml) (VB: [Window1.xaml](./VB/DXGridSample/Window1.xaml))
* [Window1.xaml.cs](./CS/DXGridSample/Window1.xaml.cs) (VB: [Window1.xaml.vb](./VB/DXGridSample/Window1.xaml.vb))
* [GZipMessageEncoderFactory.cs](./CS/GZipEncoder/GZipMessageEncoderFactory.cs) (VB: [GZipMessageEncoderFactory.vb](./VB/GZipEncoder/GZipMessageEncoderFactory.vb))
* [GZipMessageEncodingBindingElement.cs](./CS/GZipEncoder/GZipMessageEncodingBindingElement.cs) (VB: [GZipMessageEncodingBindingElementImporter.vb](./VB/GZipEncoder/GZipMessageEncodingBindingElementImporter.vb))
* [GZipMessageEncodingBindingElementImporter.cs](./CS/GZipEncoder/GZipMessageEncodingBindingElementImporter.cs) (VB: [GZipMessageEncodingBindingElementImporter.vb](./VB/GZipEncoder/GZipMessageEncodingBindingElementImporter.vb))
* [IObjectSource.cs](./CS/InterLinq.Objects/Objects/IObjectSource.cs) (VB: [IObjectSource.vb](./VB/InterLinq.Objects/Objects/IObjectSource.vb))
* [ObjectQueryHandler.cs](./CS/InterLinq.Objects/Objects/ObjectQueryHandler.cs) (VB: [ObjectQueryHandler.vb](./VB/InterLinq.Objects/Objects/ObjectQueryHandler.vb))
* [DXGridService.svc.cs](./CS/WcfSample/DXGridService.svc.cs) (VB: [DXGridService.svc.vb](./VB/WcfSample/DXGridService.svc.vb))
* [OutlookDataGenerator.cs](./CS/WcfSample/OutlookDataGenerator.cs) (VB: [OutlookDataGenerator.vb](./VB/WcfSample/OutlookDataGenerator.vb))
<!-- default file list end -->
# How to provide data to a grid via WCF Web Services in server mode


<p>The following example demonstrates how to use WCF (<a href="http://msdn.microsoft.com/en-us/netframework/aa663324.aspx"><u>Windows Communication Foundation</u></a>) to provide data for the DXGrid control. Note that in this case the DXGrid control works in <a href="https://documentation.devexpress.com/#WPF/CustomDocument6279"><u>server mode</u></a>. This guarantees the best possible performance with large datasources for passing data from a server to a client-side DXGrid control. <br><br><strong>Important Note</strong>: For version 11 and lower, this example makes use of the open source <a href="http://www.codeplex.com/interlinq/"><u>InterLINQ library</u></a>. Therefore, if you want to use it in your applications, you should review its <a href="http://interlinq.codeplex.com/license"><u>license</u></a>. For a detailed description of this approach, refer to the following Knowledge Base article: <a href="https://www.devexpress.com/Support/Center/p/K18300">K18300</a></p>

<br/>


