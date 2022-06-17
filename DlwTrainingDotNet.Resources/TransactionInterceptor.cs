using Castle.DynamicProxy;

namespace DlwTrainingDotNet.Resources
{
    public class TransactionInterceptor : IInterceptor
    {
        private readonly AsyncTransactionInterceptor _asyncTransactionInterceptor;

        public TransactionInterceptor(AsyncTransactionInterceptor asyncTransactionInterceptor)
        {
            _asyncTransactionInterceptor = asyncTransactionInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            _asyncTransactionInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}