using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineTry
{
	public class AsyncChainableResult<TResult> : Chainable
	{
		private readonly Func<Task<TResult>> func;
		private Action finallyBlock;

		internal AsyncChainableResult(Func<Task<TResult>> tryBlock)
		{
			this.func = tryBlock;
		}

		public AsyncChainableResult<TResult> Catch<TException>() where TException : Exception
		{
			exceptionsToCatch.Add(typeof(TException));
			return this;
		}

		public AsyncChainableResult<TResult> Finally(Action finallyBlock)
		{
			this.finallyBlock = finallyBlock;
			return this;
		}

		public async Task<(TResult Result, Exception Exception)> Run()
		{
			try
			{
				return (await func(), null);
			}
			catch (Exception exception)
			{
				if (IsCatchedException(exception.GetType()))
					return (default(TResult), exception);
				else
					throw;
			}
			finally
			{
				finallyBlock?.Invoke();
			}
		}
	}
}
