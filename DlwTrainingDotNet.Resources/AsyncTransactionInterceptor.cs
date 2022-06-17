using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace DlwTrainingDotNet.Resources
{
    public class AsyncTransactionInterceptor : AsyncInterceptorBase, IDisposable
    {
        private readonly DlwTrainingContext _dlwTrainingContext;
        private readonly ILogger<TransactionInterceptor> _logger;

        public AsyncTransactionInterceptor(DlwTrainingContext dlwTrainingContext, ILogger<TransactionInterceptor> logger)
        {
            _dlwTrainingContext = dlwTrainingContext;
            _logger = logger;
        }
        
        public void Dispose()
        {
            if (_dlwTrainingContext.Database.CurrentTransaction != null)
                _dlwTrainingContext.Database.CommitTransaction();
            _dlwTrainingContext.Dispose();
        }
        
        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            if (_dlwTrainingContext.Database.CurrentTransaction == null)
                await _dlwTrainingContext.Database.BeginTransactionAsync();

            try
            {
                await proceed(invocation, proceedInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await _dlwTrainingContext.Database.RollbackTransactionAsync();
                throw;
            }
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            if (_dlwTrainingContext.Database.CurrentTransaction == null)
                await _dlwTrainingContext.Database.BeginTransactionAsync();

            try
            {
                return await proceed(invocation, proceedInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await _dlwTrainingContext.Database.RollbackTransactionAsync();
                throw;
            }
        }
    }
}