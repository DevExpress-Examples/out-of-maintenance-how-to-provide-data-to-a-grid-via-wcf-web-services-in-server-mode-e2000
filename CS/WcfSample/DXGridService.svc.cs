using System;
using System.Data.Linq;
using System.ServiceModel;
using System.Web.Configuration;
using DataObjects;
using InterLinq;
using InterLinq.Communication.Wcf;
using InterLinq.Objects;
using InterLinq.Sql;

namespace WcfSample {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode =
        InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class TestServerQueryWcfHandler : IQueryRemoteHandler {

        static IQueryHandler CreateHandler() {
            string connectionString = 
                WebConfigurationManager.ConnectionStrings["DXGridServerModeDB"].ConnectionString;

            if (!string.IsNullOrEmpty(connectionString)) {
                DataContext dataContext = new DataGridTestClassesDataContext(connectionString);
                return new SqlQueryHandler(dataContext);
            }
            else {
                ExampleObjectSource exampleObjectSource = new ExampleObjectSource();

                for (int i = 0; i < 30000; i++) {
                    WpfServerSideGridTest obj = OutlookDataGenerator.CreateNewObject();
                    obj.OID = i + 1;
                    exampleObjectSource.Objects.Add(obj);
                }

                return new ObjectQueryHandler(exampleObjectSource);
            }
        }

        IQueryRemoteHandler handler;
        bool isServerMode;

        public TestServerQueryWcfHandler() {
            IQueryHandler innerHandler = CreateHandler();
            isServerMode = innerHandler is SqlQueryHandler;
            handler = new ServerQueryWcfHandler(innerHandler);
        }

        #region IQueryRemoteHandler Members

        public object Retrieve(InterLinq.Expressions.SerializableExpression expression) {
            long tickCount = Environment.TickCount;
            object obj = handler.Retrieve(expression);
            return new SeverInfoObject() {
                IsServerMode = isServerMode,
                Milliseconds = (int)(Environment.TickCount - tickCount),
                Obj = obj
            };
        }

        #endregion
    }
}
