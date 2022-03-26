using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineTry
{
	public class ChainableAction : Chainable
	{
		private readonly Action action;
		private Action finallyBlock;

		internal ChainableAction(Action action)
		{
			this.action = action;
		}

		public ChainableAction Catch<TException>() where TException : Exception
		{
			exceptionsToCatch.Add(typeof(TException));
			return this;
		}

		public ChainableAction Finally(Action finallyBlock)
		{
			this.finallyBlock = finallyBlock;
			return this;
		}

		public Exception Run()
		{
			try
			{
				action();
				return null;
			}
			catch(Exception exception)
			{
				if (IsCatchedException(exception.GetType()))
					return exception;
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
