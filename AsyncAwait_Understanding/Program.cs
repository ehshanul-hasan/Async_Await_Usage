using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait_Understanding
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            MainProxy();
            Console.WriteLine($"Main method! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        static async void MainProxy()
        {
            Console.WriteLine($"Main proxy method started! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            var FirstAwaiterTask =  FirstAwaiterProxy();
            Console.WriteLine($"Main proxy method processing! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            var SecondAwaiterTask = SecondAwaiterProxy();
            await Task.WhenAll(FirstAwaiterTask, SecondAwaiterTask);
            Console.WriteLine($"Main proxy method completed! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task FirstAwaiterProxy()
        {
            Console.WriteLine($"FirstAwaiterProxy method started! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            await MethodOne();
            Console.WriteLine($"FirstAwaiterProxy method completed! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task SecondAwaiterProxy()
        {
            Console.WriteLine($"SecondAwaiterProxy method started! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            await MethodTwo();
            Console.WriteLine($"SecondAwaiterProxy method completed! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task<int> MethodOne()
        {
            
            var responseString = await client.GetStringAsync("https://deelay.me/1000/https://reqres.in/api/users?page=2");
            Console.WriteLine($"Method One! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}. Length : {responseString.Length}");
            return 0;
        }

        static async Task<int> MethodTwo()
        {
            var responseString = await client.GetStringAsync("https://deelay.me/3000/https://reqres.in/api/users?page=2");
            Console.WriteLine($"Method Two! Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}. Length : {responseString.Length}");
            return 0;
        }
    }
}
