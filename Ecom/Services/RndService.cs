using System;

namespace Ecom.Services
{
    public interface IRndService
    {
        string GetRandom();
    }

    public interface ISingletonRnd : IRndService { }
    public class SingletonRnd : ISingletonRnd
    {
        private string _randomCode;

        public SingletonRnd()
        {
            _randomCode = new Random().Next(1,100).ToString(); 
        }

        public string GetRandom() => _randomCode;
    }

    public interface IScopedRnd: IRndService { }
    public class ScopedRnd : IScopedRnd
    {
        private string _randomCode;

        public ScopedRnd()
        {
            _randomCode = new Random().Next(1,100).ToString(); 
        }

        public string GetRandom() => _randomCode;
    }

    public interface ITransientRnd : IRndService { }

    public class TransientRnd : ITransientRnd
    {
        private string _rnd;

        public TransientRnd()
        {
            _rnd = new Random().Next(1, 100).ToString();
        }
        public string GetRandom() => _rnd;
    }

}
