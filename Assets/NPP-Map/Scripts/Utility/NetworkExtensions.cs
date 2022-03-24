using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace NPPMap.Utility
{
    public class UnityWebRequestAwaiter : INotifyCompletion
    {
        private readonly UnityWebRequestAsyncOperation _asyncOperation;
        private Action _continuation;


        public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOperation)
        {
            _asyncOperation = asyncOperation;
            if (!asyncOperation.isDone)
            {
                asyncOperation.completed += OnOperationCompleted;
            }
        }

        public bool IsCompleted => _asyncOperation.isDone;


        public void OnCompleted(Action action) => _continuation = action;


        public void GetResult()
        {
        }


        private void OnOperationCompleted(AsyncOperation operation) => _continuation?.Invoke();
    }


    public static class ExtensionMethods
    {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            return new UnityWebRequestAwaiter(asyncOp);
        }
    }
}