using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatStuff.Core.Result;
public class OperationResult<T>
{
    public bool Succeeded { get; set; }
    public T Data { get; set; }
    public string ErrorMessage { get; set; }

    public static OperationResult<T> Success(T data) => new OperationResult<T> { Succeeded = true, Data = data };
    public static OperationResult<T> Failure(string errorMessage) => new OperationResult<T> { Succeeded = false, ErrorMessage = errorMessage };
}