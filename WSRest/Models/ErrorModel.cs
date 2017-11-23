using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSRest.Models
{
    [Serializable]
    public class ErrorModel
    {
        public int ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public ErrorModel(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}