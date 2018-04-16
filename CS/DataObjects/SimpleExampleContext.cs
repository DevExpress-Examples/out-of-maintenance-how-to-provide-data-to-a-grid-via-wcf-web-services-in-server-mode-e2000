using System.Linq;
using InterLinq;

namespace DataObjects
{
    /// <summary>
    /// This is a context for easier access to the <see cref="IQueryHandler"/>.
    /// </summary>
    public class SimpleExampleContext : InterLinqContext
    {
        public SimpleExampleContext(IQueryHandler queryHandler)
            : base(queryHandler) {
        }

        public IQueryable<WpfServerSideGridTest> Objects {
            get { return QueryHander.Get<WpfServerSideGridTest>(); }
        }

    }
}
