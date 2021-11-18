using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.Common
{
    public class GenericAPIResponse<T> : APIResponse
    {
        public T Data { get; set; }
        public string Token { get; set; }
        public GenericAPIResponse()
        {

        }

        public GenericAPIResponse(T data)
        {
            this.Data = data;
        }
        public GenericAPIResponse(T data, string token)
        {
            this.Data = data;
            this.Token = token;
        }


        public GenericAPIResponse(Exception ex) : base(ex)
        {

        }
    }
}
