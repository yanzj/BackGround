using System;
using System.Collections.Generic;

namespace Lte.Domain.LinqToCsv
{
    public sealed class AggregatedException : LinqToCsvException
    {
        public readonly List<Exception> MInnerExceptionsList;
        private readonly int _mMaximumNbrExceptions;

        public AggregatedException(string typeName, string fileName, int maximumNbrExceptions) :
            base(string.Format(
                "There were 1 or more exceptions while reading data using type \"{0}\"." +
                FileNameMessage(fileName), typeName))
        {
            _mMaximumNbrExceptions = maximumNbrExceptions;
            MInnerExceptionsList = new List<Exception>();


            Data["TypeName"] = typeName;
            Data["FileName"] = fileName;
            Data["InnerExceptionsList"] = MInnerExceptionsList;
        }

        public void AddException(Exception e)
        {
            MInnerExceptionsList.Add(e);
            if ((_mMaximumNbrExceptions != -1) &&
                (MInnerExceptionsList.Count >= _mMaximumNbrExceptions))
            {
                throw this;
            }
        }

        public void ThrowIfExceptionsStored()
        {
            if (MInnerExceptionsList.Count > 0)
            {
                throw this;
            }
        }
    }
}