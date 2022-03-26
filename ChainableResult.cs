using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineTry
{
	public class ChainableResult<TResult> : Chainable
	{
		private readonly Func<TResult> func;
		private Action finallyBlock;

		internal ChainableResult(Func<TResult> func)
		{
			this.func = func;
		}

		public ChainableResult<TResult> Catch<TException>() where TException : Exception
		{
			exceptionsToCatch.Add(typeof(TException));
			return this;
		}

		public ChainableResult<TResult> Finally(Action finallyBlock)
		{
			this.finallyBlock = finallyBlock;
			return this;
		}

		public (TResult Result, Exception Exception) Run()
		{
			try
			{
				return (func(), null);
			}
			catch(Exception exception)
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
